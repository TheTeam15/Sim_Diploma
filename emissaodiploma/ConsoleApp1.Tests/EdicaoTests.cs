using Xunit;
using Moq;

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

            // Act - Tentar inscrever numa edição vazia
            model.InscreverAluno(1, "");

            // Assert - O Model bloqueia com "Edicao invalida."
            Assert.False(model.UltimaOperacaoSucesso);
            Assert.Contains("Edicao invalida", model.UltimaMensagem);
        }
    }
}