using System;

/// Ponto de entrada da aplicação.
/// 
/// Responsável por:
/// - Criar objetos
/// - Ligar componentes
/// - Iniciar fluxo
class Program
{
    static void Main()
    {
        // Serviço
        IGeradorDiploma gerador = new Gerador();

        // Model
        Model model = new Model(gerador);

        // View
        View view = new View();
        view.Subscribir(model);

        // Controller
        Controller controller = new Controller(model);

        // Ligação View → Controller
        view.OnCriarAluno             += controller.CriarAluno;
        view.OnCriarInscricao         += controller.CriarInscricao;
        view.OnConcluirInscricao      += controller.ConcluirInscricao;
        view.OnClassificar            += controller.Classificar;
        view.OnConsultarAluno         += controller.ConsultarAluno;
        view.OnConsultarInscricao     += controller.ConsultarInscricao;
        view.OnConsultarClassificacao += controller.ConsultarClassificacao;
        view.OnEmitirDiploma          += controller.EmitirDiploma;

        // Início do fluxo
        while (true)
        {
            view.Menu();
        }

        /* view.PedirEmissao();
        Console.ReadLine(); */
    }
}