using Xunit;
using Moq;

namespace ConsoleApp1.Tests
{
    public class ModelElegibilidadeTests
    {
        [Fact]
        public void EmitirDiploma_DeveChamarGerador_ComMock()
        {
            // Arrange
            var mockGerador = new Mock<IGeradorDiploma>();
            var model = new Model(mockGerador.Object);

            // Act
            model.EmitirDiploma("Joao", "C#");

            // Assert - Verifica se a interface IGeradorDiploma foi acionada 1 vez
            mockGerador.Verify(g => g.Gerar("Joao", "C#"), Times.Once);
        }
    }
}