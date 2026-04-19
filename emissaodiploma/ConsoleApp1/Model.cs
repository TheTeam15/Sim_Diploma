using System;

namespace SimDiplomaMVC
{
    /// <summary>
    /// Classe que transporta o resultado da validação.
    /// Usada para comunicar informação do Model para a View.
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
    /// Classe que transporta o diploma gerado (em bytes).
    /// </summary>
    public class DiplomaEmitidoEventArgs : EventArgs
    {
        public byte[] PdfBytes { get; }

        public DiplomaEmitidoEventArgs(byte[] pdfBytes)
        {
            PdfBytes = pdfBytes;
        }
    }

    /// <summary>
    /// Classe que transporta os dados da instituição.
    /// </summary>
    public class InstituicaoGuardadaEventArgs : EventArgs
    {
        public string NomeInstituicao { get; }
        public string DesignacaoDiploma { get; }
        public string Localidade { get; }

        public InstituicaoGuardadaEventArgs(string nomeInstituicao, string designacaoDiploma, string localidade)
        {
            NomeInstituicao = nomeInstituicao;
            DesignacaoDiploma = designacaoDiploma;
            Localidade = localidade;
        }
    }

    /// <summary>
    /// Classe que transporta os dados de um curso criado.
    /// </summary>
    public class CursoCriadoEventArgs : EventArgs
    {
        public string CodigoCurso { get; }

        public CursoCriadoEventArgs(string codigoCurso)
        {
            CodigoCurso = codigoCurso;
        }
    }

    /// <summary>
    /// Classe que transporta os dados de uma edição criada.
    /// </summary>
    public class EdicaoCriadaEventArgs : EventArgs
    {
        public string CodigoCurso { get; }
        public string CodigoEdicao { get; }

        public EdicaoCriadaEventArgs(string codigoCurso, string codigoEdicao)
        {
            CodigoCurso = codigoCurso;
            CodigoEdicao = codigoEdicao;
        }
    }

    /// <summary>
    /// Classe que transporta a alteração de estado de uma edição.
    /// </summary>
    public class EstadoEdicaoAlteradoEventArgs : EventArgs
    {
        public string CodigoCurso { get; }
        public string CodigoEdicao { get; }
        public string NovoEstado { get; }

        public EstadoEdicaoAlteradoEventArgs(string codigoCurso, string codigoEdicao, string novoEstado)
        {
            CodigoCurso = codigoCurso;
            CodigoEdicao = codigoEdicao;
            NovoEstado = novoEstado;
        }
    }

    /// <summary>
    /// Classe que transporta os dados de um aluno registado.
    /// </summary>
    public class AlunoRegistadoEventArgs : EventArgs
    {
        public int AlunoId { get; }
        public string NomeAluno { get; }

        public AlunoRegistadoEventArgs(int alunoId, string nomeAluno)
        {
            AlunoId = alunoId;
            NomeAluno = nomeAluno;
        }
    }

    /// <summary>
    /// Classe que transporta os dados de uma inscrição criada.
    /// </summary>
    public class InscricaoCriadaEventArgs : EventArgs
    {
        public int AlunoId { get; }
        public string CodigoCurso { get; }
        public string CodigoEdicao { get; }

        public InscricaoCriadaEventArgs(int alunoId, string codigoCurso, string codigoEdicao)
        {
            AlunoId = alunoId;
            CodigoCurso = codigoCurso;
            CodigoEdicao = codigoEdicao;
        }
    }

    /// <summary>
    /// Classe que transporta os dados de uma classificação lançada.
    /// </summary>
    public class ClassificacaoLancadaEventArgs : EventArgs
    {
        public int AlunoId { get; }
        public string CodigoCurso { get; }
        public string CodigoEdicao { get; }
        public decimal ClassificacaoFinal { get; }

        public ClassificacaoLancadaEventArgs(int alunoId, string codigoCurso, string codigoEdicao, decimal classificacaoFinal)
        {
            AlunoId = alunoId;
            CodigoCurso = codigoCurso;
            CodigoEdicao = codigoEdicao;
            ClassificacaoFinal = classificacaoFinal;
        }
    }

