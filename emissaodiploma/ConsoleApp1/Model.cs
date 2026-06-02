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

/// Interface que representa uma instituição
/// Permite desacoplamento entre componentes
public interface IInstituicao
{
    int IdInstituicao { get; }
    string NomeInstituicao { get; }
    string Cidade { get; }
    string Pais { get; }
}

/// Interface que representa um curso
/// Utilizada para reduzir dependência de classes concretas
public interface ICurso
{
    int IdCurso { get; }
    IInstituicao Instituicao { get; }
    string NomeCurso { get; }
    string GrauAcademico { get; }
    string Descricao { get; }
    string Estrutura { get; }
}

/// Interface que representa uma edição de curso
/// Define apenas os dados necessários à comunicação entre componentes
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

/// Transporta uma Instituicao criada/guardada
public class InstituicaoEventArgs : EventArgs
{
    public IInstituicao Instituicao { get; }

    public InstituicaoEventArgs(IInstituicao instituicao)
    {
        Instituicao = instituicao;
    }
}

/// Transporta um Curso criado
public class CursoEventArgs : EventArgs
{
    public ICurso Curso { get; }

    public CursoEventArgs(ICurso curso)
    {
        Curso = curso;
    }
}

/// Transporta uma Edicao criada ou alterada
public class EdicaoEventArgs : EventArgs
{
    public IEdicao Edicao { get; }

    public EdicaoEventArgs(IEdicao edicao)
    {
        Edicao = edicao;
    }
}

public class InstituicaoConsultadaEventArgs : EventArgs
{
    public IInstituicao? Instituicao { get; }

    public InstituicaoConsultadaEventArgs(IInstituicao? instituicao)
    {
        Instituicao = instituicao;
    }
}

public class CursoConsultadoEventArgs : EventArgs
{
    public ICurso? Curso { get; }

    public CursoConsultadoEventArgs(ICurso? curso)
    {
        Curso = curso;
    }
}

public class EdicaoConsultadaEventArgs : EventArgs
{
    public IEdicao? Edicao { get; }

