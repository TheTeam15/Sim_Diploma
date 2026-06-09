using Xunit;
using Moq;
using System;
using ConsoleApp1;

namespace ConsoleApp1.Tests
{
    public class ModelTests
    {
        [Fact]
        public void RegistarAluno_NomeVazio_DeveFalhar()
        {
            // Arrange
            var mockGerador = new Mock<IGeradorDiploma>();
            var model = new Model(mockGerador.Object);

            // Act & Assert
            // O xUnit captura a exceção em vez de deixar o teste falhar
            var excecao = Assert.Throws<ArgumentException>(() => model.RegistarAluno(1, ""));

            // Verificamos se a mensagem da exceção contém a informação correta
            Assert.Contains("invalido", excecao.Message);
        }
    }
}