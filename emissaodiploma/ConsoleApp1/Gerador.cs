using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System;
using System.IO;

/// <summary>
/// Define o contrato para geração de diplomas.
/// 
/// Esta interface permite que o Model dependa de uma abstração,
/// e não de uma implementação concreta de geração de PDF.
/// </summary>
public interface IGeradorDiploma
{
    /// <summary>
    /// Gera um diploma para o aluno e curso indicados.
    /// </summary>
    /// <param name="nomeAluno">Nome do aluno.</param>
    /// <param name="curso">Nome do curso.</param>
    /// <param name="instituicao">Nome da instituição.</param>
    /// <returns>Conteúdo do diploma em formato binário.</returns>
    byte[] Gerar(string nomeAluno, string curso, string instituicao);
}

/// <summary>
/// Implementação concreta do gerador de diplomas.
/// 
/// Utiliza a biblioteca PDFsharp para gerar um ficheiro PDF simples,
/// com informação essencial sobre o aluno, curso, datas e assinatura.
/// </summary>
public class Gerador : IGeradorDiploma
{
    private const string TituloDocumento = "Diploma";
    private const string AutorDocumento = "SimDiplomaMVC";

    /// <summary>
    /// Gera o diploma em PDF.
    /// 
    /// Este método apenas trata da criação física do documento.
    /// As regras de elegibilidade do aluno devem ser validadas previamente
    /// no Model.
    /// </summary>
    /// <param name="nomeAluno">Nome do aluno.</param>
    /// <param name="curso">Nome do curso concluído.</param>
    /// <param name="instituicao">Nome da instituição.</param>
    /// <returns>Array de bytes com o conteúdo do PDF gerado.</returns>
    public byte[] Gerar(string nomeAluno, string curso, string instituicao)
    {
        if(string.IsNullOrWhiteSpace(nomeAluno))
            throw new ArgumentException("Nome do aluno inválido.");

        if (string.IsNullOrWhiteSpace(curso))
            throw new ArgumentException("Curso inválido.");

        if (string.IsNullOrWhiteSpace(instituicao))
            throw new ArgumentException("Instituição inválida.");

        nomeAluno = nomeAluno.Trim();
        curso = curso.Trim();
        instituicao = instituicao.Trim();

        // CRIAÇÃO DO DOCUMENTO
        PdfDocument documento = new PdfDocument();

        documento.Info.Title = TituloDocumento;
        documento.Info.Author = AutorDocumento;
        documento.Info.Subject = $"Diploma de {nomeAluno}";

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

        gfx.DrawString(instituicao, fonteSubtitulo, XBrushes.Black,
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
        string dataAtual = DateTime.Now.ToString("dd/MM/yyyy");

        gfx.DrawString($"Data de conclusão: {dataAtual}", fonteNormal, XBrushes.Black,
            new XPoint(100, 350));

        gfx.DrawString($"Data de emissão: {dataAtual}", fonteNormal, XBrushes.Black,
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