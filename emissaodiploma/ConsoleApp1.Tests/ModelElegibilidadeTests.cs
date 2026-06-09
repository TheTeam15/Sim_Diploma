using System;
using Moq;
using Xunit;

public class ModelElegibilidadeTests
{
    [Fact]
    public void EmitirDiploma_DeveChamarGerador_ComMock()
    {
        // Arrange
        var mockGerador = new Mock<IGeradorDiploma>();

        mockGerador
            .Setup(g => g.Gerar("Joao", "C#"))
            .Returns(new byte[] { 0x25, 0x50, 0x44, 0x46 });

        var model = new Model(mockGerador.Object);

        model.GuardarInstituicao(1, "Universidade Aberta", "Lisboa", "Portugal");
        model.CriarCurso(1, 1, "C#", "Curso", "Descrição", "Estrutura");
        model.CriarEdicao(1, 1, "2025/2026", DateTime.Now, DateTime.Now.AddMonths(6), "E-learning");

        model.RegistarAluno(1, "Joao");
        model.InscreverAluno(1, "2025/2026");
        model.ConcluirInscricao(1, "2025/2026");
        model.LancarClassificacao(1, "2025/2026", new Nota(15));

        // Act
        model.EmitirDiploma(1, 1);

        // Assert
        mockGerador.Verify(g => g.Gerar("Joao", "C#"), Times.Once);
    }
}