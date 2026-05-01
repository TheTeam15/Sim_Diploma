using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System;
using System.IO;

/// Interface que define o contrato de geração de diplomas.
/// 
/// Permite:
/// - Trocar a implementação
/// - Reduzir acoplamento com o Model
public interface IGeradorDiploma
{
    byte[] Gerar(string nomeAluno, string curso);
}

/// Implementação concreta do gerador de diplomas.
/// 
/// Utiliza a API PDFsharp para gerar um diploma com layout estruturado
public class Gerador : IGeradorDiploma
{
    public byte[] Gerar(string nomeAluno, string curso)
    {
        
        // CRIAÇÃO DO DOCUMENTO
        PdfDocument documento = new PdfDocument();
        PdfPage pagina = documento.AddPage();

        // Definir tamanho A4
        pagina.Size = PdfSharp.PageSize.A4;

        XGraphics gfx = XGraphics.FromPdfPage(pagina);

        // Dimensões da página
        double larguraPagina = pagina.Width.Point;
        double alturaPagina = pagina.Height.Point;

        
        // FONTES
        XFont fonteTitulo = new XFont("Arial", 24);
        XFont fonteSubtitulo = new XFont("Arial", 18);
        XFont fonteNormal = new XFont("Arial", 12);

        
        // TOPO - INSTITUIÇÃO
        string nomeInstituicao = "Universidade Exemplo";

        gfx.DrawString(nomeInstituicao, fonteSubtitulo, XBrushes.Black,
            new XRect(0, 60, larguraPagina, alturaPagina),
            XStringFormats.TopCenter);

        
        // TÍTULO DO DOCUMENTO
        gfx.DrawString("DIPLOMA", fonteTitulo, XBrushes.Black,
            new XRect(0, 120, larguraPagina, alturaPagina),
            XStringFormats.TopCenter);

        
        // TEXTO DESCRITIVO
        gfx.DrawString("Certifica-se que", fonteNormal, XBrushes.Black,
            new XRect(0, 180, larguraPagina, alturaPagina),
            XStringFormats.TopCenter);

        
        // NOME DO ALUNO (DESTAQUE)
        gfx.DrawString(nomeAluno, fonteSubtitulo, XBrushes.Black,
            new XRect(0, 220, larguraPagina, alturaPagina),
            XStringFormats.TopCenter);

        
        // CURSO
        gfx.DrawString($"concluiu com aproveitamento o curso de {curso}", fonteNormal, XBrushes.Black,
            new XRect(0, 260, larguraPagina, alturaPagina),
            XStringFormats.TopCenter);

        
        // DATAS
        string dataConclusao = DateTime.Now.ToString("dd/MM/yyyy");
        string dataEmissao = DateTime.Now.ToString("dd/MM/yyyy");

        gfx.DrawString($"Data de conclusão: {dataConclusao}", fonteNormal, XBrushes.Black,
            new XPoint(100, 350));

        gfx.DrawString($"Data de emissão: {dataEmissao}", fonteNormal, XBrushes.Black,
            new XPoint(100, 380));

        
        // ASSINATURA (simulada)
        gfx.DrawString("_________________________", fonteNormal, XBrushes.Black,
            new XPoint(larguraPagina - 250, 450));

        gfx.DrawString("Diretor Académico", fonteNormal, XBrushes.Black,
            new XPoint(larguraPagina - 230, 470));

        
        // EXPORTAÇÃO
        using (MemoryStream stream = new MemoryStream())
        {
            documento.Save(stream, false);
            return stream.ToArray();
        }
    }
}