    /// <summary>
    /// Classe que transporta o resultado de uma consulta.
    /// </summary>
    public class ConsultaRealizadaEventArgs : EventArgs
    {
        public string TipoConsulta { get; }

        public ConsultaRealizadaEventArgs(string tipoConsulta)
        {
            TipoConsulta = tipoConsulta;
        }
    }

    // Interface que expõe apenas os eventos do Model.
    // A View subscreve isto — nunca a classe concreta.
    public interface IModelEventos
    {
        event EventHandler<ValidacaoEventArgs> OnValidacao;
        event EventHandler<DiplomaEmitidoEventArgs> OnDiplomaEmitido;
        event EventHandler<InstituicaoGuardadaEventArgs> OnInstituicaoGuardada;
        event EventHandler<CursoCriadoEventArgs> OnCursoCriado;
        event EventHandler<EdicaoCriadaEventArgs> OnEdicaoCriada;
        event EventHandler<EstadoEdicaoAlteradoEventArgs> OnEstadoEdicaoAlterado;
        event EventHandler<AlunoRegistadoEventArgs> OnAlunoRegistado;
        event EventHandler<InscricaoCriadaEventArgs> OnInscricaoCriada;
        event EventHandler<ClassificacaoLancadaEventArgs> OnClassificacaoLancada;
        event EventHandler<ConsultaRealizadaEventArgs> OnConsultaRealizada;
        event EventHandler OnSistemaEncerrado;
    }

    /// <summary>
    /// MODEL (núcleo da aplicação)
    /// Responsável por:
    /// - Regras de negócio
    /// - Validação de dados
    /// - Coordenação da emissão do diploma
    /// - Notificação da View (via eventos)
    ///
    /// NOTA:
    /// - Não conhece detalhes da interface
    /// - Não conhece detalhes de PDFsharp
    /// - Mantém apenas a dependência abstraída através de IGeradorDiploma
    /// </summary>
    public class Model : IModelEventos
    {
        /// <summary>
        /// Dependência abstraída (injeção via interface).
        /// </summary>
        private readonly IGeradorDiploma _gerador;

        /// <summary>
        /// Eventos já implementados no protótipo atual.
        /// </summary>
        public event EventHandler<ValidacaoEventArgs> OnValidacao;
        public event EventHandler<DiplomaEmitidoEventArgs> OnDiplomaEmitido;

        /// <summary>
        /// Eventos adicionais para cobrir o resto do protótipo.
        /// </summary>
        public event EventHandler<InstituicaoGuardadaEventArgs> OnInstituicaoGuardada;
        public event EventHandler<CursoCriadoEventArgs> OnCursoCriado;
        public event EventHandler<EdicaoCriadaEventArgs> OnEdicaoCriada;
        public event EventHandler<EstadoEdicaoAlteradoEventArgs> OnEstadoEdicaoAlterado;
        public event EventHandler<AlunoRegistadoEventArgs> OnAlunoRegistado;
        public event EventHandler<InscricaoCriadaEventArgs> OnInscricaoCriada;
        public event EventHandler<ClassificacaoLancadaEventArgs> OnClassificacaoLancada;
        public event EventHandler<ConsultaRealizadaEventArgs> OnConsultaRealizada;
        public event EventHandler OnSistemaEncerrado;

        public Model(IGeradorDiploma gerador)
        {
            _gerador = gerador;
        }

        /// <summary>
        /// Método principal do Model.
        /// Executa o fluxo completo:
        /// 1. Validação
        /// 2. Geração do diploma
        /// 3. Notificação da View
        /// </summary>
        public void EmitirDiploma(string nomeAluno, string curso)
        {
            // 1. Validação
            if (string.IsNullOrWhiteSpace(nomeAluno))
            {
                OnValidacao?.Invoke(this,
                    new ValidacaoEventArgs(false, "Erro: Nome do aluno inválido."));
                return;
            }

            OnValidacao?.Invoke(this,
                new ValidacaoEventArgs(true, "Validação concluída com sucesso."));

            // 2. Geração do diploma (delegada ao serviço)
            byte[] pdf = _gerador.Gerar(nomeAluno, curso);

            // 3. Notificar a View
            OnDiplomaEmitido?.Invoke(this,
                new DiplomaEmitidoEventArgs(pdf));
        }

