public static class Criptografia
{
    public static string Criptografar(string texto)
    {
        string resultado = "";

        foreach (char c in texto)
        {
            if (char.IsUpper(c))
            {
                int valor = (c - 'A' + 3) * 4;
                resultado += valor.ToString("D4"); // Maiúsculas: 4 dígitos
            }
            else if (char.IsLower(c))
            {
                int valor = (c - 'a' + 3) * 4 + 1000; // Prefixo 1xxx
                resultado += valor.ToString("D4");
            }
            else if (c == ' ')
            {
                resultado += "___"; // Espaço representado como ___
            }
            else
            {
                resultado += c; // Mantém outros caracteres (pontuação, etc.)
            }
        }

        return resultado;
    }

    public static string Descriptografar(string textoCriptografado)
    {
        string resultado = "";

        for (int i = 0; i < textoCriptografado.Length;)
        {
            // Verifica espaço
            if (textoCriptografado.Substring(i, 3) == "___")
            {
                resultado += " ";
                i += 3;
            }
            // Verifica se é número criptografado (4 dígitos)
            else if (char.IsDigit(textoCriptografado[i]))
            {
                string bloco = textoCriptografado.Substring(i, 4);
                int valor = int.Parse(bloco);

                if (valor >= 1000)
                {
                    // Minúscula
                    valor = (valor - 1000);
                    int letra = (valor / 4) - 3;
                    resultado += (char)(letra + 'a');
                }
                else
                {
                    // Maiúscula
                    int letra = (valor / 4) - 3;
                    resultado += (char)(letra + 'A');
                }

                i += 4;
            }
            else
            {
                // Caracteres especiais ou inválidos são mantidos
                resultado += textoCriptografado[i];
                i++;
            }
        }

        return resultado;
    }
}
