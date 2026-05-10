using System;

/// VIEW
/// Responsável apenas pela apresentação.
/// 
/// Neste exemplo:
/// - Apresenta mensagens no console
/// - Em contexto real pode ser UI gráfica ou web
/// 
/// IMPORTANTE:
/// - Não contém lógica de negócio
/// - Não chama diretamente o Model
/// - Apenas reage a eventos
public class View
{
    /// Evento que comunica com o Controller
    /// (input do utilizador → MVC Curry & Grace)

    public event Action<int, string>?         OnCriarAluno;
    public event Action<int, string>?         OnCriarInscricao;
    public event Action<int, string>?         OnConcluirInscricao;
    public event Action<int, string, double>? OnClassificar;
    public event Action<int>?                 OnConsultarAluno;
    public event Action<int, string>?         OnConsultarInscricao;
    public event Action<int, string>?         OnConsultarClassificacao;
    public event Action<string, string>? OnEmitirDiploma;

    /// Subscrição aos eventos do Model
    /// Liga a View ao fluxo de notificações
    public void Subscribir(Model model)
    {
        model.Resultado               += MostrarResultado;
        model.InscricaoCriada         += MostrarInscricaoCriada;
        model.ClassificacaoCriada     += MostrarClassificacaoCriada;
        model.AlunoConsultado         += MostrarAluno;
        model.InscricaoConsultada     += MostrarInscricaoConsultada;
        model.ClassificacaoConsultada += MostrarClassificacaoConsultada;
        model.OnValidacao += MostrarValidacao;
        model.OnDiplomaEmitido += MostrarDiploma;
    }

    // ================= MENU =================

    public void Menu()
    {
        Console.WriteLine("\n1 - Criar Aluno");
        Console.WriteLine("2 - Inscrever Aluno");
        Console.WriteLine("3 - Concluir Inscricao");
        Console.WriteLine("4 - Classificação");
        Console.WriteLine("5 - Consultar Aluno");
        Console.WriteLine("6 - Consultar Inscricao");
        Console.WriteLine("7 - Consultar Classificacao");
        Console.WriteLine("8 - Emitir Diploma");
        Console.WriteLine("0 - Sair");

        var op = Console.ReadLine();

        switch (op)
        {
            case "1": CriarAluno(); break;
            case "2": CriarInscricao(); break;
            case "3": ConcluirInscricao(); break;
            case "4": Classificar(); break;
            case "5": ConsultarAluno(); break;
            case "6": ConsultarInscricao(); break;
            case "7": ConsultarClassificacao(); break;
            case "8": PedirEmissao(); break;
            case "0":
                Console.WriteLine("O sistema foi encerrado.");
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Opcao invalida. Tente novamente.");
                break;
        }
    }

 // ================= INPUT COM VALIDACAO =================

    void CriarAluno()
    {
        Console.Write("Id: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID invalido. Introduza um numero inteiro.");
            return;
        }

