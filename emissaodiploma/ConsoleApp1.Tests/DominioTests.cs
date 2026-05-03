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
            // Não usamos Assert.Throws porque o Model apanha a sua própria exceção no código dos DEVs[cite: 4]
            model.LancarClassificacao(id, edicao, 25);

            // Assert
            // Validamos se o Model registou internamente que a operação falhou[cite: 4]
            Assert.False(model.UltimaOperacaoSucesso);

            // Validamos se a mensagem de erro é a correta (sobre a nota ser inválida)
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