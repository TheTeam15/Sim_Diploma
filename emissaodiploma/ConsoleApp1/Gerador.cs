using System.Text;

namespace SimDiplomaMVC
{
    /// Interface que define o contrato de geração de diplomas.
    ///
    /// Permite trocar a implementação concreta sem alterar o Model.
    public interface IGeradorDiploma
    {
        byte[] Gerar(string nomeAluno, string curso);
    }

    /// Implementação temporária do gerador de diplomas.
    ///
    /// Neste protótipo:
    /// - não gera ainda um PDF real;
    /// - apenas devolve bytes simulados.
    ///
    /// No projeto final:
    /// - deverá ser substituído ou completado com PDFsharp.
    public class Gerador : IGeradorDiploma
    {
        public byte[] Gerar(string nomeAluno, string curso)
        {
            // Simulação simples do conteúdo do diploma
            string conteudo = "Diploma de " + nomeAluno + " - " + curso;

            // Conversão para bytes para simular o ficheiro final
            return Encoding.UTF8.GetBytes(conteudo);
        }
    }
}