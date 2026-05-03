using Xunit;
using Moq;

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

            // Act - O Model bloqueia nomes vazios (string.IsNullOrWhiteSpace)
            model.RegistarAluno(1, "");

            // Assert
            Assert.False(model.UltimaOperacaoSucesso);
            Assert.Contains("Nome invalido", model.UltimaMensagem);
        }
    }
}