using GeradorSenhas.Api.Dominio;

namespace GeradorSenhas.Testes;

public class GeradorDeSenhasTestes
{
    private readonly GeradorDeSenhas _gerador = new();

    [Fact]
    public void Gerar_Deve_Respeitar_Comprimento_Minimo()
    {
        for (var execucao = 0; execucao < 100; execucao++)
        {
            var senha = _gerador.Gerar();

            Assert.True(senha.Length >= 16, $"A senha '{senha}' possui apenas {senha.Length} caracteres.");
        }
    }

    [Fact]
    public void Gerar_Deve_Conter_Caractere_Especial()
    {
        for (var execucao = 0; execucao < 100; execucao++)
        {
            var senha = _gerador.Gerar();

            Assert.Contains(senha, caractere => !char.IsLetterOrDigit(caractere));
        }
    }

    [Fact]
    public void Gerar_Nao_Deve_Conter_Espacos_Em_Branco()
    {
        for (var execucao = 0; execucao < 100; execucao++)
        {
            var senha = _gerador.Gerar();

            Assert.DoesNotContain(senha, char.IsWhiteSpace);
        }
    }
}
