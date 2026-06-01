using System;
using System.Collections.Generic;
using System.Linq;

/// Classe que transporta o resultado da validação.
/// Usada para comunicar informação do Model para a View.

public interface IAluno
{
    int Id { get; }
    string Nome { get; }
}

public interface IInscricaoAluno
{
    int AlunoId { get; }
    string Edicao { get; }
    bool Ativa { get; }
    bool TemClassificacao { get; }
}

public interface IClassificacao
{
    double NotaValor { get; }
    bool Aprovado { get; }
}

/// Transporta o resultado de uma operacao (sucesso/erro + mensagem)
public class ResultadoEventArgs : EventArgs
{
    public bool Sucesso { get; }
    public string Mensagem { get; }

    public ResultadoEventArgs(bool sucesso, string mensagem)
    {
        Sucesso = sucesso;
        Mensagem = mensagem;
    }
}

/// Transporta uma InscricaoAluno criada
public class InscricaoAlunoEventArgs : EventArgs
{
    public IInscricaoAluno Inscricao { get; }

    public InscricaoAlunoEventArgs(IInscricaoAluno inscricao)
    {
        Inscricao = inscricao;
    }
}

/// Transporta uma Classificacao criada
public class ClassificacaoEventArgs : EventArgs
{
    public IClassificacao Classificacao { get; }

    public ClassificacaoEventArgs(IClassificacao classificacao)
    {
        Classificacao = classificacao;
    }
}

/// Transporta um Aluno consultado (pode ser null se nao encontrado)
public class AlunoEventArgs : EventArgs
{
    public IAluno? Aluno { get; }

    public AlunoEventArgs(IAluno? aluno)
    {
        Aluno = aluno;
    }
}

/// Transporta uma InscricaoAluno consultada (pode ser null)
public class InscricaoAlunoConsultadaEventArgs : EventArgs
{
    public IInscricaoAluno? Inscricao { get; }

    public InscricaoAlunoConsultadaEventArgs(IInscricaoAluno? inscricao)
    {
        Inscricao = inscricao;
    }
}

/// Transporta uma Classificacao consultada (pode ser null)
public class ClassificacaoConsultadaEventArgs : EventArgs
{
    public IClassificacao? Classificacao { get; }

    public ClassificacaoConsultadaEventArgs(IClassificacao? classificacao)
    {
        Classificacao = classificacao;
    }
}

public class ValidacaoEventArgs : EventArgs
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
public class DiplomaEmitidoEventArgs : EventArgs
{
    public byte[] PdfBytes { get; }

    public DiplomaEmitidoEventArgs(byte[] pdfBytes)
    {
        PdfBytes = pdfBytes;
    }
}

public interface IModelEventos
{
    event EventHandler<ResultadoEventArgs>? Resultado;

    event EventHandler<InscricaoAlunoEventArgs>? InscricaoCriada;
    event EventHandler<ClassificacaoEventArgs>? ClassificacaoCriada;

    event EventHandler<AlunoEventArgs>? AlunoConsultado;
    event EventHandler<InscricaoAlunoConsultadaEventArgs>? InscricaoConsultada;
    event EventHandler<ClassificacaoConsultadaEventArgs>? ClassificacaoConsultada;

    event EventHandler<ValidacaoEventArgs>? OnValidacao;
    event EventHandler<DiplomaEmitidoEventArgs>? OnDiplomaEmitido;
}

/// CLASSE DE SUPORTE PARA REGRAS DE NEGÓCIO
/// (no futuro deve refletir o modelo UML completo)

/// Representa um aluno do sistema
public class Aluno : IAluno
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    // Relacao 1:N — um aluno pode ter varias inscricoes
    public List<InscricaoAluno> Inscricoes { get; set; } = new();

    /// Verifica se existe inscricao ativa numa edicao
    public bool TemInscricaoAtiva(string edicao)
        => Inscricoes.Any(i => i.Edicao == edicao && i.Ativa);

    /// Obtem inscricao por edicao
    public InscricaoAluno? ObterInscricao(string edicao)
        => Inscricoes.FirstOrDefault(i => i.Edicao == edicao);
}

