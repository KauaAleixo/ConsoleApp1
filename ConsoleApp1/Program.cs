using System;

class Program
{
    static void Main()
    {
        Console.Write("Digite a mensagem para criptografar: ");
        string? mensagem = Console.ReadLine();

        if (string.IsNullOrEmpty(mensagem))
        {
            Console.WriteLine("Mensagem vazia. Encerrando.");
            return;
        }

        string criptografada = Criptografia.Criptografar(mensagem);
        Console.WriteLine("Mensagem Criptografada: " + criptografada);

        string descriptografada = Criptografia.Descriptografar(criptografada);
        Console.WriteLine("Mensagem Descriptografada: " + descriptografada);
    }
}
