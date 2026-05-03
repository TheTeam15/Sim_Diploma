using System;

// Representa uma edição associada a um curso
public class Edicao
{
    public string CodigoCurso { get; }
    public string CodigoEdicao { get; }
    public EstadoEdicao Estado { get; private set; }

    public Edicao(string codigoCurso, string codigoEdicao)
    {
        if (string.IsNullOrWhiteSpace(codigoCurso))
            throw new ArgumentException("O código do curso é obrigatório.");

        if (string.IsNullOrWhiteSpace(codigoEdicao))
            throw new ArgumentException("O código da edição é obrigatório.");

        CodigoCurso = codigoCurso.Trim();
        CodigoEdicao = codigoEdicao.Trim();
        Estado = EstadoEdicao.Planeada;
    }

    public void AlterarEstado(EstadoEdicao novoEstado)
    {
        Estado = novoEstado;
    }

    public override string ToString()
    {
        return $"Curso: {CodigoCurso} | Edição: {CodigoEdicao} | Estado: {Estado}";
    }
}