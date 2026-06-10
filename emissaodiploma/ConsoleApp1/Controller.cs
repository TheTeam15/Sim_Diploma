using System;

/// <summary>
/// Controller da aplicação SimDiploma.
/// 
/// Responsabilidades:
/// - Coordenar a interação entre a View e o Model;
/// - Receber os eventos disparados pela View;
/// - Invocar os métodos adequados do Model;
/// - Tratar erros da aplicação de forma centralizada.
/// 
/// O Controller não deve conter regras de negócio.
/// As regras de negócio pertencem ao Model.
/// </summary>
public class Controller
{
    private readonly Model _model;
    private readonly View _view;

    /// <summary>
    /// Cria uma nova instância do Controller.
    /// </summary>
    /// <param name="model">Componente responsável pelas regras de negócio.</param>
    /// <param name="view">Componente responsável pela interação com o utilizador.</param>
    public Controller(Model model, View view)
    {
        _model = model ?? throw new ArgumentNullException(nameof(model));
        _view = view ?? throw new ArgumentNullException(nameof(view));

        LigarEventosDaView(view);
    }

    /// <summary>
    /// Inicia o ciclo principal da aplicação.
    /// </summary>
    public void Iniciar()
    {
        bool sair = false;

        while (!sair)
        {
            ExecutarOperacao(() =>
            {
                sair = _view.Menu();
            });
        }
    }

    /// <summary>
    /// Liga os eventos da View aos respetivos métodos do Controller.
    /// </summary>
    private void LigarEventosDaView(View view)
    {
        view.OnCriarAluno += CriarAluno;
        view.OnCriarInscricao += CriarInscricao;
        view.OnConcluirInscricao += ConcluirInscricao;
        view.OnClassificar += Classificar;

        view.OnConsultarAluno += ConsultarAluno;
        view.OnConsultarInscricao += ConsultarInscricao;
        view.OnConsultarClassificacao += ConsultarClassificacao;

        view.OnGuardarInstituicao += GuardarInstituicao;
        view.OnAlterarInstituicao += AlterarInstituicao;
        view.OnApagarInstituicao += ApagarInstituicao;
        view.OnConsultarInstituicao += ConsultarInstituicao;

        view.OnCriarCurso += CriarCurso;
        view.OnAlterarCurso += AlterarCurso;
        view.OnApagarCurso += ApagarCurso;
        view.OnConsultarCurso += ConsultarCurso;

        view.OnCriarEdicao += CriarEdicao;
        view.OnAlterarEstadoEdicao += AlterarEstadoEdicao;
        view.OnAlterarEdicao += AlterarEdicao;
        view.OnApagarEdicao += ApagarEdicao;
        view.OnConsultarEdicao += ConsultarEdicao;

        view.OnEmitirDiploma += EmitirDiploma;
    }

    /// <summary>
    /// Executa uma operação da aplicação e trata eventuais erros.
    /// </summary>
    /// <param name="operacao">Operação a executar.</param>
    private void ExecutarOperacao(Action operacao)
    {
        try
        {
            operacao();
        }
        catch (Exception e)
        {
            TratarErro(e);
        }
    }

    /// <summary>
    /// Apresenta uma mensagem de erro ao utilizador.
    /// </summary>
    /// <param name="e">Exceção capturada.</param>
    private void TratarErro(Exception e)
    {
        _view.MostrarResultado(false, e.Message);
    }

    // ============================================================
    // GESTÃO DE ALUNOS, INSCRIÇÕES E CLASSIFICAÇÕES
    // ============================================================
    
    public void CriarAluno(int id, string nome)
    {
        ExecutarOperacao(() =>
        {
            _model.RegistarAluno(id, nome);
        });
    }

    public void CriarInscricao(int alunoId, int edicaoId)
    {
        ExecutarOperacao(() =>
        {
            _model.InscreverAluno(alunoId, edicaoId);
        });
    }

    public void ConcluirInscricao(int alunoId, int edicaoId)
    {
        ExecutarOperacao(() =>
        {
            _model.ConcluirInscricao(alunoId, edicaoId);
        });
    }

    public void Classificar(int alunoId, int edicaoId, double valorNota)
    {
        ExecutarOperacao(() =>
        {
            Nota nota = new Nota(valorNota);
            _model.LancarClassificacao(alunoId, edicaoId, nota);
        });
    }