/// Representa a inscricao de um aluno numa edicao
    public class InscricaoAluno : IInscricaoAluno
{
    public int AlunoId { get; set; }
    public string Edicao { get; set; } = string.Empty;

    // Regra: fica false quando a inscricao e concluida
    public bool Ativa { get; set; } = true;

    // Regra: so pode existir UMA classificacao por inscricao
    public Classificacao? ClassificacaoFinal { get; set; }

    public bool Concluida => !Ativa;
    public bool TemClassificacao => ClassificacaoFinal != null;
}

/// Representa a classificacao final de uma inscricao
public class Classificacao : IClassificacao
{
    public InscricaoAluno Inscricao { get; set; } = null!;
    public Nota Nota { get; set; } = null!;

    // Regra: aprovado se nota >= 10
    public bool Aprovado => Nota.Valor >= 10;

    // propriedade usada pela interface
    public double NotaValor => Nota.Valor;
}

/// Representa uma inscrição simplificada
public class Inscricao
{
    // Inicialização evita warnings de nullability
    public string NomeAluno { get; set; } = "";
    public string Curso { get; set; } = "";

    // Nullable porque pode ainda não existir classificação
    public int? ClassificacaoFinal { get; set; }

    public string Estado { get; set; } = "";
}

/// Value Object que representa uma nota académica válida (0-20)
public class Nota
{
    public double Valor { get; }

    public Nota(double valor)
    {
        if (valor < 0 || valor > 20)
            throw new NotaInvalidaException($"Nota '{valor}' invalida. Deve estar entre 0 e 20.");
        Valor = valor;
    }
}

/// EXCEÇÕES ESPECÍFICAS (boas práticas)
/// Permitem distinguir tipos de erro de negócio

public class AlunoNaoEncontradoException : Exception
{
    public AlunoNaoEncontradoException(string msg) : base(msg) { }
}

public class AlunoJaExisteException : Exception
{
    public AlunoJaExisteException(string msg) : base(msg) { }
}

public class InscricaoAlunoNaoEncontradaException : Exception
{
    public InscricaoAlunoNaoEncontradaException(string msg) : base(msg) { }
}

public class InscricaoAlunoDuplicadaException : Exception
{
    public InscricaoAlunoDuplicadaException(string msg) : base(msg) { }
}

public class InscricaoAlunoJaConcluidaException : Exception
{
    public InscricaoAlunoJaConcluidaException(string msg) : base(msg) { }
}

public class ClassificacaoJaExisteException : Exception
{
    public ClassificacaoJaExisteException(string msg) : base(msg) { }
}

public class NotaInvalidaException : Exception
{
    public NotaInvalidaException(string msg) : base(msg) { }
}

public class InscricaoAlunoAindaAtivaException : Exception
{
    public InscricaoAlunoAindaAtivaException(string msg) : base(msg) { }
}

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
public class Model : IModelEventos
{
    private List<Aluno> alunos = new(); 

    /// Ultimo aluno consultado — estado interno do Model
    public Aluno? UltimoAlunoConsultado { get; private set; }

    /// Ultima inscricao consultada — estado interno do Model
    public InscricaoAluno? UltimaInscricaoConsultada { get; private set; }

    /// Ultima classificacao consultada — estado interno do Model
    public Classificacao? UltimaClassificacaoConsultada { get; private set; }

    /// Resultado da ultima operacao
    public bool UltimaOperacaoSucesso { get; private set; }

    /// Mensagem da ultima operacao
    public string UltimaMensagem { get; private set; } = string.Empty;

    // ================= EVENTOS (EventHandler) =================

