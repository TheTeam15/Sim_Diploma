using Xunit;
using Moq;
using System;
using ConsoleApp1;

namespace ConsoleApp1.Tests
{
    public class InscricaoTests
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(21)]
        public void LancarClassificacao_DeveFalharForaDasFronteiras(double notaInvalida)
        {
            // Arrange
            var mockGerador = new Mock<IGeradorDiploma>();
            var model = new Model(mockGerador.Object);
            model.RegistarAluno(1, "Joao");
            model.InscreverAluno(1, "C#_2026");
            model.ConcluirInscricao(1, "C#_2026");

            // Act & Assert
            // Instanciamos o objeto Nota e garantimos que uma exceção é lançada
            Assert.ThrowsAny<Exception>(() => model.LancarClassificacao(1, "C#_2026", new Nota(notaInvalida)));
        }
    }
}