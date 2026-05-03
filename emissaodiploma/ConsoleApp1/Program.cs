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
        // SISTEMA ORIGINAL
        IGeradorDiploma gerador = new Gerador();
        Model model = new Model(gerador);
        View view = new View();
        view.Subscribir(model);
        Controller controller = new Controller(model);

        view.OnCriarAluno += controller.CriarAluno;
        view.OnCriarInscricao += controller.CriarInscricao;
        view.OnConcluirInscricao += controller.ConcluirInscricao;
        view.OnClassificar += controller.Classificar;
        view.OnConsultarAluno += controller.ConsultarAluno;
        view.OnConsultarInscricao += controller.ConsultarInscricao;
        view.OnConsultarClassificacao += controller.ConsultarClassificacao;
        view.OnEmitirDiploma += controller.EmitirDiploma;

        // SISTEMA (CURSOS) 
        CursosModel cursosModel = new CursosModel();
        CursosView cursosView = new CursosView();
        CursosController cursosController = new CursosController(cursosModel);

        cursosView.Subscribir(cursosModel);

        cursosView.OnGuardarInstituicao += cursosController.GuardarInstituicao;
        cursosView.OnCriarCurso += cursosController.CriarCurso;
        cursosView.OnCriarEdicao += cursosController.CriarEdicao;
        cursosView.OnAlterarEstadoEdicao += cursosController.AlterarEstadoEdicao;

        // MENU PRINCIPAL 
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== SISTEMA DE GESTÃO ESCOLAR ===");
            Console.WriteLine();
            Console.WriteLine("1 - Sistema de Alunos");
            Console.WriteLine("2 - Sistema de Cursos");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            string? opcao = Console.ReadLine();

            if (opcao == "1")
            {
                // Loop para manter no submenu 
                bool noSubmenu = true;
                while (noSubmenu)
                {
                    view.Menu();

                    Console.WriteLine("\nPressione ESC para voltar ao menu principal ou ENTER para continuar...");
                    var tecla = Console.ReadKey(true);
                    if (tecla.Key == ConsoleKey.Escape)
                    {
                        noSubmenu = false;
                    }
                }
            }
            else if (opcao == "2")
            {
                // Loop para manter no  submenu cursos
                bool noSubmenu = true;
                while (noSubmenu)
                {
                    cursosView.MostrarFormulario(cursosController);

                    Console.WriteLine("\nPressione ESC para voltar ao menu principal ou ENTER para continuar...");
                    var tecla = Console.ReadKey(true);
                    if (tecla.Key == ConsoleKey.Escape)
                    {
                        noSubmenu = false;
                    }
                }
            }
            else if (opcao == "0")
            {
                Console.WriteLine("Programa terminado.");
                break;
            }
            else
            {
                Console.WriteLine("Opção inválida. Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}