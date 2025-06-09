using System;
using System.Linq;
using System.Text.RegularExpressions;

public static class Criptografia
{
    private static Random random = new Random();

    public static string Criptografar(string texto)
    {
        int chave = random.Next(2, 10); // Chave dinâmica entre 2 e 10
        string resultado = "";

        foreach (char c in texto)
        {
            if (char.IsUpper(c))
            {
                int valor = (c - 'A' + chave);
                valor = (int)Math.Pow(valor, 2); // Aplicando potência
                resultado += valor.ToString("D4") + GerarRuido(); // Adiciona ruído
            }
            else if (char.IsLower(c))
            {
                int valor = (c - 'a' + chave);
                valor = (int)Math.Pow(valor, 2) + 1000; // Prefixo 1xxx
                resultado += valor.ToString("D4") + GerarRuido();
            }
            else if (c == ' ')
            {
                resultado += "9999" + GerarRuido(); // Código especial para espaço
            }
            else
            {
                resultado += c;
            }
        }

        return resultado + $"|{chave}"; // Adiciona a chave ao final (sem embaralhar)
    }

    public static string Descriptografar(string textoCriptografado)
    {
        string[] partes = textoCriptografado.Split('|');
        if (partes.Length < 2) return "Erro: Chave ausente.";

        string textoComRuido = partes[0];
        int chave = int.Parse(partes[1]);

        string resultado = "";
        int i = 0;

        while (i < textoComRuido.Length)
        {
            // Verifica se há um bloco de 4 dígitos
            if (i + 4 <= textoComRuido.Length && Regex.IsMatch(textoComRuido.Substring(i, 4), @"^\d{4}$"))
            {

                string bloco = textoComRuido.Substring(i, 4);
                i += 4;

                // Pula o ruído (2 dígitos) se existir após o bloco
                if (i + 2 <= textoComRuido.Length && Regex.IsMatch(textoComRuido.Substring(i, 2), @"^\d{2}$"))
                {
                    i += 2;
                }

                if (bloco == "9999") // Espaço
                {
                    resultado += " ";
                }
                else
                {
                    int valor = int.Parse(bloco);
                    if (valor >= 1000) // Letra minúscula
                    {
                        valor -= 1000;
                        int letra = (int)Math.Sqrt(valor) - chave;
                        if (letra >= 0 && letra < 26)
                            resultado += (char)(letra + 'a');
                    }
                    else // Letra maiúscula
                    {
                        int letra = (int)Math.Sqrt(valor) - chave;
                        if (letra >= 0 && letra < 26)
                            resultado += (char)(letra + 'A');
                    }
                }
            }
            else
            {
                // Se não for um bloco de 4 dígitos, mantém o caractere original
                resultado += textoComRuido[i];
                i++;
            }
        }

        return resultado;
    }

    private static string GerarRuido()
    {
        return random.Next(10, 99).ToString(); // Gera ruído aleatório de 2 dígitos
    }
}