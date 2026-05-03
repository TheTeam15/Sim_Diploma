using Xunit;
using System.Reflection;
using ConsoleApp1;

namespace ConsoleApp1.Tests
{
    public class AlunoTests
    {
        [Theory]
        [InlineData("Id")]    
        [InlineData("Nome")] 
        [InlineData("Email")]
        public void Aluno_DevePossuirPropriedadesObrigatorias(string nomePropriedade)
        {
            // Arrange
            var tipoAluno = typeof(Aluno);

            // Act
            PropertyInfo? propriedade = tipoAluno.GetProperty(nomePropriedade);

            // Assert
            Assert.True(propriedade != null,
                $"FALHA DE CONTRATO: A propriedade '{nomePropriedade}' não foi encontrada na classe Aluno.");
        }
    }
}