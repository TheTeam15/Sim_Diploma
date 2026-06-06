using Xunit;
using Xunit.Abstractions; // Necessário para escrever mensagens no teste
using System.Reflection;
using ConsoleApp1;

namespace ConsoleApp1.Tests
{
    public class AlunoTests
    {
        private readonly ITestOutputHelper _output;

        // O xUnit injeta automaticamente esta ferramenta para podermos escrever logs
        public AlunoTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [InlineData("Id", true)]
        [InlineData("Nome", true)]
        [InlineData("Email", false)] // Marcado como opcional de acordo com o RF05
        public void Aluno_VerificarPropriedades(string nomePropriedade, bool obrigatoria)
        {
            // Arrange
            var tipoAluno = typeof(Aluno);

            // Act
            PropertyInfo? propriedade = tipoAluno.GetProperty(nomePropriedade);

            // Assert
            if (obrigatoria)
            {
                // Se for obrigatória e faltar, o teste falha (Vermelho)
                Assert.True(propriedade != null,
                    $"FALHA DE CONTRATO: A propriedade obrigatória '{nomePropriedade}' não foi encontrada na classe Aluno.");
            }
            else
            {
                // Se for opcional e faltar, o teste passa (Verde), mas deixa um aviso
                if (propriedade == null)
                {
                    _output.WriteLine($"INFO: A propriedade opcional '{nomePropriedade}' não está presente. Conforme o RF05, o contacto é 'se necessário'.");
                }
                else
                {
                    _output.WriteLine($"INFO: A propriedade opcional '{nomePropriedade}' foi implementada.");
                }
            }
        }
    }
}