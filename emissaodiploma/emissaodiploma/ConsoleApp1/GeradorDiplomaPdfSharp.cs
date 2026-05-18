using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.IO;

// ==========================================================
// IMPLEMENTAÇÃO CONCRETA DO GERADOR DE DIPLOMAS
// ==========================================================
// Esta classe utiliza PDFsharp para gerar o documento.
//
// A classe implementa IGeradorDiploma.
// Desta forma o restante sistema conhece apenas a interface.
// ==========================================================

public class GeradorDiplomaPdfSharp : IGeradorDiploma
{
    public byte[] Gerar(string nomeAluno, string curso)
    {
        // Criação do documento PDF
        PdfDocument documento = new PdfDocument();

        // Adição de uma página ao documento
        PdfPage pagina = documento.AddPage();

        // Objeto responsável por desenhar elementos gráficos
        XGraphics gfx = XGraphics.FromPdfPage(pagina);

        // Definição de tipos de letra
        XFont titulo = new XFont("Arial", 24);
        XFont texto = new XFont("Arial", 16);

        // Título principal do diploma
        gfx.DrawString(
            "Diploma",
            titulo,
            XBrushes.Black,
            new XRect(0, 100, pagina.Width.Point, 40),
            XStringFormats.TopCenter
        );

        // Nome do aluno
        gfx.DrawString(
            $"Aluno: {nomeAluno}",
            texto,
            XBrushes.Black,
            new XRect(0, 180, pagina.Width.Point, 30),
            XStringFormats.TopCenter
        );

        // Nome do curso
        gfx.DrawString(
            $"Curso: {curso}",
            texto,
            XBrushes.Black,
            new XRect(0, 220, pagina.Width.Point, 30),
            XStringFormats.TopCenter
        );

        // Stream em memória para devolver o PDF em bytes
        using MemoryStream stream = new MemoryStream();

        documento.Save(stream, false);

        // Conversão do PDF para byte[]
        return stream.ToArray();
    }
}