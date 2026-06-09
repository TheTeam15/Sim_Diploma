using System;
using PdfSharp.Fonts;

/// <summary>
/// Ponto de entrada da aplicação SimDiploma.
/// 
/// Responsabilidades:
/// - Criar os componentes principais da aplicação;
/// - Ligar as dependências entre Model, View e Controller;
/// - Iniciar o fluxo principal da aplicação;
/// - Tratar erros críticos de arranque ou execução.
/// </summary>
class Program
{
    /// <summary>
    /// Método principal da aplicação.
    /// </summary>
    static void Main()
    {
        try
        {
            GlobalFontSettings.UseWindowsFontsUnderWindows = true;

            Controller controller = CriarAplicacao();

            controller.Iniciar();
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine("ERRO CRÍTICO DA APLICAÇÃO");
            Console.WriteLine("-------------------------");
            Console.WriteLine("Ocorreu um erro inesperado que impediu a continuação da aplicação.");
            Console.WriteLine();
            Console.WriteLine($"Detalhe técnico: {ex.Message}");
            Console.WriteLine();
            Console.WriteLine("Prima qualquer tecla para sair...");
            Console.ReadKey();
        }
    }

    /// <summary>
    /// Cria e liga os principais componentes da aplicação.
    /// </summary>
    /// <returns>Controller principal da aplicação.</returns>
    private static Controller CriarAplicacao()
    {
        // Serviço responsável pela geração textual ou lógica do diploma.
        IGeradorDiploma gerador = new Gerador();

        // Componente responsável pelos dados e regras de negócio.
        Model model = new Model(gerador);

        // Componente responsável pela interação com o utilizador.
        View view = new View();

        // A View subscreve os eventos do Model para apresentar mensagens ao utilizador.
        view.Subscrever(model);

        // O Controller coordena a interação entre a View e o Model.
        Controller controller = new Controller(model, view);

        return controller;
    }
}