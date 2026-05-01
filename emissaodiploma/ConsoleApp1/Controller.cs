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

    /// Método que inicia o processo de emissão
    public void EmitirDiploma(string nomeAluno, string curso)
    {
        _model.EmitirDiploma(nomeAluno, curso);
    }
}