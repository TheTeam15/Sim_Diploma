using System;

/// Classe que transporta o resultado da validação.
/// Usada para comunicar informação do Model para a View.
public class ValidacaoEventArgs
{
    public bool Sucesso { get; }
    public string Mensagem { get; }

    public ValidacaoEventArgs(bool sucesso, string mensagem)
    {
        Sucesso = sucesso;
        Mensagem = mensagem;
    }
}

/// Classe que transporta o diploma gerado (em bytes).
public class DiplomaEmitidoEventArgs
{
    public byte[] PdfBytes { get; }

    public DiplomaEmitidoEventArgs(byte[] pdfBytes)
    {
        PdfBytes = pdfBytes;
    }
}

/// CLASSE DE SUPORTE PARA REGRAS DE NEGÓCIO
/// Representa uma inscrição simplificada
/// (no futuro deve refletir o modelo UML completo)
public class Inscricao
{
    // Inicialização evita warnings de nullability
    public string NomeAluno { get; set; } = "";
    public string Curso { get; set; } = "";

    // Nullable porque pode ainda não existir classificação
    public int? ClassificacaoFinal { get; set; }

    public string Estado { get; set; } = "";
}

/// EXCEÇÕES ESPECÍFICAS (boas práticas)
/// Permitem distinguir tipos de erro de negócio
public class InscricaoInvalidaException : Exception
{
    public InscricaoInvalidaException() : base("Inscrição inválida.") { }
}

public class ClassificacaoInvalidaException : Exception
{
    public ClassificacaoInvalidaException() : base("Classificação inexistente.") { }
}

public class SemAproveitamentoException : Exception
{
    public SemAproveitamentoException() : base("Aluno sem aproveitamento.") { }
}

public class InscricaoNaoConcluidaException : Exception
{
    public InscricaoNaoConcluidaException() : base("Inscrição não concluída.") { }
}

/// MODEL (núcleo da aplicação)
/// Responsável por:
/// - Regras de negócio
/// - Validação de dados
/// - Coordenação da emissão do diploma
/// - Notificação da View (via eventos)
/// 
/// NOTA: Não conhece detalhes de PDFsharp → baixo acoplamento
public class Model
{
    // Dependência abstraída (injeção via interface)
    private readonly IGeradorDiploma _gerador;

    // Eventos que notificam a View
    // Nullable porque podem não ter subscritores
    public event EventHandler<ValidacaoEventArgs>? OnValidacao;
    public event EventHandler<DiplomaEmitidoEventArgs>? OnDiplomaEmitido;

    // Estado interno do Model (importante no MVC Curry & Grace)
    public byte[]? UltimoDiploma { get; private set; }
    public bool UltimaValidacaoSucesso { get; private set; }

    public Model(IGeradorDiploma gerador)
    {
        _gerador = gerador;
    }

    /// MÉTODO DE ELEGIBILIDADE (implementa RG07)
    /// Valida se o aluno pode receber diploma
    private void ValidarElegibilidade(Inscricao inscricao)
    {
        if (inscricao == null)
            throw new InscricaoInvalidaException();

        if (inscricao.ClassificacaoFinal == null)
            throw new ClassificacaoInvalidaException();

        if (inscricao.ClassificacaoFinal < 10)
            throw new SemAproveitamentoException();

        if (inscricao.Estado != "Concluida")
            throw new InscricaoNaoConcluidaException();
    }

    /// Método principal do Model
    /// Executa o fluxo completo:
    /// 1. Validação
    /// 2. Verificação de elegibilidade
    /// 3. Geração do diploma
    /// 4. Notificação da View
    public void EmitirDiploma(string nomeAluno, string curso)
    {
        try
        {
            // Simulação de dados (no futuro virá do sistema real)
            Inscricao inscricao = new Inscricao
            {
                NomeAluno = nomeAluno,
                Curso = curso,
                ClassificacaoFinal = 14,
                Estado = "Concluida"
            };

            // Validação básica
            if (string.IsNullOrWhiteSpace(nomeAluno))
                throw new ArgumentException("Erro: Nome do aluno inválido.");

            // Regras de negócio (RG07)
            ValidarElegibilidade(inscricao);

            UltimaValidacaoSucesso = true;

            // Notifica sucesso de validação
            OnValidacao?.Invoke(this,
                new ValidacaoEventArgs(true, "Validação concluída com sucesso."));

            // Geração do diploma (delegada ao serviço)
            byte[] pdf = _gerador.Gerar(nomeAluno, curso);

            // Guardar estado interno
            UltimoDiploma = pdf;

            // Notificar a View com o resultado
            OnDiplomaEmitido?.Invoke(this,
                new DiplomaEmitidoEventArgs(pdf));
        }
        catch (Exception ex)
        {
            UltimaValidacaoSucesso = false;

            // Notificar erro
            OnValidacao?.Invoke(this,
                new ValidacaoEventArgs(false, ex.Message));
        }
    }
}