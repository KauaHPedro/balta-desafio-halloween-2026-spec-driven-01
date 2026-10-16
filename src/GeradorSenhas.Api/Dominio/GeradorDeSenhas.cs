using System.Security.Cryptography;

namespace GeradorSenhas.Api.Dominio;

public sealed class GeradorDeSenhas : IGeradorDeSenhas
{
    public const int ComprimentoMinimo = 16;

    private const int ComprimentoMaximo = 20;

    private const string LetrasMaiusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string LetrasMinusculas = "abcdefghijklmnopqrstuvwxyz";
    private const string Digitos = "0123456789";
    private const string Especiais = "!@#$%^&*()-_=+[]{};:,.?/";

    private const string TodosOsCaracteres = LetrasMaiusculas + LetrasMinusculas + Digitos + Especiais;

    public string Gerar()
    {
        var comprimento = RandomNumberGenerator.GetInt32(ComprimentoMinimo, ComprimentoMaximo + 1);
        var caracteres = new char[comprimento];

        caracteres[0] = SortearCaractere(Especiais);
        for (var posicao = 1; posicao < comprimento; posicao++)
        {
            caracteres[posicao] = SortearCaractere(TodosOsCaracteres);
        }

        Embaralhar(caracteres);

        return new string(caracteres);
    }

    private static char SortearCaractere(string alfabeto)
        => alfabeto[RandomNumberGenerator.GetInt32(alfabeto.Length)];

    private static void Embaralhar(char[] caracteres)
    {
        for (var posicao = caracteres.Length - 1; posicao > 0; posicao--)
        {
            var sorteada = RandomNumberGenerator.GetInt32(posicao + 1);
            (caracteres[posicao], caracteres[sorteada]) = (caracteres[sorteada], caracteres[posicao]);
        }
    }
}
