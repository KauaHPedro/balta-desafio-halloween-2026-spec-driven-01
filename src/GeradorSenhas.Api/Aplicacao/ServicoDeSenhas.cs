using GeradorSenhas.Api.Dados;
using GeradorSenhas.Api.Dominio;
using Microsoft.EntityFrameworkCore;

namespace GeradorSenhas.Api.Aplicacao;

public sealed class ServicoDeSenhas
{
    private readonly IGeradorDeSenhas _gerador;
    private readonly ContextoSenhas _contexto;

    public ServicoDeSenhas(IGeradorDeSenhas gerador, ContextoSenhas contexto)
    {
        _gerador = gerador;
        _contexto = contexto;
    }

    public async Task<Guid> GerarAsync(CancellationToken cancelamento = default)
    {
        var senha = _gerador.Gerar();
        var registro = new SenhaArmazenada
        {
            Id = Guid.NewGuid(),
            Senha = senha
        };

        _contexto.Senhas.Add(registro);
        await _contexto.SaveChangesAsync(cancelamento);

        return registro.Id;
    }

    public Task<string?> ObterPorGuidAsync(Guid id, CancellationToken cancelamento = default)
        => _contexto.Senhas
            .Where(senha => senha.Id == id)
            .Select(senha => senha.Senha)
            .FirstOrDefaultAsync(cancelamento);
}
