using System;

// Delegados da View
public delegate void GuardarInstituicaoEventHandler(string nomeInstituicao);
public delegate void CriarCursoEventHandler(string codigoCurso);
public delegate void CriarEdicaoEventHandler(string codigoCurso, string codigoEdicao);
public delegate void AlterarEstadoEdicaoEventHandler(string codigoCurso, string codigoEdicao, EstadoEdicao novoEstado);

// Responsável pela interação com o utilizador
public class CursosView
{
    // Eventos disparados pela View
    public event GuardarInstituicaoEventHandler? OnGuardarInstituicao;
    public event CriarCursoEventHandler? OnCriarCurso;
    public event CriarEdicaoEventHandler? OnCriarEdicao;
    public event AlterarEstadoEdicaoEventHandler? OnAlterarEstadoEdicao;

    // Mostra o formulário/menu principal
    public void MostrarFormulario(CursosController controller)
    {
        Console.WriteLine();
        Console.WriteLine("=== Aplicação Demonstradora MVC ===");
        Console.WriteLine("1 - Guardar Instituição");
        Console.WriteLine("2 - Criar Curso");
        Console.WriteLine("3 - Criar Edição");
        Console.WriteLine("4 - Alterar Estado da Edição");
        Console.WriteLine("0 - Sair");
        Console.Write("Opção: ");

        string? opcao = Console.ReadLine();

        switch (opcao)
        {
            case "1":
                GuardarInstituicao();
                break;

            case "2":
                CriarCurso();
                break;

            case "3":
                CriarEdicao(controller);
                break;

            case "4":
                AlterarEstadoEdicao(controller);
                break;

            case "0":
                Console.WriteLine("Programa terminado.");
                Environment.Exit(0);
                break;

            default:
                Console.WriteLine("Opção inválida.");
                break;
        }
    }

    // GUARDAR INSTITUIÇÃO
    public void GuardarInstituicao()
    {
        Console.Write("Nome da instituição: ");
        string nomeInstituicao = Console.ReadLine() ?? string.Empty;

        OnGuardarInstituicao?.Invoke(nomeInstituicao);
    }

    // CASO 1 - CRIAR CURSO
    public void CriarCurso()
    {
        Console.Write("Código do curso: ");
        string codigoCurso = Console.ReadLine() ?? string.Empty;

        OnCriarCurso?.Invoke(codigoCurso);
    }

    // CASO 2 - CRIAR EDIÇÃO
    public void CriarEdicao(CursosController controller)
    {
        Console.Write("Código do curso: ");
        string codigoCurso = Console.ReadLine() ?? string.Empty;

        if (!controller.ExisteCurso(codigoCurso))
        {
            Console.WriteLine("O curso indicado não existe.");
            return;
        }

        Console.Write("Código da edição: ");
        string codigoEdicao = Console.ReadLine() ?? string.Empty;

        OnCriarEdicao?.Invoke(codigoCurso, codigoEdicao);
    }

    // ALTERAR ESTADO DA EDIÇÃO
    public void AlterarEstadoEdicao(CursosController controller)
    {
        Console.Write("Código do curso: ");
        string codigoCurso = Console.ReadLine() ?? string.Empty;

        if (!controller.ExisteCurso(codigoCurso))
        {
            Console.WriteLine("O curso indicado não existe.");
            return;
        }

        Console.Write("Código da edição: ");
        string codigoEdicao = Console.ReadLine() ?? string.Empty;

        if (!controller.ExisteEdicao(codigoCurso, codigoEdicao))
        {
            Console.WriteLine("A edição indicada não existe.");
            return;
        }

        Console.WriteLine("Novo estado:");
        Console.WriteLine("0 - Planeada");
        Console.WriteLine("1 - Aberta");
        Console.WriteLine("2 - Encerrada");
        Console.WriteLine("3 - Cancelada");
        Console.Write("Escolha: ");

        string? valor = Console.ReadLine();

        if (!int.TryParse(valor, out int estadoInt) ||
            estadoInt < 0 || estadoInt > 3)
        {
            Console.WriteLine("Estado inválido.");
            return;
        }

        EstadoEdicao novoEstado = (EstadoEdicao)estadoInt;

        OnAlterarEstadoEdicao?.Invoke(codigoCurso, codigoEdicao, novoEstado);
    }

    // Métodos Mostrar*
    public void MostrarResultado(string mensagem)
    {
        Console.WriteLine(mensagem);
    }

    public void MostrarInstituicaoGuardada(Instituicao instituicao)
    {
        Console.WriteLine("Dados atualizados da instituição:");
        Console.WriteLine(instituicao);
    }

    public void MostrarCursoCriado(Curso curso)
    {
        Console.WriteLine("Dados atualizados do curso:");
        Console.WriteLine(curso);
    }

    public void MostrarEdicaoCriada(Edicao edicao)
    {
        Console.WriteLine("Dados atualizados da edição:");
        Console.WriteLine(edicao);
    }

    public void MostrarEstadoEdicaoAlterado(Edicao edicao)
    {
        Console.WriteLine("Dados atualizados da edição:");
        Console.WriteLine(edicao);
    }
    public void Subscribir(CursosModel model)
    {
        model.ResultadoOperacao += MostrarResultado;
        model.InstituicaoGuardada += MostrarInstituicaoGuardada;
        model.CursoCriado += MostrarCursoCriado;
        model.EdicaoCriada += MostrarEdicaoCriada;
        model.EstadoEdicaoAlterado += MostrarEstadoEdicaoAlterado;
    }
}