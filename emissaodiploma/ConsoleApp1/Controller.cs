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

    public Controller(Model model)
    {
        _model = model;
    }

    public void CriarAluno(int id, string nome)
        => _model.RegistarAluno(id, nome);

    public void CriarInscricao(int alunoId, string edicao)
        => _model.InscreverAluno(alunoId, edicao);

    public void ConcluirInscricao(int alunoId, string edicao)
        => _model.ConcluirInscricao(alunoId, edicao);

    public void Classificar(int alunoId, string edicao, double nota)
        => _model.LancarClassificacao(alunoId, edicao, nota);

    public void ConsultarAluno(int id)
        => _model.ConsultarAluno(id);

    public void ConsultarInscricao(int alunoId, string edicao)
        => _model.ConsultarInscricao(alunoId, edicao);

    public void ConsultarClassificacao(int alunoId, string edicao)
        => _model.ConsultarClassificacao(alunoId, edicao);

    /// Método que inicia o processo de emissão
    public void EmitirDiploma(string nomeAluno, string curso)
    {
        _model.EmitirDiploma(nomeAluno, curso);
    }
}