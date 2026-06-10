using System;
using System.Collections.Generic;
using System.Linq;

// ============================================================
// INTERFACES DE LEITURA
// ============================================================
//
// Estas interfaces expõem apenas os dados necessários à View,
// evitando que os restantes componentes dependam diretamente das
// classes concretas do domínio.

/// <summary>
/// Representa os dados públicos de um aluno.
/// </summary>
public interface IAluno
{
    int Id { get; }
    string Nome { get; }
}

/// <summary>
/// Representa os dados públicos de uma inscrição de aluno.
/// </summary>
public interface IInscricaoAluno
{
    int AlunoId { get; }
    int EdicaoId { get; }
    bool Ativa { get; }
    bool TemClassificacao { get; }
}

/// <summary>
/// Representa os dados públicos de uma classificação.
/// </summary>
public interface IClassificacao
{
    double NotaValor { get; }
    bool Aprovado { get; }
}

/// <summary>
/// Representa os dados públicos de uma instituição.
/// </summary>
public interface IInstituicao
{
    int IdInstituicao { get; }
    string NomeInstituicao { get; }
    string Cidade { get; }
    string Pais { get; }
}

/// <summary>
/// Representa os dados públicos de um curso académico.
/// </summary>
public interface ICurso
{
    int IdCurso { get; }
    IInstituicao Instituicao { get; }
    string NomeCurso { get; }
    string GrauAcademico { get; }
    string Descricao { get; }
    string Estrutura { get; }
}

/// <summary>
/// Representa os dados públicos de uma edição de curso.
/// </summary>
public interface IEdicao
{
    int IdEdicao { get; }
    ICurso Curso { get; }
    string AnoLetivo { get; }
    DateTime DataInicio { get; }
    DateTime DataFim { get; }
    string Modalidade { get; }
    EstadoEdicao Estado { get; }
}

// ============================================================
// EVENT ARGS
// ============================================================

/// <summary>
/// Transporta o resultado genérico de uma operação.
/// 
/// É usado para comunicar mensagens de sucesso ou erro sem expor
/// detalhes internos do Model.
/// </summary>
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

/// <summary>
/// Transporta a inscrição criada pelo Model para a View.
/// </summary>
public class InscricaoAlunoEventArgs : EventArgs
{
    public IInscricaoAluno Inscricao { get; }

    public InscricaoAlunoEventArgs(IInscricaoAluno inscricao)
    {
        Inscricao = inscricao;
    }
}

/// <summary>
/// Transporta a classificação criada pelo Model para a View.
/// </summary>
public class ClassificacaoEventArgs : EventArgs
{
    public IClassificacao Classificacao { get; }

    public ClassificacaoEventArgs(IClassificacao classificacao)
    {
        Classificacao = classificacao;
    }
}

/// <summary>
/// Transporta o resultado da consulta de um aluno.
/// O valor pode ser nulo quando o aluno não é encontrado.
/// </summary>
public class AlunoEventArgs : EventArgs
{
    public IAluno? Aluno { get; }

    public AlunoEventArgs(IAluno? aluno)
    {
        Aluno = aluno;
    }
}

/// <summary>
/// Transporta o resultado da consulta de uma inscrição.
/// O valor pode ser nulo quando a inscrição não é encontrada.
/// </summary>
public class InscricaoAlunoConsultadaEventArgs : EventArgs
{
    public IInscricaoAluno? Inscricao { get; }

    public InscricaoAlunoConsultadaEventArgs(IInscricaoAluno? inscricao)
    {
        Inscricao = inscricao;
    }
}

/// <summary>
/// Transporta o resultado da consulta de uma classificação.
/// O valor pode ser nulo quando a classificação não existe.
/// </summary>
public class ClassificacaoConsultadaEventArgs : EventArgs
{
    public IClassificacao? Classificacao { get; }

    public ClassificacaoConsultadaEventArgs(IClassificacao? classificacao)
    {
        Classificacao = classificacao;
    }
}

/// <summary>
/// Transporta uma instituição criada, alterada ou guardada.
/// </summary>
public class InstituicaoEventArgs : EventArgs
{
    public IInstituicao Instituicao { get; }

    public InstituicaoEventArgs(IInstituicao instituicao)
    {
        Instituicao = instituicao;
    }
}

/// <summary>
/// Transporta um curso criado ou alterado.
/// </summary>
public class CursoEventArgs : EventArgs
{
    public ICurso Curso { get; }

    public CursoEventArgs(ICurso curso)
    {
        Curso = curso;
    }
}

/// <summary>
/// Transporta uma edição criada ou alterada.
/// </summary>
public class EdicaoEventArgs : EventArgs
{
    public IEdicao Edicao { get; }

    public EdicaoEventArgs(IEdicao edicao)
    {
        Edicao = edicao;
    }
}

