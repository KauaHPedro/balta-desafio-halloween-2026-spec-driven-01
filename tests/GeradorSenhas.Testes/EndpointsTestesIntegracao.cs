using System.Net;
using System.Net.Http.Json;
using GeradorSenhas.Api.Contratos;
using GeradorSenhas.Api.Dados;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GeradorSenhas.Testes;

public class EndpointsTestesIntegracao : IClassFixture<EndpointsTestesIntegracao.FabricaDaAplicacao>
{
    private readonly FabricaDaAplicacao _fabrica;

    public EndpointsTestesIntegracao(FabricaDaAplicacao fabrica)
    {
        _fabrica = fabrica;
    }

    [Fact]
    public async Task PostSenhas_Deve_Retornar_200_Com_Guid()
    {
        var cliente = _fabrica.CreateClient();

        var resposta = await cliente.PostAsync("/senhas", content: null);

        Assert.Equal(HttpStatusCode.OK, resposta.StatusCode);

        var corpo = await resposta.Content.ReadFromJsonAsync<GerarSenhaResposta>();
        Assert.NotNull(corpo);
        Assert.NotEqual(Guid.Empty, corpo!.Id);
    }

    [Fact]
    public async Task GetSenha_Deve_Retornar_200_Com_Senha_Que_Respeita_As_Regras()
    {
        var cliente = _fabrica.CreateClient();

        var geracao = await cliente.PostAsync("/senhas", content: null);
        var criada = await geracao.Content.ReadFromJsonAsync<GerarSenhaResposta>();

        var resposta = await cliente.GetAsync($"/senhas/{criada!.Id}");

        Assert.Equal(HttpStatusCode.OK, resposta.StatusCode);

        var recuperada = await resposta.Content.ReadFromJsonAsync<RecuperarSenhaResposta>();
        Assert.NotNull(recuperada);
        Assert.True(recuperada!.Senha.Length >= 16);
        Assert.Contains(recuperada.Senha, caractere => !char.IsLetterOrDigit(caractere));
        Assert.DoesNotContain(recuperada.Senha, char.IsWhiteSpace);
    }

    [Fact]
    public async Task GetSenha_Com_Guid_Invalido_Deve_Retornar_400_Com_ProblemDetails()
    {
        var cliente = _fabrica.CreateClient();

        var resposta = await cliente.GetAsync("/senhas/isto-nao-e-um-guid");

        Assert.Equal(HttpStatusCode.BadRequest, resposta.StatusCode);
        Assert.Equal("application/problem+json", resposta.Content.Headers.ContentType?.MediaType);
    }

    [Fact]
    public async Task GetSenha_Com_Guid_Inexistente_Deve_Retornar_404()
    {
        var cliente = _fabrica.CreateClient();

        var resposta = await cliente.GetAsync($"/senhas/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, resposta.StatusCode);
    }

    public sealed class FabricaDaAplicacao : WebApplicationFactory<Program>
    {
        private readonly SqliteConnection _conexao = new("Data Source=:memory:");

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            _conexao.Open();

            builder.ConfigureServices(servicos =>
            {
                servicos.RemoveAll<DbContextOptions<ContextoSenhas>>();
                servicos.RemoveAll<ContextoSenhas>();
                servicos.AddDbContext<ContextoSenhas>(opcoes => opcoes.UseSqlite(_conexao));
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                _conexao.Dispose();
            }
        }
    }
}