        Console.Write("Nome: ");
        string nome = Console.ReadLine()!;

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("Nome nao pode ser vazio.");
            return;
        }

        OnCriarAluno?.Invoke(id, nome);
    }

    void CriarInscricao()
    {
        Console.Write("Aluno Id: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID invalido. Introduza um numero inteiro.");
            return;
        }

        Console.Write("Edicao: ");
        string ed = Console.ReadLine()!;

        if (string.IsNullOrWhiteSpace(ed))
        {
            Console.WriteLine("Edicao nao pode ser vazia.");
            return;
        }

        OnCriarInscricao?.Invoke(id, ed);
    }

    void ConcluirInscricao()
    {
        Console.Write("Aluno Id: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID invalido. Introduza um numero inteiro.");
            return;
        }

        Console.Write("Edicao: ");
        string ed = Console.ReadLine()!;

        if (string.IsNullOrWhiteSpace(ed))
        {
            Console.WriteLine("Edicao nao pode ser vazia.");
            return;
        }

        OnConcluirInscricao?.Invoke(id, ed);
    }

    void Classificar()
    {
        Console.Write("Aluno Id: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID invalido. Introduza um numero inteiro.");
            return;
        }

        Console.Write("Edicao: ");
        string ed = Console.ReadLine()!;

        if (string.IsNullOrWhiteSpace(ed))
        {
            Console.WriteLine("Edicao nao pode ser vazia.");
            return;
        }

        Console.Write("Nota (0-20): ");
        if (!double.TryParse(Console.ReadLine(), out double nota))
        {
            Console.WriteLine("Nota invalida. Introduza um numero entre 0 e 20.");
            return;
        }

        OnClassificar?.Invoke(id, ed, nota);
    }

    void ConsultarAluno()
    {
        Console.Write("Id: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID invalido. Introduza um numero inteiro.");
            return;
        }

        OnConsultarAluno?.Invoke(id);
    }

    void ConsultarInscricao()
    {
        Console.Write("Aluno Id: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID invalido. Introduza um numero inteiro.");
            return;
        }

        Console.Write("Edicao: ");
        string ed = Console.ReadLine()!;

        if (string.IsNullOrWhiteSpace(ed))
        {
            Console.WriteLine("Edicao nao pode ser vazia.");
            return;
        }

        OnConsultarInscricao?.Invoke(id, ed);
    }

    void ConsultarClassificacao()
    {
        Console.Write("Aluno Id: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID invalido. Introduza um numero inteiro.");
            return;
        }

        Console.Write("Edicao: ");
        string ed = Console.ReadLine()!;

        if (string.IsNullOrWhiteSpace(ed))
        {
            Console.WriteLine("Edicao nao pode ser vazia.");
            return;
        }

        OnConsultarClassificacao?.Invoke(id, ed);
    }

    /// Simula input do utilizador
    public void PedirEmissao()
    {
        Console.WriteLine("Nome do aluno:");
        string nome = Console.ReadLine() ?? ""; // evita null

        Console.WriteLine("Curso:");
        string curso = Console.ReadLine() ?? "";

        // Envia evento para o Controller
        OnEmitirDiploma?.Invoke(nome, curso);
    }

    // ================= OUTPUT (Curry & Grace) =================
    // 1. Model notificou a View (evento)
    // 2. View vai buscar dados ao estado interno do Model (sender)
    // 3. View apresenta ao utilizador

    /// Resultado generico de operacao
    private void MostrarResultado(object? sender, ResultadoEventArgs e)
    {
        Console.WriteLine(e.Sucesso ? $"OK: {e.Mensagem}" : $"ERRO: {e.Mensagem}");
    }

   /// Inscricao criada — dados vem do EventArgs (criacao, nao consulta)
    private void MostrarInscricaoCriada(object? sender, InscricaoAlunoEventArgs e)
    {
        var i = e.Inscricao; // dados da inscricao RECEM criada

        Console.WriteLine($"\n=== INSCRICAO CRIADA ===");
        Console.WriteLine($"Aluno:             {i.AlunoId}");
        Console.WriteLine($"Edicao:            {i.Edicao}");
        Console.WriteLine($"Ativa:             {i.Ativa}");
        Console.WriteLine($"Tem classificacao: {i.TemClassificacao}");
    }

    /// Classificacao lancada — dados vem do EventArgs (criacao, nao consulta)
    private void MostrarClassificacaoCriada(object? sender, ClassificacaoEventArgs e)
    {
        var c = e.Classificacao; // dados da classificacao RECEM lancada

        Console.WriteLine($"\n=== CLASSIFICACAO LANCADA ===");
        Console.WriteLine($"Nota:     {c.Nota}");
        Console.WriteLine($"Aprovado: {c.Aprovado}");
    }

    /// Aluno consultado — View vai buscar ao estado interno do Model
    private void MostrarAluno(object? sender, AlunoEventArgs e)
    {
        var model = sender as Model;
        var a = model?.UltimoAlunoConsultado ?? e.Aluno;

        if (a == null)
        {
            Console.WriteLine("Aluno nao encontrado.");
            return;
        }

        Console.WriteLine($"\n=== ALUNO ===");
        Console.WriteLine($"ID:         {a.Id}");
        Console.WriteLine($"Nome:       {a.Nome}");
        Console.WriteLine($"Inscricoes: {a.Inscricoes.Count}");
    }

    /// Inscricao consultada — View vai buscar ao estado interno do Model
    private void MostrarInscricaoConsultada(object? sender, InscricaoAlunoConsultadaEventArgs e)
    {
        var model = sender as Model;
        var i = model?.UltimaInscricaoConsultada ?? e.Inscricao;

        if (i == null)
        {
            Console.WriteLine("Inscricao nao encontrada.");
            return;
        }

        Console.WriteLine($"\n=== INSCRICAO ===");
        Console.WriteLine($"Aluno:             {i.AlunoId}");
        Console.WriteLine($"Edicao:            {i.Edicao}");
        Console.WriteLine($"Ativa:             {i.Ativa}");
        Console.WriteLine($"Tem classificacao: {i.TemClassificacao}");
    }

    /// Classificacao consultada — View vai buscar ao estado interno do Model
    private void MostrarClassificacaoConsultada(object? sender, ClassificacaoConsultadaEventArgs e)
    {
        var model = sender as Model;
        var c = model?.UltimaClassificacaoConsultada ?? e.Classificacao;

        if (c == null)
        {
            Console.WriteLine("Classificacao nao encontrada.");
            return;
        }

        Console.WriteLine($"\n=== CLASSIFICACAO ===");
        Console.WriteLine($"Nota:     {c.Nota}");
        Console.WriteLine($"Aprovado: {c.Aprovado}");
    }

    /// Apresenta o resultado da validação
    private void MostrarValidacao(object? sender, ValidacaoEventArgs e)
    {
        Console.WriteLine("[VIEW] " + e.Mensagem);
    }

    /// Apresenta informação sobre o diploma gerado
    private void MostrarDiploma(object? sender, DiplomaEmitidoEventArgs e)
    {
        Console.WriteLine("[VIEW] Diploma gerado com sucesso!");
        Console.WriteLine("[VIEW] Tamanho do PDF: " + e.PdfBytes.Length + " bytes");

        // Simula download/armazenamento
        System.IO.File.WriteAllBytes("diploma.pdf", e.PdfBytes);
    }
}