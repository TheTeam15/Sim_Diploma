using Xunit;
using System;
using System.IO;
using ConsoleApp1;
using PdfSharp.Fonts;

namespace ConsoleApp1.Tests
{
    public class SmokeTests
    {
        [Fact]
        public void FluxoCompleto_DeveGerarPdfNoDisco()
        {
            // 1. ARRANGE - Prepara a aplicação inteira
            // Usamos o Gerador real em vez do Mock para criar mesmo o PDF
            GlobalFontSettings.UseWindowsFontsUnderWindows = true;
            IGeradorDiploma gerador = new Gerador();
            Model model = new Model(gerador);
            View view = new View();

            // Liga a View ao Model para que a View oiça o evento de gerar PDF
            view.Subscrever(model);

            // Liga o Controller
            Controller controller = new Controller(model, view);

            // Apaga PDFs gerados por testes anteriores
            foreach (string ficheiro in Directory.GetFiles(Environment.CurrentDirectory, "diploma_*.pdf"))
            {
                File.Delete(ficheiro);
            }

            // --- DADOS DE TESTE ---
            // Declaras as variáveis de ID no início
            int idInstituicao = 1;
            int idCurso = 1;
            int idEdicao = 1;
            int numeroAluno = 1001;

            string nomeInstituicao = "Universidade Aberta";
            string nomeAluno = "Obi-Wan Kenobi";
            string nomeCurso = "Engenharia Informática";
            string anoLetivo = "2025/2026";

            // 2. ACT - Usas as variáveis em vez dos números "soltos"
            controller.GuardarInstituicao(idInstituicao, nomeInstituicao, "Lisboa", "Portugal");
            controller.CriarCurso(idCurso, idInstituicao, nomeCurso, "Licenciatura", "Engenharia de Software", "Semestral");
            controller.CriarEdicao(idEdicao, idCurso, anoLetivo, DateTime.Now, DateTime.Now.AddMonths(6), "E-learning");

            controller.CriarAluno(numeroAluno, nomeAluno);
            controller.CriarInscricao(numeroAluno, idEdicao);
            controller.ConcluirInscricao(numeroAluno, idEdicao);
            controller.Classificar(numeroAluno, idEdicao, 18.5);

            // Pedir o diploma com as variáveis
            controller.EmitirDiploma(numeroAluno, idEdicao);

            // 3. ASSERT - Verifica se tudo correu bem
            // Verifica se a View reagiu ao evento e criou o ficheiro físico
            string[] ficheirosGerados = Directory.GetFiles(
                Environment.CurrentDirectory,
                "diploma_*.pdf"
            );

            Assert.True(model.UltimaValidacaoSucesso, "A validação de elegibilidade falhou no Model.");
            Assert.True(ficheirosGerados.Length > 0, "Nenhum ficheiro de diploma foi gerado no disco.");

            // Verifica se o ficheiro tem conteúdo
            FileInfo infoFicheiro = new FileInfo(ficheirosGerados[0]);
            Assert.True(infoFicheiro.Length > 0, "O ficheiro PDF foi criado, mas está vazio.");
        }
    }
}