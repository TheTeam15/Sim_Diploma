using System;

namespace SimDiplomaMVC
{
    /// Ponto de entrada da aplicação.
    ///
    /// Responsável por:
    /// - Criar os componentes principais
    /// - Ligar as dependências
    /// - Arrancar o protótipo
    class Program
    {
        static void Main()
        {
            // Serviço responsável pela geração do diploma
            IGeradorDiploma gerador = new Gerador();

            // Núcleo da aplicação
            Model model = new Model(gerador);

            // Componente de apresentação
            View view = new View();

            // A View subscreve os eventos do Model
            view.Subscrever(model);

            // Componente de coordenação
            Controller controller = new Controller(model, view);

            // Arranque do protótipo
            // Para já, como só a emissão de diploma está implementada,
            // continua a ser feita uma simulação simples.
            controller.EmitirDiploma("João Silva", "Engenharia Informática");

            Console.ReadLine();
        }
    }
}