/// <summary>
/// Transporta o resultado da consulta de uma instituição.
/// </summary>
public class InstituicaoConsultadaEventArgs : EventArgs
{
    public IInstituicao? Instituicao { get; }

    public InstituicaoConsultadaEventArgs(IInstituicao? instituicao)
    {
        Instituicao = instituicao;
    }
}

/// <summary>
/// Transporta o resultado da consulta de um curso.
/// </summary>
public class CursoConsultadoEventArgs : EventArgs
{
    public ICurso? Curso { get; }

    public CursoConsultadoEventArgs(ICurso? curso)
    {
        Curso = curso;
    }
}

/// <summary>
/// Transporta o resultado da consulta de uma edição.
/// </summary>
public class EdicaoConsultadaEventArgs : EventArgs
{
    public IEdicao? Edicao { get; }

    public EdicaoConsultadaEventArgs(IEdicao? edicao)
    {
        Edicao = edicao;
    }
}

/// <summary>
/// Transporta o resultado da validação de elegibilidade para diploma.
/// </summary>
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

/// <summary>
/// Transporta o diploma gerado em formato binário.
/// </summary>
public class DiplomaEmitidoEventArgs : EventArgs
{
    public byte[] PdfBytes { get; }

    public DiplomaEmitidoEventArgs(byte[] pdfBytes)
    {
        PdfBytes = pdfBytes;
    }
}

// ============================================================
// INTERFACE DE EVENTOS DO MODEL
// ============================================================

/// <summary>
/// Define os eventos públicos disponibilizados pelo Model.
/// 
/// Permite que a View observe alterações e resultados sem conhecer
/// a implementação concreta do Model.
/// </summary>
public interface IModelEventos
{
    event EventHandler<ResultadoEventArgs>? Resultado;

    event EventHandler<InscricaoAlunoEventArgs>? InscricaoCriada;
    event EventHandler<ClassificacaoEventArgs>? ClassificacaoCriada;

    event EventHandler<AlunoEventArgs>? AlunoConsultado;
    event EventHandler<InscricaoAlunoConsultadaEventArgs>? InscricaoConsultada;
    event EventHandler<ClassificacaoConsultadaEventArgs>? ClassificacaoConsultada;

    event EventHandler<InstituicaoEventArgs>? InstituicaoGuardada;
    event EventHandler<InstituicaoEventArgs>? InstituicaoAlterada;

    event EventHandler<CursoEventArgs>? CursoCriado;
    event EventHandler<CursoEventArgs>? CursoAlterado;

    event EventHandler<EdicaoEventArgs>? EdicaoCriada;
    event EventHandler<EdicaoEventArgs>? EdicaoAlterada;
    event EventHandler<EdicaoEventArgs>? EstadoEdicaoAlterado;

    event EventHandler<InstituicaoConsultadaEventArgs>? InstituicaoConsultada;
    event EventHandler<CursoConsultadoEventArgs>? CursoConsultado;
    event EventHandler<EdicaoConsultadaEventArgs>? EdicaoConsultada;

    event EventHandler<ValidacaoEventArgs>? OnValidacao;
    event EventHandler<DiplomaEmitidoEventArgs>? OnDiplomaEmitido;
}

// ============================================================
// CLASSES DE DOMÍNIO
// ============================================================
//
// Estas classes representam as principais entidades da aplicação:
// aluno, inscrição, classificação, instituição, curso e edição.
//
// Aqui ficam concentrados os dados e algumas regras simples
// diretamente associadas às entidades.

/// <summary>
/// Representa um aluno do sistema.
/// </summary>
public class Aluno : IAluno
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    // Relacao 1:N — um aluno pode ter varias inscricoes
    public List<InscricaoAluno> Inscricoes { get; set; } = new();

    /// <summary>
    /// Verifica se o aluno já tem uma inscrição ativa numa determinada edição.
    /// </summary>
    public bool TemInscricaoAtiva(int edicaoId)
    => Inscricoes.Any(i => i.EdicaoId == edicaoId && i.Ativa);

    /// <summary>
    /// Obtém a inscrição do aluno numa determinada edição.
    /// </summary>
    public InscricaoAluno? ObterInscricao(int edicaoId)
    => Inscricoes.FirstOrDefault(i => i.EdicaoId == edicaoId);
}

/// <summary>
/// Representa a inscrição de um aluno numa edição.
/// </summary>
public class InscricaoAluno : IInscricaoAluno
{
    public int AlunoId { get; set; }
    public int EdicaoId { get; set; }

    // Regra: fica false quando a inscricao e concluida
    public bool Ativa { get; set; } = true;

    // Regra: so pode existir UMA classificacao por inscricao
    public Classificacao? ClassificacaoFinal { get; set; }

    public bool Concluida => !Ativa;
    public bool TemClassificacao => ClassificacaoFinal != null;
}