    /// Notifica resultado generico de operacao (sucesso ou erro)
    public event EventHandler<ResultadoEventArgs>? Resultado;

    /// Notifica que uma inscricao foi criada
    public event EventHandler<InscricaoAlunoEventArgs>? InscricaoCriada;

    /// Notifica que uma classificacao foi lancada
    public event EventHandler<ClassificacaoEventArgs>? ClassificacaoCriada;

    /// Notifica resultado de consulta de aluno
    public event EventHandler<AlunoEventArgs>? AlunoConsultado;

    /// Notifica resultado de consulta de inscricao
    public event EventHandler<InscricaoAlunoConsultadaEventArgs>? InscricaoConsultada;

    /// Notifica resultado de consulta de classificacao
    public event EventHandler<ClassificacaoConsultadaEventArgs>? ClassificacaoConsultada;
    
    // Dependência abstraída (injeção via interface)
    private readonly IGeradorDiploma _gerador;

    // Eventos que notificam a View
    // Nullable porque podem não ter subscritores
    public event EventHandler<ValidacaoEventArgs>? OnValidacao;
    public event EventHandler<DiplomaEmitidoEventArgs>? OnDiplomaEmitido;

    // Estado interno do Model (importante no MVC Curry & Grace)
    public byte[]? UltimoDiploma { get; private set; }
    public bool UltimaValidacaoSucesso { get; private set; }

    /// Regista um novo aluno no sistema
    public void RegistarAluno(int id, string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome invalido.");

        if (id <= 0)
            throw new ArgumentException("ID invalido.");

        if (alunos.Any(a => a.Id == id))
            throw new AlunoJaExisteException($"Ja existe um aluno com o ID {id}.");

        var aluno = new Aluno { Id = id, Nome = nome };
        alunos.Add(aluno);

        // Atualizar estado interno — Curry & Grace
        UltimaOperacaoSucesso = true;
        UltimaMensagem = $"Aluno '{nome}' registado com sucesso.";
            
        Resultado?.Invoke(this, new ResultadoEventArgs(true, UltimaMensagem));
    }

    /// Verifica se existe aluno com o id dado
    public bool ExisteAluno(int id)
        => alunos.Any(a => a.Id == id);

    // ================= INSCRICAO =================

    /// Inscreve um aluno numa edicao
    public void InscreverAluno(int alunoId, string edicao)
    {
        var aluno = alunos.FirstOrDefault(a => a.Id == alunoId)
            ?? throw new AlunoNaoEncontradoException($"Aluno com ID {alunoId} nao existe.");

        if (string.IsNullOrWhiteSpace(edicao))
            throw new ArgumentException("Edicao invalida.");

        if (aluno.TemInscricaoAtiva(edicao))
            throw new InscricaoAlunoDuplicadaException($"Ja existe uma inscricao ativa na edicao '{edicao}'.");

        var inscricao = new InscricaoAluno
        {
            AlunoId = alunoId,
            Edicao = edicao,
            Ativa = true
        };

        aluno.Inscricoes.Add(inscricao);

        // Atualizar estado interno — Curry & Grace
        UltimaInscricaoConsultada = inscricao;
        UltimaOperacaoSucesso = true;
        UltimaMensagem = $"Inscricao na edicao '{edicao}' realizada com sucesso.";

        Resultado?.Invoke(this, new ResultadoEventArgs(true, UltimaMensagem));
        InscricaoCriada?.Invoke(this, new InscricaoAlunoEventArgs(inscricao));
    }

