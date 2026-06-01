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
        // Serviço responsável pela geração do diploma
        IGeradorDiploma gerador = new Gerador();

        // Model
        Model model = new Model(gerador);

        // View
        View view = new View();

        view.Subscrever(model);

        // Controller
        // O Controller liga os eventos da View aos métodos do Model
        Controller controller = new Controller(model, view);

        // Início do fluxo
        while (true)
        {
            view.Menu();
        }

        /* view.PedirEmissao();
        Console.ReadLine(); */
    }
}