/// <summary>
/// Representa a classificação final associada a uma inscrição.
/// </summary>
public class Classificacao : IClassificacao
{
    public InscricaoAluno Inscricao { get; set; } = null!;
    public Nota Nota { get; set; } = null!;

    // Regra: aprovado se nota >= 10
    public bool Aprovado => Nota.Valor >= 10;

    // propriedade usada pela interface
    public double NotaValor => Nota.Valor;
}

/// <summary>
/// Value Object que representa uma nota académica válida.
/// Garante que a nota fica sempre no intervalo permitido: 0 a 20.
/// </summary>
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

/// <summary>
/// Representa os estados possíveis de uma edição de curso.
/// </summary>
public enum EstadoEdicao
{
    Planeada,
    Aberta,
    Encerrada,
    Cancelada
}

/// <summary>
/// Representa uma instituição de ensino.
/// </summary>
public class Instituicao : IInstituicao
{
    public int IdInstituicao { get; set; }
    public string NomeInstituicao { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string Pais { get; set; } = string.Empty;
}

/// <summary>
/// Representa um curso académico associado a uma instituição.
/// </summary>
public class Curso : ICurso
{
    public int IdCurso { get; set; }

    public IInstituicao Instituicao { get; set; } = null!;

    public string NomeCurso { get; set; } = string.Empty;
    public string GrauAcademico { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Estrutura { get; set; } = string.Empty;
}

/// <summary>
/// Representa uma edição específica de um curso.
/// </summary>
public class Edicao : IEdicao
{
    public int IdEdicao { get; set; }

    public ICurso Curso { get; set; } = null!;

    public string AnoLetivo { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public string Modalidade { get; set; } = string.Empty;

    public EstadoEdicao Estado { get; private set; } = EstadoEdicao.Planeada;

    /// <summary>
    /// Altera o estado atual da edição.
    /// </summary>
    public void AlterarEstado(EstadoEdicao novoEstado)
    {
        Estado = novoEstado;
    }
}

// ============================================================
// EXCEÇÕES DE NEGÓCIO
// ============================================================
//
// Estas exceções representam erros específicos da lógica da aplicação.
// Permitem distinguir falhas de validação, entidades inexistentes,
// duplicações e estados inválidos.
//
// O Model lança estas exceções quando deteta erros de negócio.
// O Controller é responsável por capturá-las e pedir à View que apresente a mensagem.

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

public class InstituicaoJaExisteException : Exception
{
    public InstituicaoJaExisteException(string msg) : base(msg) { }
}

public class InstituicaoNaoEncontradaException : Exception
{
    public InstituicaoNaoEncontradaException(string msg) : base(msg) { }
}

public class CursoJaExisteException : Exception
{
    public CursoJaExisteException(string msg) : base(msg) { }
}

public class CursoNaoEncontradoException : Exception
{
    public CursoNaoEncontradoException(string msg) : base(msg) { }
}

public class EdicaoJaExisteException : Exception
{
    public EdicaoJaExisteException(string msg) : base(msg) { }
}

public class EdicaoNaoEncontradaException : Exception
{
    public EdicaoNaoEncontradaException(string msg) : base(msg) { }
}

public class DataEdicaoInvalidaException : Exception
{
    public DataEdicaoInvalidaException(string msg) : base(msg) { }
}

// ============================================================
// MODEL
// ============================================================

/// <summary>
/// Model da aplicação SimDiploma.
/// 
/// Responsabilidades:
/// - Guardar o estado interno da aplicação;
/// - Aplicar as regras de negócio;
/// - Validar dados recebidos do Controller;
/// - Coordenar a emissão de diplomas;
/// - Notificar a View através de eventos.
/// 
/// O Model não lê dados do utilizador e não escreve diretamente no console.
/// Também não conhece os detalhes concretos da geração do diploma, dependendo
/// apenas da interface IGeradorDiploma.
/// </summary>
public class Model : IModelEventos
{
    private List<Aluno> alunos = new();

    private readonly List<Instituicao> instituicoes = new();
    private readonly List<Curso> cursos = new();
    private readonly List<Edicao> edicoes = new();

    private readonly IGeradorDiploma _gerador;

    public Instituicao? UltimaInstituicaoConsultada { get; private set; }
    public Curso? UltimoCursoConsultado { get; private set; }
    public Edicao? UltimaEdicaoConsultada { get; private set; }

    public Instituicao? UltimaInstituicaoGuardada { get; private set; }
    public Instituicao? UltimaInstituicaoAlterada { get; private set; }

    public Curso? UltimoCursoCriado { get; private set; }
    public Curso? UltimoCursoAlterado { get; private set; }

    public Edicao? UltimaEdicaoCriada { get; private set; }
    public Edicao? UltimaEdicaoAlterada { get; private set; }

    public Aluno? UltimoAlunoConsultado { get; private set; }
    public InscricaoAluno? UltimaInscricaoConsultada { get; private set; }
    public Classificacao? UltimaClassificacaoConsultada { get; private set; }

