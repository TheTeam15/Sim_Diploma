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

            // Act
            model.LancarClassificacao(id, edicao, 25);

            // Assert
            Assert.False(model.UltimaOperacaoSucesso);

            Assert.Contains("invalida", model.UltimaMensagem);
        }

        [Fact]
        public void Gerador_DeveSerValidadoViaMock()
        {
            // Arrange
            var mockGerador = new Mock<IGeradorDiploma>();
            mockGerador.Setup(g => g.Gerar(It.IsAny<string>(), It.IsAny<string>()))
                       .Returns(new byte[] { 0x25, 0x50, 0x44, 0x46 });

            var model = new Model(mockGerador.Object);

            // Act
            model.EmitirDiploma("Joao", "C#");

            // Assert
            mockGerador.Verify(g => g.Gerar("Joao", "C#"), Times.Once);
        }
    }
}