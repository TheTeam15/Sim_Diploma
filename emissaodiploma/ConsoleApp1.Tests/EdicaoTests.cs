using Xunit;
using Moq;
using System;
using ConsoleApp1;

namespace ConsoleApp1.Tests
{
    public class EdicaoTests
    {
        [Fact]
        public void InscreverAluno_EdicaoVazia_DeveFalhar()
        {
            // Arrange
            var mockGerador = new Mock<IGeradorDiploma>();
            var model = new Model(mockGerador.Object);
            model.RegistarAluno(1, "Ana");

            // Act & Assert
            var excecao = Assert.Throws<ArgumentException>(() => model.InscreverAluno(1, ""));

            Assert.Contains("invalida", excecao.Message.ToLower());
        }
    }
}