    public bool UltimaOperacaoSucesso { get; private set; }
    public string UltimaMensagem { get; private set; } = string.Empty;

    public byte[]? UltimoDiploma { get; private set; }
    public bool UltimaValidacaoSucesso { get; private set; }

    // ============================================================
    // EVENTOS DO MODEL
    // ============================================================
    //
    // Os eventos permitem que o Model comunique resultados à View
    // sem depender diretamente dela. Esta comunicação é essencial
    // para manter a separação entre lógica de negócio e apresentação.

    public event EventHandler<ResultadoEventArgs>? Resultado;

    public event EventHandler<InscricaoAlunoEventArgs>? InscricaoCriada;
    public event EventHandler<ClassificacaoEventArgs>? ClassificacaoCriada;

    public event EventHandler<AlunoEventArgs>? AlunoConsultado;
    public event EventHandler<InscricaoAlunoConsultadaEventArgs>? InscricaoConsultada;
    public event EventHandler<ClassificacaoConsultadaEventArgs>? ClassificacaoConsultada;

    public event EventHandler<InstituicaoEventArgs>? InstituicaoGuardada;
    public event EventHandler<InstituicaoEventArgs>? InstituicaoAlterada;

    public event EventHandler<CursoEventArgs>? CursoCriado;
    public event EventHandler<CursoEventArgs>? CursoAlterado;

    public event EventHandler<EdicaoEventArgs>? EdicaoCriada;
    public event EventHandler<EdicaoEventArgs>? EdicaoAlterada;
    public event EventHandler<EdicaoEventArgs>? EstadoEdicaoAlterado;

    public event EventHandler<InstituicaoConsultadaEventArgs>? InstituicaoConsultada;
    public event EventHandler<CursoConsultadoEventArgs>? CursoConsultado;
    public event EventHandler<EdicaoConsultadaEventArgs>? EdicaoConsultada;

    public event EventHandler<ValidacaoEventArgs>? OnValidacao;
    public event EventHandler<DiplomaEmitidoEventArgs>? OnDiplomaEmitido;

    /// <summary>
    /// Cria uma nova instância do Model.
    /// 
    /// A geração do diploma é recebida por interface, permitindo trocar
    /// a implementação concreta sem alterar o Model.
    /// </summary>
    public Model(IGeradorDiploma gerador)
    {
        _gerador = gerador ?? throw new ArgumentNullException(nameof(gerador));
    }

    // ============================================================
    // ALUNOS
    // ============================================================

    /// <summary>
    /// Regista um novo aluno no sistema, garantindo que o ID é válido
    /// e que não existe outro aluno com o mesmo identificador.
    /// </summary>
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

    /// <summary>
    /// Verifica se existe um aluno com o identificador indicado.
    /// </summary>
    public bool ExisteAluno(int id)
        => alunos.Any(a => a.Id == id);

    // ============================================================
    // INSCRIÇÕES
    // ============================================================

    /// <summary>
    /// Inscreve um aluno numa edição, garantindo que o aluno existe
    /// e que não tem já uma inscrição ativa nessa edição.
    /// </summary>
    public void InscreverAluno(int alunoId, int edicaoId)
    {
    var aluno = alunos.FirstOrDefault(a => a.Id == alunoId)
        ?? throw new AlunoNaoEncontradoException($"Aluno com ID {alunoId} não existe.");

    var edicao = edicoes.FirstOrDefault(e => e.IdEdicao == edicaoId)
        ?? throw new EdicaoNaoEncontradaException($"Edição com ID {edicaoId} não existe.");

    if (aluno.TemInscricaoAtiva(edicaoId))
        throw new InscricaoAlunoDuplicadaException($"Já existe uma inscrição ativa na edição '{edicaoId}'.");

    var inscricao = new InscricaoAluno
    {
        AlunoId = alunoId,
        EdicaoId = edicaoId,
        Ativa = true
    };

    aluno.Inscricoes.Add(inscricao);

    UltimaInscricaoConsultada = inscricao;
    UltimaOperacaoSucesso = true;
    UltimaMensagem = $"Inscrição na edição '{edicao.AnoLetivo}' realizada com sucesso.";

    Resultado?.Invoke(this, new ResultadoEventArgs(true, UltimaMensagem));
    InscricaoCriada?.Invoke(this, new InscricaoAlunoEventArgs(inscricao));
    }

