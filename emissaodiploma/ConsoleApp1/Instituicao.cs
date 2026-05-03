using System;

// Representa a instituição da aplicação demonstradora
public class Instituicao
{
    public string Nome { get; }

    public Instituicao(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome da instituição é obrigatório.");

        Nome = nome.Trim();
    }

    public override string ToString()
    {
        return $"Instituição: {Nome}";
    }
}