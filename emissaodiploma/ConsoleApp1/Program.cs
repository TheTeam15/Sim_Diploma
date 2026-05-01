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
        view.OnEmitirDiploma += controller.EmitirDiploma;

        // Início do fluxo
        view.PedirEmissao();

        Console.ReadLine();
    }
}