    /// <summary>
    /// Conclui a inscrição de um aluno numa edição.
    /// </summary>
    public void ConcluirInscricao(int alunoId, int edicaoId)
    {
        var aluno = alunos.FirstOrDefault(a => a.Id == alunoId)
            ?? throw new AlunoNaoEncontradoException($"Aluno com ID {alunoId} nao existe.");

        var inscricao = aluno.ObterInscricao(edicaoId)
            ?? throw new InscricaoAlunoNaoEncontradaException($"Inscricao na edicao '{edicaoId}' nao encontrada.");

        if (!inscricao.Ativa)
            throw new InscricaoAlunoJaConcluidaException("A inscricao ja se encontra concluida.");

        inscricao.Ativa = false;

        UltimaOperacaoSucesso = true;
        UltimaMensagem = $"Inscricao na edicao '{edicaoId}' concluida com sucesso.";

        Resultado?.Invoke(this, new ResultadoEventArgs(true, UltimaMensagem));
    }

    // ============================================================
    // CLASSIFICAÇÕES
    // ============================================================

    /// <summary>
    /// Lança a classificação final de um aluno numa edição.
    /// 
    /// A classificação só pode ser lançada se a inscrição existir,
    /// estiver concluída e ainda não tiver classificação associada.
    /// </summary>
    public void LancarClassificacao(int alunoId, int edicaoId, Nota nota)
    {
        var aluno = alunos.FirstOrDefault(a => a.Id == alunoId)
            ?? throw new AlunoNaoEncontradoException($"Aluno com ID {alunoId} nao existe.");

        var inscricao = aluno.ObterInscricao(edicaoId)
            ?? throw new InscricaoAlunoNaoEncontradaException($"Inscricao na edicao '{edicaoId}' nao encontrada.");

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

        UltimaClassificacaoConsultada = classificacao;
        UltimaOperacaoSucesso = true;
        UltimaMensagem = $"Classificacao lancada: {nota.Valor} | Aprovado: {classificacao.Aprovado}";
    
        Resultado?.Invoke(this, new ResultadoEventArgs(true, UltimaMensagem));
        ClassificacaoCriada?.Invoke(this, new ClassificacaoEventArgs(classificacao));
    }

    // ============================================================
    // CONSULTAS DE ALUNOS, INSCRIÇÕES E CLASSIFICAÇÕES
    // ============================================================

    /// <summary>
    /// Consulta um aluno pelo ID e notifica a View.
    /// </summary>
    public void ConsultarAluno(int id)
    {
        UltimoAlunoConsultado = alunos.FirstOrDefault(a => a.Id == id);
        AlunoConsultado?.Invoke(this, new AlunoEventArgs(UltimoAlunoConsultado));
    }

    /// <summary>
    /// Consulta uma inscrição e notifica a View.
    /// </summary>
    public void ConsultarInscricao(int id, int edicaoId)
    {
        var aluno = alunos.FirstOrDefault(a => a.Id == id);
        UltimaInscricaoConsultada = aluno?.ObterInscricao(edicaoId);
        InscricaoConsultada?.Invoke(this, new InscricaoAlunoConsultadaEventArgs(UltimaInscricaoConsultada));
    }

    /// <summary>
    /// Consulta uma classificação e notifica a View.
    /// </summary>
    public void ConsultarClassificacao(int id, int edicaoId)
    {
        var aluno = alunos.FirstOrDefault(a => a.Id == id);
        var insc = aluno?.ObterInscricao(edicaoId);
        UltimaClassificacaoConsultada = insc?.ClassificacaoFinal;
        ClassificacaoConsultada?.Invoke(this, new ClassificacaoConsultadaEventArgs(UltimaClassificacaoConsultada));
    }

    // ============================================================
    // GESTÃO DE INSTITUIÇÕES
    // ============================================================

    /// <summary>
    /// Guarda uma nova instituição no sistema e notifica a View.
    /// </summary>
    public void GuardarInstituicao(int idInstituicao, string nomeInstituicao, string cidade, string pais)
    {
        if (idInstituicao <= 0)
            throw new ArgumentException("ID da instituição inválido.");

        if (string.IsNullOrWhiteSpace(nomeInstituicao))
            throw new ArgumentException("Nome da instituição inválido.");

        if (string.IsNullOrWhiteSpace(cidade))
            throw new ArgumentException("Cidade inválida.");

        if (string.IsNullOrWhiteSpace(pais))
            throw new ArgumentException("País inválido.");

        if (instituicoes.Any(i => i.IdInstituicao == idInstituicao))
            throw new InstituicaoJaExisteException($"Já existe uma instituição com o ID {idInstituicao}.");

        var instituicao = new Instituicao
        {
            IdInstituicao = idInstituicao,
            NomeInstituicao = nomeInstituicao,
            Cidade = cidade,
            Pais = pais
        };

        instituicoes.Add(instituicao);

        UltimaInstituicaoGuardada = instituicao;
        UltimaOperacaoSucesso = true;
        UltimaMensagem = $"Instituição '{nomeInstituicao}' guardada com sucesso.";

        Resultado?.Invoke(this, new ResultadoEventArgs(true, UltimaMensagem));
        InstituicaoGuardada?.Invoke(this, new InstituicaoEventArgs(instituicao));
    }