        /// <summary>
        /// Gestão da instituição.
        /// </summary>
        public void GuardarInstituicao(string nomeInstituicao, string designacaoDiploma, string localidade)
        {
            // TODO:
            // 1. Validar os dados da instituição
            // 2. Guardar ou atualizar a instituição
            // 3. Notificar a View através de:
            //    OnValidacao
            //    OnInstituicaoGuardada
        }

        /// <summary>
        /// Criação de curso.
        /// </summary>
        public void CriarCurso(string codigoCurso)
        {
            // TODO:
            // 1. Validar o código do curso
            // 2. Garantir que não existe duplicação
            // 3. Criar o curso
            // 4. Notificar a View através de:
            //    OnValidacao
            //    OnCursoCriado
        }

        /// <summary>
        /// Criação de edição.
        /// </summary>
        public void CriarEdicao(string codigoCurso, string codigoEdicao)
        {
            // TODO:
            // 1. Verificar se o curso existe
            // 2. Garantir que a edição não está duplicada
            // 3. Criar a edição
            // 4. Notificar a View através de:
            //    OnValidacao
            //    OnEdicaoCriada
        }

        /// <summary>
        /// Alteração do estado de uma edição.
        /// </summary>
        public void AlterarEstadoEdicao(string codigoCurso, string codigoEdicao, string novoEstado)
        {
            // TODO:
            // 1. Localizar a edição
            // 2. Validar a transição de estado
            // 3. Atualizar o estado
            // 4. Notificar a View através de:
            //    OnValidacao
            //    OnEstadoEdicaoAlterado
        }

        /// <summary>
        /// Registo de aluno.
        /// </summary>
        public void RegistarAluno(int alunoId, string nomeAluno)
        {
            // TODO:
            // 1. Validar os dados do aluno
            // 2. Registar o aluno
            // 3. Notificar a View através de:
            //    OnValidacao
            //    OnAlunoRegistado
        }

        /// <summary>
        /// Criação de inscrição.
        /// </summary>
        public void CriarInscricao(int alunoId, string codigoCurso, string codigoEdicao)
        {
            // TODO:
            // 1. Validar aluno
            // 2. Validar curso
            // 3. Validar edição
            // 4. Garantir que não existe inscrição ativa duplicada
            // 5. Criar a inscrição
            // 6. Notificar a View através de:
            //    OnValidacao
            //    OnInscricaoCriada
        }

        /// <summary>
        /// Lançamento de classificação.
        /// </summary>
        public void LancarClassificacao(int alunoId, string codigoCurso, string codigoEdicao, decimal classificacaoFinal)
        {
            // TODO:
            // 1. Localizar a inscrição
            // 2. Validar a classificação
            // 3. Associar a classificação à inscrição
            // 4. Notificar a View através de:
            //    OnValidacao
            //    OnClassificacaoLancada
        }

        /// <summary>
        /// Consulta de dados do sistema.
        /// </summary>
        public void ConsultarDados(string tipoConsulta)
        {
            // TODO:
            // 1. Interpretar o tipo de consulta
            // 2. Preparar os dados a devolver
            // 3. Notificar a View através de:
            //    OnConsultaRealizada
        }

        /// <summary>
        /// Encerramento do sistema.
        /// </summary>
        public void Sair()
        {
            // TODO:
            // 1. Tratar o encerramento do sistema
            // 2. Notificar a View através de:
            //    OnSistemaEncerrado
        }

        /// <summary>
        /// Métodos auxiliares de validação antecipada.
        /// </summary>
        public bool ExisteCurso(string codigoCurso)
        {
            // TODO:
            // Verificar se o curso existe.
            return false;
        }

        public bool ExisteEdicao(string codigoCurso, string codigoEdicao)
        {
            // TODO:
            // Verificar se a edição existe.
            return false;
        }

        public bool ExisteAluno(int alunoId)
        {
            // TODO:
            // Verificar se o aluno existe.
            return false;
        }
    }
}