/// CONTROLLER
/// Responsável por coordenar a interação.
/// 
/// Função:
/// - Recebe input
/// - Invoca o Model
/// 
/// IMPORTANTE:
/// - Não contém lógica de negócio
/// - Não processa dados
public class Controller
{
    private readonly Model _model;

    // public Controller(Model model)
    // {
    //     _model = model;
    // }

    public Controller(Model model, View view)
    {
        _model = model;

        LigarEventosDaView(view);
    }

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
        view.OnCriarCurso += CriarCurso;
        view.OnAlterarCurso += AlterarCurso;
        view.OnApagarCurso += ApagarCurso;
        view.OnCriarEdicao += CriarEdicao;
        view.OnAlterarEstadoEdicao += AlterarEstadoEdicao;
        view.OnAlterarEdicao += AlterarEdicao;
        view.OnApagarEdicao += ApagarEdicao;
        view.OnConsultarInstituicao += ConsultarInstituicao;
        view.OnConsultarCurso += ConsultarCurso;
        view.OnConsultarEdicao += ConsultarEdicao;

    }

    public void CriarAluno(int id, string nome)
        => _model.RegistarAluno(id, nome);

    public void CriarInscricao(int alunoId, string edicao)
        => _model.InscreverAluno(alunoId, edicao);

    public void ConcluirInscricao(int alunoId, string edicao)
        => _model.ConcluirInscricao(alunoId, edicao);

    public void Classificar(int alunoId, string edicao, Nota nota)
        => _model.LancarClassificacao(alunoId, edicao, nota);

    public void ConsultarAluno(int id)
        => _model.ConsultarAluno(id);

    public void ConsultarInscricao(int alunoId, string edicao)
        => _model.ConsultarInscricao(alunoId, edicao);

    public void ConsultarClassificacao(int alunoId, string edicao)
        => _model.ConsultarClassificacao(alunoId, edicao);

    // GESTÃO DE INSTITUIÇÕES 

    public void GuardarInstituicao(int idInstituicao, string nomeInstituicao, string cidade, string pais)
        => _model.GuardarInstituicao(idInstituicao, nomeInstituicao, cidade, pais);

    public void AlterarInstituicao(int id, string nome, string cidade, string pais)
    => _model.AlterarInstituicao(id, nome, cidade, pais);

    public void ApagarInstituicao(int id)
    => _model.ApagarInstituicao(id);

    // GESTÃO DE CURSOS 

    public void CriarCurso(int idCurso, int idInstituicao, string nomeCurso,
        string grauAcademico, string descricao, string estrutura)
        => _model.CriarCurso(idCurso, idInstituicao, nomeCurso,
            grauAcademico, descricao, estrutura);

    public void AlterarCurso(int idCurso, string nomeCurso,
    string grauAcademico, string descricao, string estrutura)
    => _model.AlterarCurso(idCurso, nomeCurso,
        grauAcademico, descricao, estrutura);

    public void ConsultarCurso(int idCurso)
        => _model.ConsultarCurso(idCurso);


    public void ApagarCurso(int idCurso)
    => _model.ApagarCurso(idCurso);

    // GESTÃO DE EDIÇÕES
    public void CriarEdicao(int idEdicao, int idCurso, string anoLetivo,
        DateTime dataInicio, DateTime dataFim, string modalidade)
        => _model.CriarEdicao(idEdicao, idCurso, anoLetivo,
            dataInicio, dataFim, modalidade);

    public void AlterarEstadoEdicao(int idEdicao, EstadoEdicao novoEstado)
        => _model.AlterarEstadoEdicao(idEdicao, novoEstado);

    public void ConsultarInstituicao(int idInstituicao)
    => _model.ConsultarInstituicao(idInstituicao);

    public void AlterarEdicao(int idEdicao, string anoLetivo,
    DateTime dataInicio, DateTime dataFim, string modalidade)
    => _model.AlterarEdicao(idEdicao,anoLetivo,
        dataInicio, dataFim, modalidade);

    public void ApagarEdicao(int idEdicao)
    => _model.ApagarEdicao(idEdicao);


    public void ConsultarEdicao(int idEdicao)
        => _model.ConsultarEdicao(idEdicao);
}