    /// Conclui a inscricao de um aluno numa edicao
    public void ConcluirInscricao(int alunoId, string edicao)
    {
        var aluno = alunos.FirstOrDefault(a => a.Id == alunoId)
            ?? throw new AlunoNaoEncontradoException($"Aluno com ID {alunoId} nao existe.");

        var inscricao = aluno.ObterInscricao(edicao)
            ?? throw new InscricaoAlunoNaoEncontradaException($"Inscricao na edicao '{edicao}' nao encontrada.");

        if (!inscricao.Ativa)
            throw new InscricaoAlunoJaConcluidaException("A inscricao ja se encontra concluida.");

        inscricao.Ativa = false;

        // Atualizar estado interno — Curry & Grace
        UltimaOperacaoSucesso = true;
        UltimaMensagem = $"Inscricao na edicao '{edicao}' concluida com sucesso.";

        Resultado?.Invoke(this, new ResultadoEventArgs(true, UltimaMensagem));
    }

    // ================= CLASSIFICACAO =================

    /// Lanca a classificacao final de um aluno numa edicao
    public void LancarClassificacao(int alunoId, string edicao, Nota nota)
    {
        var aluno = alunos.FirstOrDefault(a => a.Id == alunoId)
            ?? throw new AlunoNaoEncontradoException($"Aluno com ID {alunoId} nao existe.");

        var inscricao = aluno.ObterInscricao(edicao)
            ?? throw new InscricaoAlunoNaoEncontradaException($"Inscricao na edicao '{edicao}' nao encontrada.");

        if (inscricao.Ativa)
            throw new InscricaoAlunoAindaAtivaException("A inscricao deve estar concluida antes de lancar classificacao.");

        if (inscricao.ClassificacaoFinal != null)
            throw new ClassificacaoJaExisteException("Ja existe uma classificacao para esta inscricao.");

        var classificacao = new Classificacao
        {
            Inscricao = inscricao,
            Nota = nota
        };

        inscricao.ClassificacaoFinal = classificacao;

        // Atualizar estado interno — Curry & Grace
        UltimaClassificacaoConsultada = classificacao;
        UltimaOperacaoSucesso = true;
        UltimaMensagem = $"Classificacao lancada: {nota.Valor} | Aprovado: {classificacao.Aprovado}";
    
        Resultado?.Invoke(this, new ResultadoEventArgs(true, UltimaMensagem));
        ClassificacaoCriada?.Invoke(this, new ClassificacaoEventArgs(classificacao));
    }

    // ================= CONSULTAS =================

    /// Consulta um aluno pelo id e notifica a View
    public void ConsultarAluno(int id)
    {
        // Guardar estado interno — Curry & Grace
        UltimoAlunoConsultado = alunos.FirstOrDefault(a => a.Id == id);
        AlunoConsultado?.Invoke(this, new AlunoEventArgs(UltimoAlunoConsultado));
    }

    /// Consulta uma inscricao e notifica a View
    public void ConsultarInscricao(int id, string edicao)
    {
        var aluno = alunos.FirstOrDefault(a => a.Id == id);
        UltimaInscricaoConsultada = aluno?.ObterInscricao(edicao);
        InscricaoConsultada?.Invoke(this, new InscricaoAlunoConsultadaEventArgs(UltimaInscricaoConsultada));
    }

    /// Consulta uma classificacao e notifica a View
    public void ConsultarClassificacao(int id, string edicao)
    {
        var aluno = alunos.FirstOrDefault(a => a.Id == id);
        var insc = aluno?.ObterInscricao(edicao);
        UltimaClassificacaoConsultada = insc?.ClassificacaoFinal;
        ClassificacaoConsultada?.Invoke(this, new ClassificacaoConsultadaEventArgs(UltimaClassificacaoConsultada));
    }
    
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
        // Validação básica
        if (string.IsNullOrWhiteSpace(nomeAluno))
            throw new ArgumentException("Nome do aluno inválido.");

        if (string.IsNullOrWhiteSpace(curso))
            throw new ArgumentException("Curso invalido.");

        // Simulação de dados (no futuro virá do sistema real)
        Inscricao inscricao = new Inscricao
        {
            NomeAluno = nomeAluno,
            Curso = curso,
            ClassificacaoFinal = 14,
            Estado = "Concluida"
        };

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
}