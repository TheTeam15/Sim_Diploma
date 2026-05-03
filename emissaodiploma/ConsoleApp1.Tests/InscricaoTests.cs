using Xunit;
using Moq;

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

            // Act - O Model valida se a nota é < 0 ou > 20
            model.LancarClassificacao(1, "C#_2026", notaInvalida);

            // Assert
            Assert.False(model.UltimaOperacaoSucesso); // O Model guarda o erro aqui
            Assert.Contains("invalida", model.UltimaMensagem); // A mensagem diz "Nota '{nota}' invalida."
        }
    }
}