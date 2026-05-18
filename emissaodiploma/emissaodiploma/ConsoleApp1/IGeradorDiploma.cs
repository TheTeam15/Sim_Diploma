/// Interface que define o contrato de geração de diplomas.
/// 
// Esta interface abstrai o mecanismo concreto de geração PDF.
// O Model depende apenas desta interface e não diretamente
// da implementação PDFsharp.
//
// Isto reduz o acoplamento e facilita substituição futura
// da tecnologia de geração de PDF.
/// Vantagem:
/// - Permite trocar PDFsharp por outra solução sem alterar o Model
public interface IGeradorDiploma
{
    /// Gera um diploma em formato PDF (array de bytes)
    byte[] Gerar(string nomeAluno, string curso);
}
