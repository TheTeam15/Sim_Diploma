// ==========================================================
// CONTROLLER
// ==========================================================
// O Controller coordena toda a interação:
//
// View -> Controller -> Model
// Model -> Controller -> View
//
// O Controller é o mediador entre os componentes.
// ==========================================================

public class Controller
{
    private readonly Model model;

    private readonly View view;

    // ======================================================
    // CONSTRUTOR
    // ======================================================

    public Controller(Model model, View view)
    {
        this.model = model;
        this.view = view;

        // ==============================================
        // SUBSCRIÇÃO AOS EVENTOS DO MODEL
        // ==============================================
        // O Controller recebe os eventos do Model.
        // A View NÃO comunica diretamente com o Model.
        // ==============================================

        model.OnValidacao += ProcessarValidacao;

        model.OnDiplomaEmitido += ProcessarDiploma;
    }

    // ======================================================
    // RECEBE PEDIDO DA VIEW
    // ======================================================

    public void EmitirDiploma(string nomeAluno, string curso)
    {
        model.EmitirDiploma(nomeAluno, curso);
    }

    // ======================================================
    // PROCESSAMENTO DA VALIDAÇÃO
    // ======================================================

    public void ProcessarValidacao(
        object? sender,
        ValidacaoEventArgs e
    )
    {
        view.MostrarValidacao(e);
    }

    // ======================================================
    // PROCESSAMENTO DO PDF GERADO
    // ======================================================

    public void ProcessarDiploma(
        object? sender,
        DiplomaEmitidoEventArgs e
    )
    {
        view.MostrarDiploma(e.Pdf);
    }
}