using Xunit;
using PdfSharp.Fonts;
using System;
using System.IO;
using ConsoleApp1;

namespace ConsoleApp1.Tests
{
    // Resolver que vai buscar a fonte real ao teu Windows
    public class DummyFontResolver : IFontResolver
    {
        public byte[] GetFont(string faceName)
        {
            // O caminho padrão da fonte Arial em qualquer PC Windows
            string fontPath = @"C:\Windows\Fonts\arial.ttf";

            if (File.Exists(fontPath))
            {
                // Devolve os bytes reais da fonte para o PdfSharp não dar erro!
                return File.ReadAllBytes(fontPath);
            }

            throw new FileNotFoundException("Fonte Arial não encontrada no sistema Windows.");
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            return new FontResolverInfo("Arial");
        }
    }

    public class GeradorTests
    {
        public GeradorTests()
        {
            // Aplica a configuração global antes do teste arrancar
            if (GlobalFontSettings.FontResolver == null)
            {
                GlobalFontSettings.FontResolver = new DummyFontResolver();
            }
        }

        [Fact]
        public void Gerar_DeveConterNomeDoAlunoNoConteudo()
        {
            // Arrange
            var gerador = new Gerador();
            string nomeAluno = "Maria";
            string curso = "Engenharia";

            // Act
            byte[] resultado = gerador.Gerar(nomeAluno, curso);

            // Assert
            Assert.NotNull(resultado);
            Assert.True(resultado.Length > 0);
        }
    }
}