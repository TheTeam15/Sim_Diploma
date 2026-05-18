// ==========================================================
// PROGRAM
// ==========================================================
// Classe principal da aplicação.
//
// Responsável por:
// - criar os componentes;
// - estabelecer dependências;
// - iniciar o fluxo MVC.
// ==========================================================

class Program
{
    static void Main()
    {
        // ==============================================
        // CRIAÇÃO DO GERADOR PDF
        // ==============================================

        IGeradorDiploma gerador =
            new GeradorDiplomaPdfSharp();

        // ==============================================
        // CRIAÇÃO DO MODEL
        // ==============================================

        Model model = new Model(gerador);

        // ==============================================
        // CRIAÇÃO DA VIEW
        // ==============================================

        View view = new View();

        // ==============================================
        // CRIAÇÃO DO CONTROLLER
        // ==============================================

        Controller controller =
            new Controller(model, view);

        // ==============================================
        // ASSOCIAÇÃO DA VIEW AO CONTROLLER
        // ==============================================

        view.SetController(controller);

        // ==============================================
        // INÍCIO DA APLICAÇÃO
        // ==============================================

        view.PedirEmissao();
    }
}