    /// <summary>
    /// Altera os dados de uma instituição existente.
    /// </summary>
    public void AlterarInstituicao(int idInstituicao, string nomeInstituicao, string cidade, string pais)
    {
        var instituicao = instituicoes.FirstOrDefault(i => i.IdInstituicao == idInstituicao)
            ?? throw new InstituicaoNaoEncontradaException("A instituição indicada não existe.");

        if (string.IsNullOrWhiteSpace(nomeInstituicao))
            throw new ArgumentException("Nome da instituição inválido.");

        if (string.IsNullOrWhiteSpace(cidade))
            throw new ArgumentException("Cidade inválida.");

        if (string.IsNullOrWhiteSpace(pais))
            throw new ArgumentException("País inválido.");

        instituicao.NomeInstituicao = nomeInstituicao;
        instituicao.Cidade = cidade;
        instituicao.Pais = pais;

        UltimaInstituicaoAlterada = instituicao;
        UltimaOperacaoSucesso = true;
        UltimaMensagem = $"Instituição '{nomeInstituicao}' alterada com sucesso.";

        Resultado?.Invoke(this, new ResultadoEventArgs(true, UltimaMensagem));
        InstituicaoAlterada?.Invoke(this, new InstituicaoEventArgs(instituicao));
    }

    /// <summary>
    /// Apaga uma instituição, desde que não existam cursos associados.
    /// </summary>
    public void ApagarInstituicao(int idInstituicao)
    {
        var instituicao = instituicoes.FirstOrDefault(i => i.IdInstituicao == idInstituicao)
            ?? throw new InstituicaoNaoEncontradaException("A instituição indicada não existe.");

        if (cursos.Any(c => c.Instituicao.IdInstituicao == idInstituicao))
            throw new InvalidOperationException("Não é possível apagar a instituição porque existem cursos associados.");

        instituicoes.Remove(instituicao);

        UltimaOperacaoSucesso = true;
        UltimaMensagem = "Instituição apagada com sucesso.";

        Resultado?.Invoke(this, new ResultadoEventArgs(true, UltimaMensagem));
    }

    /// <summary>
    /// Consulta uma instituição pelo ID e notifica a View.
    /// </summary>
    public void ConsultarInstituicao(int idInstituicao)
    {
        UltimaInstituicaoConsultada =
            instituicoes.FirstOrDefault(i => i.IdInstituicao == idInstituicao);

        InstituicaoConsultada?.Invoke(
            this,
            new InstituicaoConsultadaEventArgs(UltimaInstituicaoConsultada));
    }

    // ============================================================
    // GESTÃO DE CURSOS
    // ============================================================

    /// <summary>
    /// Cria um novo curso associado a uma instituição existente.
    /// </summary>
    public void CriarCurso(int idCurso, int idInstituicao, string nomeCurso, string grauAcademico, string descricao, string estrutura)
    {
        if (idCurso <= 0)
            throw new ArgumentException("ID do curso inválido.");

        if (string.IsNullOrWhiteSpace(nomeCurso))
            throw new ArgumentException("Nome do curso inválido.");

        if (string.IsNullOrWhiteSpace(grauAcademico))
            throw new ArgumentException("Grau académico inválido.");

        if (cursos.Any(c => c.IdCurso == idCurso))
            throw new CursoJaExisteException($"Já existe um curso com o ID {idCurso}.");

        var instituicao = instituicoes.FirstOrDefault(i => i.IdInstituicao == idInstituicao)
            ?? throw new InstituicaoNaoEncontradaException("A instituição indicada não existe.");

        var curso = new Curso
        {
            IdCurso = idCurso,
            Instituicao = instituicao,
            NomeCurso = nomeCurso,
            GrauAcademico = grauAcademico,
            Descricao = descricao,
            Estrutura = estrutura
        };

        cursos.Add(curso);

        UltimoCursoCriado = curso;
        UltimaOperacaoSucesso = true;
        UltimaMensagem = $"Curso '{nomeCurso}' criado com sucesso.";

        Resultado?.Invoke(this, new ResultadoEventArgs(true, UltimaMensagem));
        CursoCriado?.Invoke(this, new CursoEventArgs(curso));
    }

    /// <summary>
    /// Altera os dados de um curso existente.
    /// </summary>
    public void AlterarCurso(int idCurso, string nomeCurso,
    string grauAcademico, string descricao, string estrutura)
    {
        var curso = cursos.FirstOrDefault(c => c.IdCurso == idCurso)
            ?? throw new CursoNaoEncontradoException("O curso indicado não existe.");

        if (string.IsNullOrWhiteSpace(nomeCurso))
            throw new ArgumentException("Nome do curso inválido.");

        if (string.IsNullOrWhiteSpace(grauAcademico))
            throw new ArgumentException("Grau académico inválido.");

        curso.NomeCurso = nomeCurso;
        curso.GrauAcademico = grauAcademico;
        curso.Descricao = descricao;
        curso.Estrutura = estrutura;

        UltimoCursoAlterado = curso;
        UltimaOperacaoSucesso = true;
        UltimaMensagem = $"Curso '{nomeCurso}' alterado com sucesso.";

        Resultado?.Invoke(this, new ResultadoEventArgs(true, UltimaMensagem));
        CursoAlterado?.Invoke(this, new CursoEventArgs(curso));
    }

