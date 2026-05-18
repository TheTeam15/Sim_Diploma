using System;

// ==========================================================
// VIEW
// ==========================================================
// A View apenas trata:
// - interação com utilizador;
// - apresentação de resultados.
//
// A View não conhece o Model.
// Comunica apenas com o Controller.
// ==========================================================

public class View
{
    private Controller? controller;

    // ======================================================
    // ASSOCIAÇÃO AO CONTROLLER
    // ======================================================

    public void SetController(Controller controller)
    {
        this.controller = controller;
    }

    // ======================================================
    // PEDIDO DE EMISSÃO
    // ======================================================

    public void PedirEmissao()
    {
        Console.WriteLine("Nome do aluno:");
        string nome = Console.ReadLine() ?? "";

        Console.WriteLine("Curso:");
        string curso = Console.ReadLine() ?? "";

        // Comunicação apenas com o Controller
        controller!.EmitirDiploma(nome, curso);
    }

    // ======================================================
    // APRESENTAÇÃO DO RESULTADO DA VALIDAÇÃO
    // ======================================================

    public void MostrarValidacao(ValidacaoEventArgs e)
    {
        Console.WriteLine(e.Mensagem);
    }

    // ======================================================
    // APRESENTAÇÃO DO DIPLOMA
    // ======================================================

    public void MostrarDiploma(byte[] pdf)
    {
        Console.WriteLine($"PDF gerado com sucesso.");
        Console.WriteLine($"Tamanho do ficheiro: {pdf.Length} bytes");
    }
}