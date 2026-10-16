namespace GeradorSenhas.Api.Dados;

public sealed class SenhaArmazenada
{
    public Guid Id { get; set; }

    public string Senha { get; set; } = string.Empty;
}