    /// <summary>
    /// Apaga um curso, desde que não existam edições associadas.
    /// </summary>
    public void ApagarCurso(int idCurso)
    {
        var curso = cursos.FirstOrDefault(c => c.IdCurso == idCurso)
            ?? throw new CursoNaoEncontradoException("O curso indicado não existe.");

        if (edicoes.Any(e => e.Curso.IdCurso == idCurso))
            throw new InvalidOperationException("Não é possível apagar o curso porque existem edições associadas.");

        cursos.Remove(curso);

        UltimaOperacaoSucesso = true;
        UltimaMensagem = "Curso apagado com sucesso.";

        Resultado?.Invoke(this, new ResultadoEventArgs(true, UltimaMensagem));
    }

    /// <summary>
    /// Consulta um curso pelo ID e notifica a View.
    /// </summary>
    public void ConsultarCurso(int idCurso)
    {
        UltimoCursoConsultado =
            cursos.FirstOrDefault(c => c.IdCurso == idCurso);

        CursoConsultado?.Invoke(
            this,
            new CursoConsultadoEventArgs(UltimoCursoConsultado));
    }

    // ============================================================
    // GESTÃO DE EDIÇÕES
    // ============================================================

    /// <summary>
    /// Cria uma nova edição de curso.
    /// </summary>
    public void CriarEdicao(int idEdicao, int idCurso, string anoLetivo, DateTime dataInicio, DateTime dataFim, string modalidade)
    {
        if (idEdicao <= 0)
            throw new ArgumentException("ID da edição inválido.");

        if (string.IsNullOrWhiteSpace(anoLetivo))
            throw new ArgumentException("Ano letivo inválido.");

        if (string.IsNullOrWhiteSpace(modalidade))
            throw new ArgumentException("Modalidade inválida.");

        if (dataFim <= dataInicio)
            throw new DataEdicaoInvalidaException("A data de fim deve ser posterior à data de início.");

        if (edicoes.Any(e => e.IdEdicao == idEdicao))
            throw new EdicaoJaExisteException($"Já existe uma edição com o ID {idEdicao}.");

        var curso = cursos.FirstOrDefault(c => c.IdCurso == idCurso)
            ?? throw new CursoNaoEncontradoException("O curso indicado não existe.");

        var edicao = new Edicao
        {
            IdEdicao = idEdicao,
            Curso = curso,
            AnoLetivo = anoLetivo,
            DataInicio = dataInicio,
            DataFim = dataFim,
            Modalidade = modalidade
        };

        edicoes.Add(edicao);

        UltimaEdicaoCriada = edicao;
        UltimaOperacaoSucesso = true;
        UltimaMensagem = $"Edição '{idEdicao}' criada com sucesso.";

        Resultado?.Invoke(this, new ResultadoEventArgs(true, UltimaMensagem));
        EdicaoCriada?.Invoke(this, new EdicaoEventArgs(edicao));
    }

    /// <summary>
    /// Altera os dados de uma edição existente.
    /// </summary>
    public void AlterarEdicao(
    int idEdicao,
    string anoLetivo,
    DateTime dataInicio,
    DateTime dataFim,
    string modalidade)
    {
        var edicao = edicoes.FirstOrDefault(e => e.IdEdicao == idEdicao)
            ?? throw new EdicaoNaoEncontradaException("A edição indicada não existe.");

        if (string.IsNullOrWhiteSpace(anoLetivo))
            throw new ArgumentException("Ano letivo inválido.");

        if (string.IsNullOrWhiteSpace(modalidade))
            throw new ArgumentException("Modalidade inválida.");

        if (dataFim <= dataInicio)
            throw new DataEdicaoInvalidaException("A data de fim deve ser posterior à data de início.");

        edicao.AnoLetivo = anoLetivo;
        edicao.DataInicio = dataInicio;
        edicao.DataFim = dataFim;
        edicao.Modalidade = modalidade;

        UltimaEdicaoAlterada = edicao;
        UltimaOperacaoSucesso = true;
        UltimaMensagem = $"Edição '{idEdicao}' alterada com sucesso.";

        Resultado?.Invoke(this, new ResultadoEventArgs(true, UltimaMensagem));
        EdicaoAlterada?.Invoke(this, new EdicaoEventArgs(edicao));
    }

