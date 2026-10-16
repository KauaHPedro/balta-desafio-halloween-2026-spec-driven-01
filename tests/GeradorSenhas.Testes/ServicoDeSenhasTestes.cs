using GeradorSenhas.Api.Aplicacao;
using GeradorSenhas.Api.Dados;
using GeradorSenhas.Api.Dominio;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace GeradorSenhas.Testes;

public class ServicoDeSenhasTestes : IDisposable
{
    private readonly SqliteConnection _conexao;
    private readonly ContextoSenhas _contexto;
    private readonly ServicoDeSenhas _servico;

    public ServicoDeSenhasTestes()
    {
        _conexao = new SqliteConnection("Data Source=:memory:");
        _conexao.Open();

        var opcoes = new DbContextOptionsBuilder<ContextoSenhas>()
            .UseSqlite(_conexao)
            .Options;

        _contexto = new ContextoSenhas(opcoes);
        _contexto.Database.EnsureCreated();

        _servico = new ServicoDeSenhas(new GeradorDeSenhas(), _contexto);
    }

    [Fact]
    public async Task GerarAsync_Deve_Persistir_Senha_Valida_E_Retornar_Guid()
    {
        var id = await _servico.GerarAsync();

        var senha = await _servico.ObterPorGuidAsync(id);

        Assert.NotEqual(Guid.Empty, id);
        Assert.NotNull(senha);
        Assert.True(senha!.Length >= 16);
        Assert.Contains(senha, caractere => !char.IsLetterOrDigit(caractere));
        Assert.DoesNotContain(senha, char.IsWhiteSpace);
    }

    [Fact]
    public async Task ObterPorGuidAsync_Deve_Retornar_Nulo_Quando_Senha_Nao_Existe()
    {
        var senha = await _servico.ObterPorGuidAsync(Guid.NewGuid());

        Assert.Null(senha);
    }

    public void Dispose()
    {
        _contexto.Dispose();
        _conexao.Dispose();
    }
}
