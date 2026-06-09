using Xunit;
using Moq;
using System;
using ConsoleApp1;

namespace ConsoleApp1.Tests
{
    public class DominioTests
    {
        [Fact]
        public void TestarRegistoENota()
        {
            // Arrange
            var mockGerador = new Mock<IGeradorDiploma>();
            var model = new Model(mockGerador.Object);
            int id = 10;
            string edicao = "C#_2026";

            model.RegistarAluno(id, "Joao");
            model.InscreverAluno(id, edicao);
            model.ConcluirInscricao(id, edicao);

            // Act & Assert
            Assert.ThrowsAny<Exception>(() => model.LancarClassificacao(id, edicao, new Nota(25)));
        }

        [Fact]
        public void Gerador_DeveSerValidadoViaMock()
        {
            // Arrange
            var mockGerador = new Mock<IGeradorDiploma>();

            mockGerador
                .Setup(g => g.Gerar("Joao", "C#"))
                .Returns(new byte[] { 0x25, 0x50, 0x44, 0x46 });

            var model = new Model(mockGerador.Object);

            model.GuardarInstituicao(1, "Universidade Aberta", "Lisboa", "Portugal");
            model.CriarCurso(1, 1, "C#", "Curso", "Descrição", "Estrutura");
            model.CriarEdicao(1, 1, "2025/2026", DateTime.Now, DateTime.Now.AddMonths(6), "E-learning");

            model.RegistarAluno(1, "Joao");
            model.InscreverAluno(1, "2025/2026");
            model.ConcluirInscricao(1, "2025/2026");
            model.LancarClassificacao(1, "2025/2026", new Nota(15));

            // Act
            model.EmitirDiploma(1, 1);

            // Assert
            mockGerador.Verify(g => g.Gerar("Joao", "C#"), Times.Once);
        }
    }
}