    /// <summary>
    /// Altera o estado de uma edição existente.
    /// </summary>
    public void AlterarEstadoEdicao(int idEdicao, EstadoEdicao novoEstado)
    {
        var edicao = edicoes.FirstOrDefault(e => e.IdEdicao == idEdicao)
            ?? throw new EdicaoNaoEncontradaException("A edição indicada não existe.");

        edicao.AlterarEstado(novoEstado);

        UltimaEdicaoAlterada = edicao;
        UltimaOperacaoSucesso = true;
        UltimaMensagem = $"Estado da edição '{idEdicao}' alterado para '{novoEstado}'.";

        Resultado?.Invoke(this, new ResultadoEventArgs(true, UltimaMensagem));
        EstadoEdicaoAlterado?.Invoke(this, new EdicaoEventArgs(edicao));
    }

    /// <summary>
    /// Apaga uma edição, desde que não existam inscrições associadas.
    /// </summary>
    public void ApagarEdicao(int idEdicao)
    {
        var edicao = edicoes.FirstOrDefault(e => e.IdEdicao == idEdicao)
            ?? throw new EdicaoNaoEncontradaException("A edição indicada não existe.");

        if (alunos.Any(a => a.Inscricoes.Any(i => i.EdicaoId == idEdicao)))
        throw new InvalidOperationException("Não é possível apagar a edição porque existem inscrições associadas.");

        edicoes.Remove(edicao);

        UltimaOperacaoSucesso = true;
        UltimaMensagem = "Edição apagada com sucesso.";

        Resultado?.Invoke(this, new ResultadoEventArgs(true, UltimaMensagem));
    }
    /// <summary>
    /// Consulta uma edição pelo ID e notifica a View.
    /// </summary>
    public void ConsultarEdicao(int idEdicao)
    {
        UltimaEdicaoConsultada =
            edicoes.FirstOrDefault(e => e.IdEdicao == idEdicao);

        EdicaoConsultada?.Invoke(
            this,
            new EdicaoConsultadaEventArgs(UltimaEdicaoConsultada));
    }

    // ============================================================
    // EMISSÃO DE DIPLOMAS
    // ============================================================

    /// <summary>
    /// Valida se uma inscrição real reúne as condições necessárias
    /// para emissão de diploma.
    /// </summary>
    private void ValidarElegibilidade(InscricaoAluno inscricao)
    {
        if (inscricao == null)
            throw new InscricaoInvalidaException();

        if (inscricao.Ativa)
            throw new InscricaoNaoConcluidaException();

        if (inscricao.ClassificacaoFinal == null)
            throw new ClassificacaoInvalidaException();

        if (!inscricao.ClassificacaoFinal.Aprovado)
            throw new SemAproveitamentoException();
    }

    /// <summary>
    /// Executa o processo de emissão de diploma.
    /// 
    /// O método recebe o ID do aluno e o ID da edição, procura os dados reais
    /// no Model, valida a elegibilidade da inscrição, delega a geração do
    /// diploma no serviço IGeradorDiploma e notifica a View através de eventos.
    /// </summary>
    public void EmitirDiploma(int alunoId, int idEdicao)
    {
        if (alunoId <= 0)
            throw new ArgumentException("ID do aluno inválido.");

        if (idEdicao <= 0)
            throw new ArgumentException("ID da edição inválido.");

        var aluno = alunos.FirstOrDefault(a => a.Id == alunoId)
            ?? throw new AlunoNaoEncontradoException($"Aluno com ID {alunoId} não encontrado.");

        var edicao = edicoes.FirstOrDefault(e => e.IdEdicao == idEdicao)
            ?? throw new EdicaoNaoEncontradaException($"Edição com ID {idEdicao} não encontrada.");

        var inscricao = aluno.Inscricoes
            .FirstOrDefault(i => i.EdicaoId == idEdicao)
            ?? throw new InscricaoAlunoNaoEncontradaException(
                $"O aluno com ID {alunoId} não tem inscrição na edição {idEdicao}.");

        ValidarElegibilidade(inscricao);

        UltimaValidacaoSucesso = true;

        OnValidacao?.Invoke(
            this,
            new ValidacaoEventArgs(true, "Validação concluída com sucesso."));

        string nomeAluno = aluno.Nome;
        string nomeCurso = edicao.Curso.NomeCurso;
        string nomeInstituicao = edicao.Curso.Instituicao.NomeInstituicao;

        byte[] pdf = _gerador.Gerar(nomeAluno, nomeCurso, nomeInstituicao);

        if (pdf == null || pdf.Length == 0)
            throw new InvalidOperationException("O diploma não foi gerado corretamente.");

        UltimoDiploma = pdf;

        UltimaOperacaoSucesso = true;
        UltimaMensagem = "Diploma emitido com sucesso.";

        Resultado?.Invoke(
            this,
            new ResultadoEventArgs(true, UltimaMensagem));

        OnDiplomaEmitido?.Invoke(
            this,
            new DiplomaEmitidoEventArgs(pdf));
    }
}