    public EdicaoConsultadaEventArgs(IEdicao? edicao)
    {
        Edicao = edicao;
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

public interface IModelEventos
{
    event EventHandler<ResultadoEventArgs>? Resultado;

    event EventHandler<InscricaoAlunoEventArgs>? InscricaoCriada;
    event EventHandler<ClassificacaoEventArgs>? ClassificacaoCriada;

    event EventHandler<AlunoEventArgs>? AlunoConsultado;
    event EventHandler<InscricaoAlunoConsultadaEventArgs>? InscricaoConsultada;
    event EventHandler<ClassificacaoConsultadaEventArgs>? ClassificacaoConsultada;
    event EventHandler<InstituicaoEventArgs>? InstituicaoGuardada;
    event EventHandler<CursoEventArgs>? CursoCriado;
    event EventHandler<EdicaoEventArgs>? EdicaoCriada;
    event EventHandler<EdicaoEventArgs>? EstadoEdicaoAlterado;
    event EventHandler<InstituicaoConsultadaEventArgs>? InstituicaoConsultada;
    event EventHandler<CursoConsultadoEventArgs>? CursoConsultado;
    event EventHandler<EdicaoConsultadaEventArgs>? EdicaoConsultada;
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

/// Representa os estados possíveis de uma edição de curso
public enum EstadoEdicao
{
    Planeada,
    Aberta,
    Encerrada,
    Cancelada
}

/// Representa uma instituição de ensino
/// Implementa a interface IInstituicao
public class Instituicao : IInstituicao
{
    public int IdInstituicao { get; set; }
    public string NomeInstituicao { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string Pais { get; set; } = string.Empty;
}

/// Representa um curso académico
/// Implementa a interface ICurso
public class Curso : ICurso
{
    public int IdCurso { get; set; }

    // Associação a uma instituição
    public IInstituicao Instituicao { get; set; } = null!;

    public string NomeCurso { get; set; } = string.Empty;
    public string GrauAcademico { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Estrutura { get; set; } = string.Empty;
}

/// Representa uma edição específica de um curso
/// Implementa a interface IEdicao
public class Edicao : IEdicao
{
    public int IdEdicao { get; set; }

    // Associação ao curso da edição
    public ICurso Curso { get; set; } = null!;

    public string AnoLetivo { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public string Modalidade { get; set; } = string.Empty;

    // Estado inicial da edição
    public EstadoEdicao Estado { get; private set; } = EstadoEdicao.Planeada;

    /// Altera o estado atual da edição
    public void AlterarEstado(EstadoEdicao novoEstado)
    {
        Estado = novoEstado;
    }
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

    private readonly List<Instituicao> instituicoes = new();
    private readonly List<Curso> cursos = new();
    private readonly List<Edicao> edicoes = new();
    public Instituicao? UltimaInstituicaoConsultada { get; private set; }
    public Curso? UltimoCursoConsultado { get; private set; }
    public Edicao? UltimaEdicaoConsultada { get; private set; }

    public Instituicao? UltimaInstituicaoGuardada { get; private set; }
    public Curso? UltimoCursoCriado { get; private set; }
    public Edicao? UltimaEdicaoCriada { get; private set; }
    public Edicao? UltimaEdicaoAlterada { get; private set; }

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

    public event EventHandler<InstituicaoEventArgs>? InstituicaoGuardada;
    public event EventHandler<CursoEventArgs>? CursoCriado;
    public event EventHandler<EdicaoEventArgs>? EdicaoCriada;
    public event EventHandler<EdicaoEventArgs>? EstadoEdicaoAlterado;
    public event EventHandler<InstituicaoConsultadaEventArgs>? InstituicaoConsultada;
    public event EventHandler<CursoConsultadoEventArgs>? CursoConsultado;
    public event EventHandler<EdicaoConsultadaEventArgs>? EdicaoConsultada;

    /// Regista um novo aluno no sistema
    public void RegistarAluno(int id, string nome)
    {
        try
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
        catch (AlunoJaExisteException e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
        catch (ArgumentException e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
        catch (Exception e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = $"Erro inesperado: {e.Message}";
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
    }

    /// Verifica se existe aluno com o id dado
    public bool ExisteAluno(int id)
        => alunos.Any(a => a.Id == id);

    // ================= INSCRICAO =================

    /// Inscreve um aluno numa edicao
    public void InscreverAluno(int alunoId, string edicao)
    {
        try
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
        catch (AlunoNaoEncontradoException e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
        catch (InscricaoAlunoDuplicadaException e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
        catch (ArgumentException e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
        catch (Exception e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = $"Erro inesperado: {e.Message}";
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
    }

    /// Conclui a inscricao de um aluno numa edicao
    public void ConcluirInscricao(int alunoId, string edicao)
    {
        try
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
        catch (AlunoNaoEncontradoException e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
        catch (InscricaoAlunoNaoEncontradaException e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
        catch (InscricaoAlunoJaConcluidaException e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
        catch (Exception e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = $"Erro inesperado: {e.Message}";
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
    }

    // ================= CLASSIFICACAO =================

    /// Lanca a classificacao final de um aluno numa edicao
    public void LancarClassificacao(int alunoId, string edicao, Nota nota)
    {
        try
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
        catch (AlunoNaoEncontradoException e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
        catch (InscricaoAlunoNaoEncontradaException e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
        catch (InscricaoAlunoAindaAtivaException e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
        catch (ClassificacaoJaExisteException e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
        catch (Exception e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = $"Erro inesperado: {e.Message}";
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
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

    // GESTÃO DE INSTITUIÇÕES 
    /// Guarda uma nova instituição no sistema
    /// e notifica a View através de eventos
    public void GuardarInstituicao(int idInstituicao, string nomeInstituicao, string cidade, string pais)
    {
        try
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
        catch (InstituicaoJaExisteException e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
        catch (ArgumentException e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
        catch (Exception e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = $"Erro inesperado: {e.Message}";
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
    }

    public void AlterarInstituicao(int idInstituicao, string nomeInstituicao, string cidade, string pais)
    {
        try
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

            UltimaInstituicaoGuardada = instituicao;
            UltimaOperacaoSucesso = true;
            UltimaMensagem = $"Instituição '{nomeInstituicao}' alterada com sucesso.";

            Resultado?.Invoke(this, new ResultadoEventArgs(true, UltimaMensagem));
            InstituicaoGuardada?.Invoke(this, new InstituicaoEventArgs(instituicao));
        }
        catch (Exception e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
    }

    public void ApagarInstituicao(int idInstituicao)
    {
        try
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
        catch (Exception e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
    }

    // GESTÃO DE CURSOS
    /// Cria um novo curso associado a uma instituição
    /// e comunica o resultado da operação
    public void CriarCurso(int idCurso, int idInstituicao, string nomeCurso, string grauAcademico, string descricao, string estrutura)
    {
        try
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
        catch (CursoJaExisteException e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
        catch (InstituicaoNaoEncontradaException e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
        catch (ArgumentException e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
        catch (Exception e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = $"Erro inesperado: {e.Message}";
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
    }

    public void AlterarCurso(int idCurso, string nomeCurso,
    string grauAcademico, string descricao, string estrutura)
    {
        try
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

            UltimoCursoCriado = curso;
            UltimaOperacaoSucesso = true;
            UltimaMensagem = $"Curso '{nomeCurso}' alterado com sucesso.";

            Resultado?.Invoke(this, new ResultadoEventArgs(true, UltimaMensagem));
            CursoCriado?.Invoke(this, new CursoEventArgs(curso));
        }
        catch (Exception e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
    }

    public void ApagarCurso(int idCurso)
    {
        try
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
        catch (Exception e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
    }

    // GESTÃO DE EDIÇÕES
    /// Cria uma nova edição de curso
    /// e notifica os componentes interessados
    public void CriarEdicao(int idEdicao, int idCurso, string anoLetivo, DateTime dataInicio, DateTime dataFim, string modalidade)
    {
        try
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
        catch (EdicaoJaExisteException e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
        catch (CursoNaoEncontradoException e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
        catch (DataEdicaoInvalidaException e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
        catch (ArgumentException e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
        catch (Exception e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = $"Erro inesperado: {e.Message}";
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
    }
    /// Altera o estado de uma edição existente
    /// mantendo baixo acoplamento entre componentes
    public void AlterarEstadoEdicao(int idEdicao, EstadoEdicao novoEstado)
    {
        try
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
        catch (EdicaoNaoEncontradaException e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
        catch (Exception e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = $"Erro inesperado: {e.Message}";
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
    }

    public void AlterarEdicao(int idEdicao, string anoLetivo,
    DateTime dataInicio, DateTime dataFim, string modalidade)
    {
        try
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
            EdicaoCriada?.Invoke(this, new EdicaoEventArgs(edicao));
        }
        catch (Exception e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
    }

    public void ApagarEdicao(int idEdicao)
    {
        try
        {
            var edicao = edicoes.FirstOrDefault(e => e.IdEdicao == idEdicao)
                ?? throw new EdicaoNaoEncontradaException("A edição indicada não existe.");

            if (alunos.Any(a => a.Inscricoes.Any(i => i.Edicao == idEdicao.ToString())))
                throw new InvalidOperationException("Não é possível apagar a edição porque existem inscrições associadas.");

            edicoes.Remove(edicao);

            UltimaOperacaoSucesso = true;
            UltimaMensagem = "Edição apagada com sucesso.";
            Resultado?.Invoke(this, new ResultadoEventArgs(true, UltimaMensagem));
        }
        catch (Exception e)
        {
            UltimaOperacaoSucesso = false;
            UltimaMensagem = e.Message;
            Resultado?.Invoke(this, new ResultadoEventArgs(false, UltimaMensagem));
        }
    }
    public void ConsultarInstituicao(int idInstituicao)
    {
        UltimaInstituicaoConsultada =
            instituicoes.FirstOrDefault(i => i.IdInstituicao == idInstituicao);

        InstituicaoConsultada?.Invoke(
            this,
            new InstituicaoConsultadaEventArgs(UltimaInstituicaoConsultada));
    }

    public void ConsultarCurso(int idCurso)
    {
        UltimoCursoConsultado =
            cursos.FirstOrDefault(c => c.IdCurso == idCurso);

        CursoConsultado?.Invoke(
            this,
            new CursoConsultadoEventArgs(UltimoCursoConsultado));
    }

    public void ConsultarEdicao(int idEdicao)
    {
        UltimaEdicaoConsultada =
            edicoes.FirstOrDefault(e => e.IdEdicao == idEdicao);

        EdicaoConsultada?.Invoke(
            this,
            new EdicaoConsultadaEventArgs(UltimaEdicaoConsultada));
    }
}
