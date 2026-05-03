using System;

// Representa um curso da aplicação demonstradora
public class Curso
{
    public string Codigo { get; }

    public Curso(string codigo)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            throw new ArgumentException("O código do curso é obrigatório.");

        Codigo = codigo.Trim();
    }

    public override string ToString()
    {
        return $"Curso: {Codigo}";
    }
}