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

            int idInstituicao = 1;
            int idCurso = 1;
            int idEdicao = 1;

            model.GuardarInstituicao(
                idInstituicao,
                "Universidade Aberta",
                "Lisboa",
                "Portugal"
            );

            model.CriarCurso(
                idCurso,
                idInstituicao,
                "C#",
                "Curso",
                "Descrição",
                "Estrutura"
            );

            model.CriarEdicao(
                idEdicao,
                idCurso,
                "2025/2026",
                DateTime.Now,
                DateTime.Now.AddMonths(6),
                "E-learning"
            );

            model.RegistarAluno(1, "Joao");
            model.InscreverAluno(1, idEdicao);
            model.ConcluirInscricao(1, idEdicao);

            // Act & Assert
            Assert.ThrowsAny<Exception>(() =>
                model.LancarClassificacao(1, idEdicao, new Nota(notaInvalida))
            );
        }
    }
}