    public void ConsultarAluno(int id)
    {
        ExecutarOperacao(() =>
        {
            _model.ConsultarAluno(id);
        });
    }

    public void ConsultarInscricao(int alunoId, int edicaoId)
    {
        ExecutarOperacao(() =>
        {
            _model.ConsultarInscricao(alunoId, edicaoId);
        });
    }

    public void ConsultarClassificacao(int alunoId, int edicaoId)
    {
        ExecutarOperacao(() =>
        {
            _model.ConsultarClassificacao(alunoId, edicaoId);
        });
    }

    // ============================================================
    // GESTÃO DE INSTITUIÇÕES
    // ============================================================

    
    public void GuardarInstituicao(int idInstituicao, string nomeInstituicao, string cidade, string pais)
    {
        ExecutarOperacao(() =>
        {
            _model.GuardarInstituicao(idInstituicao, nomeInstituicao, cidade, pais);
        });
    }

    public void AlterarInstituicao(int idInstituicao, string nomeInstituicao, string cidade, string pais)
    {
        ExecutarOperacao(() =>
        {
            _model.AlterarInstituicao(idInstituicao, nomeInstituicao, cidade, pais);
        });
    }

    public void ApagarInstituicao(int idInstituicao)
    {
        ExecutarOperacao(() =>
        {
            _model.ApagarInstituicao(idInstituicao);
        });
    }

    public void ConsultarInstituicao(int idInstituicao)
    {
        ExecutarOperacao(() =>
        {
            _model.ConsultarInstituicao(idInstituicao);
        });
    }

    // ============================================================
    // GESTÃO DE CURSOS 

    public void CriarCurso(
        int idCurso,
        int idInstituicao,
        string nomeCurso,
        string grauAcademico,
        string descricao,
        string estrutura)
    {
        ExecutarOperacao(() =>
        {
            _model.CriarCurso(
                idCurso,
                idInstituicao,
                nomeCurso,
                grauAcademico,
                descricao,
                estrutura);
        });
    }

    public void AlterarCurso(
        int idCurso,
        string nomeCurso,
        string grauAcademico,
        string descricao,
        string estrutura)
    {
        ExecutarOperacao(() =>
        {
            _model.AlterarCurso(
                idCurso,
                nomeCurso,
                grauAcademico,
                descricao,
                estrutura);
        });
    }

    public void ApagarCurso(int idCurso)
    {
        ExecutarOperacao(() =>
        {
            _model.ApagarCurso(idCurso);
        });
    }

    public void ConsultarCurso(int idCurso)
    {
        ExecutarOperacao(() =>
        {
            _model.ConsultarCurso(idCurso);
        });
    }

    // ============================================================
    // GESTÃO DE EDIÇÕES
    // ============================================================

    public void CriarEdicao(
        int idEdicao,
        int idCurso,
        string anoLetivo,
        DateTime dataInicio,
        DateTime dataFim,
        string modalidade)
    {
        ExecutarOperacao(() =>
        {
            _model.CriarEdicao(
                idEdicao,
                idCurso,
                anoLetivo,
                dataInicio,
                dataFim,
                modalidade);
        });
    }

    public void AlterarEdicao(
        int idEdicao,
        string anoLetivo,
        DateTime dataInicio,
        DateTime dataFim,
        string modalidade)
    {
        ExecutarOperacao(() =>
        {
            _model.AlterarEdicao(
                idEdicao,
                anoLetivo,
                dataInicio,
                dataFim,
                modalidade);
        });
    }

    public void AlterarEstadoEdicao(int idEdicao, EstadoEdicao novoEstado)
    {
        ExecutarOperacao(() =>
        {
            _model.AlterarEstadoEdicao(idEdicao, novoEstado);
        });
    }

    public void ApagarEdicao(int idEdicao)
    {
        ExecutarOperacao(() =>
        {
            _model.ApagarEdicao(idEdicao);
        });
    }

    public void ConsultarEdicao(int idEdicao)
    {
        ExecutarOperacao(() =>
        {
            _model.ConsultarEdicao(idEdicao);
        });
    }

    // ============================================================
    // EMISSÃO DE DIPLOMAS
    // ============================================================
    public void EmitirDiploma(int alunoId, int idEdicao)
    {
        ExecutarOperacao(() =>
        {
            _model.EmitirDiploma(alunoId, idEdicao);
        });
    }
}