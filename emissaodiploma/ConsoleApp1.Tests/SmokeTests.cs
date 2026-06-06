using Xunit;
using System;
using System.IO;
using ConsoleApp1;

namespace ConsoleApp1.Tests
{
    public class SmokeTests
    {
        [Fact]
        public void FluxoCompleto_DeveGerarPdfNoDisco()
        {
            // 1. ARRANGE - Prepara a aplicação inteira
            // Usamos o Gerador real em vez do Mock para criar mesmo o PDF
            IGeradorDiploma gerador = new Gerador();
            Model model = new Model(gerador);
            View view = new View();

            // Liga a View ao Model para que a View oiça o evento de gerar PDF
            view.Subscrever(model);

            // Liga o Controller
            Controller controller = new Controller(model, view);

            // Apaga o PDF se já existir de um teste anterior
            if (File.Exists("diploma.pdf"))
            {
                File.Delete("diploma.pdf");
            }

            // --- DADOS DE TESTE ---
            string nomeInstituicao = "Universidade Aberta";
            string nomeAluno = "Obi-Wan Kenobi";
            string nomeCurso = "Engenharia Informática";
            string edicao = "2025/2026";

            // 2. ACT - Executa as operações através do Controller simulando o utilizador
            controller.GuardarInstituicao(1, "Universidade Aberta", "Lisboa", "Portugal");
            controller.CriarCurso(1, 1, nomeCurso, "Licenciatura", "Engenharia de Software", "Semestral");
            controller.CriarEdicao(1, 1, edicao, DateTime.Now, DateTime.Now.AddMonths(6), "E-learning");

            controller.CriarAluno(1001, nomeAluno);
            controller.CriarInscricao(1001, edicao);
            controller.ConcluirInscricao(1001, edicao);
            controller.Classificar(1001, edicao, 18.5);

            // O grande momento: pedir o diploma
            controller.EmitirDiploma(nomeAluno, nomeCurso);

            // 3. ASSERT - Verifica se tudo correu bem
            // Verifica se a View reagiu ao evento e criou o ficheiro físico
            bool ficheiroGerado = File.Exists("diploma.pdf");

            Assert.True(model.UltimaValidacaoSucesso, "A validação de elegibilidade falhou no Model.");
            Assert.True(ficheiroGerado, "O ficheiro diploma.pdf não foi gerado no disco.");

            // Verifica se o ficheiro tem conteúdo (não está vazio)
            if (ficheiroGerado)
            {
                FileInfo infoFicheiro = new FileInfo("diploma.pdf");
                Assert.True(infoFicheiro.Length > 0, "O ficheiro PDF foi criado, mas está vazio.");
            }
        }
    }
}