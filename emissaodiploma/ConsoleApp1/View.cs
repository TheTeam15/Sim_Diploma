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

    public event Action<int, string>? OnCriarAluno;
    public event Action<int, string>? OnCriarInscricao;
    public event Action<int, string>? OnConcluirInscricao;
    public event Action<int, string, Nota>? OnClassificar;
    public event Action<int>? OnConsultarAluno;
    public event Action<int, string>? OnConsultarInscricao;
    public event Action<int, string>? OnConsultarClassificacao;

    // EVENTOS DA GESTÃO ACADÉMICA
    public event Action<int, string, string, string>? OnGuardarInstituicao;
    public event Action<int, int, string, string, string, string>? OnCriarCurso;
    public event Action<int, int, string, DateTime, DateTime, string>? OnCriarEdicao;
    public event Action<int, EstadoEdicao>? OnAlterarEstadoEdicao;
    public event Action<int>? OnConsultarInstituicao;
    public event Action<int>? OnConsultarCurso;
    public event Action<int>? OnConsultarEdicao;

    /// Subscrição aos eventos do Model
    /// Liga a View ao fluxo de notificações
    public void Subscrever(IModelEventos model)
    {
        model.Resultado += MostrarResultado;
        model.InscricaoCriada += MostrarInscricaoCriada;
        model.ClassificacaoCriada += MostrarClassificacaoCriada;
        model.AlunoConsultado += MostrarAluno;
        model.InscricaoConsultada += MostrarInscricaoConsultada;
        model.ClassificacaoConsultada += MostrarClassificacaoConsultada;
        model.InstituicaoGuardada += MostrarInstituicaoGuardada;
        model.CursoCriado += MostrarCursoCriado;
        model.EdicaoCriada += MostrarEdicaoCriada;
        model.EstadoEdicaoAlterado += MostrarEstadoEdicaoAlterado;
        model.InstituicaoConsultada += MostrarInstituicaoConsultada;
        model.CursoConsultado += MostrarCursoConsultado;
        model.EdicaoConsultada += MostrarEdicaoConsultada;
    }

    // Mantém compatibilidade com código já existente que use o nome antigo.
    public void Subscribir(IModelEventos model)
    {
        Subscrever(model);
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
        Console.WriteLine("8 - Guardar Instituicao");
        Console.WriteLine("9 - Criar Curso");
        Console.WriteLine("10 - Criar Edicao");
        Console.WriteLine("11 - Alterar Estado Edicao");
        Console.WriteLine("12 - Consultar Instituicao");
        Console.WriteLine("13 - Consultar Curso");
        Console.WriteLine("14 - Consultar Edicao");
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
            case "8": GuardarInstituicao(); break;
            case "9": CriarCurso(); break;
            case "10": CriarEdicao(); break;
            case "11": AlterarEstadoEdicao(); break;
            case "12": ConsultarInstituicao(); break;
            case "13": ConsultarCurso(); break;
            case "14": ConsultarEdicao(); break;
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
        if (!double.TryParse(Console.ReadLine(), out double valor))
        {
            Console.WriteLine("Nota invalida. Introduza um numero entre 0 e 20.");
            return;
        }

        try
        {
            Nota nota = new Nota(valor);
            OnClassificar?.Invoke(id, ed, nota);
        }
        catch (NotaInvalidaException e)
        {
            Console.WriteLine($"ERRO: {e.Message}");
        }
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

    // ================= GESTÃO ACADÉMICA =================

    void GuardarInstituicao()
    {
        Console.Write("Id Instituicao: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID da instituição inválido. Introduza um número inteiro.");
            return;
        }

        Console.Write("Nome: ");
        string nome = Console.ReadLine()!;

        Console.Write("Cidade: ");
        string cidade = Console.ReadLine()!;

        Console.Write("Pais: ");
        string pais = Console.ReadLine()!;

        OnGuardarInstituicao?.Invoke(id, nome, cidade, pais);
    }

    void CriarCurso()
    {
        Console.Write("Id Curso: ");
        if (!int.TryParse(Console.ReadLine(), out int idCurso))
        {
            Console.WriteLine("ID de Curso inválido. Introduza um número inteiro.");
            return;
        }

        Console.Write("Id Instituicao: ");
        if (!int.TryParse(Console.ReadLine(), out int idInstituicao))
        {
            Console.WriteLine("ID de Instituicao inválido. Introduza um número inteiro.");
            return;
        }

        Console.Write("Nome Curso: ");
        string nome = Console.ReadLine()!;

        Console.Write("Grau Academico: ");
        string grau = Console.ReadLine()!;

        Console.Write("Descricao: ");
        string descricao = Console.ReadLine()!;

        Console.Write("Estrutura: ");
        string estrutura = Console.ReadLine()!;

        OnCriarCurso?.Invoke(idCurso, idInstituicao,
            nome, grau, descricao, estrutura);
    }

    void CriarEdicao()
    {
        Console.Write("Id Edicao: ");

        if (!int.TryParse(Console.ReadLine(), out int idEdicao))
        {
            Console.WriteLine("ID da edição inválido.");
            return;
        }

        Console.Write("Id Curso: ");
        if (!int.TryParse(Console.ReadLine(), out int idCurso))
        {
            Console.WriteLine("ID do curso inválido.");
            return;
        }

        Console.Write("Ano Letivo: ");
        string ano = Console.ReadLine()!;

        Console.Write("Data Inicio (yyyy-mm-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime inicio))
        {
            Console.WriteLine("Data de início inválida.");
            return;
        }

        Console.Write("Data Fim (yyyy-mm-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime fim))
        {
            Console.WriteLine("Data de fim inválida.");
            return;
        }
        if (fim <= inicio)
        {
            Console.WriteLine("A data de fim deve ser posterior à data de início.");
            return;
        }

        Console.Write("Modalidade: ");
        string modalidade = Console.ReadLine()!;

        OnCriarEdicao?.Invoke(idEdicao, idCurso,
            ano, inicio, fim, modalidade);
    }

    void AlterarEstadoEdicao()
    {
        Console.Write("Id Edicao: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID da edição inválido.");
            return;
        }

        Console.WriteLine("0 - Planeada");
        Console.WriteLine("1 - Aberta");
        Console.WriteLine("2 - Encerrada");
        Console.WriteLine("3 - Cancelada");

        if (!int.TryParse(Console.ReadLine(), out int estado) || estado < 0 || estado > 3)
        {
            Console.WriteLine("Estado inválido.");
            return;
        }

        OnAlterarEstadoEdicao?.Invoke(id, (EstadoEdicao)estado);
    }

    void ConsultarInstituicao()
    {
        Console.Write("Id Instituicao: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID da instituição inválido.");
            return;
        }

        OnConsultarInstituicao?.Invoke(id);
    }

    void ConsultarCurso()
    {
        Console.Write("Id Curso: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID do curso inválido.");
            return;
        }

        OnConsultarCurso?.Invoke(id);
    }

    void ConsultarEdicao()
    {
        Console.Write("Id Edicao: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID da edição inválido.");
            return;
        }

        OnConsultarEdicao?.Invoke(id);
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
        Console.WriteLine($"Nota: {c.NotaValor}");
        Console.WriteLine($"Aprovado: {c.Aprovado}");
    }

    /// Aluno consultado — View vai buscar ao estado interno do Model
    private void MostrarAluno(object? sender, AlunoEventArgs e)
    {
        var a = e.Aluno;

        if (a == null)
        {
            Console.WriteLine("Aluno nao encontrado.");
            return;
        }

        Console.WriteLine($"\n=== ALUNO ===");
        Console.WriteLine($"ID:         {a.Id}");
        Console.WriteLine($"Nome:       {a.Nome}");
    }

    /// Inscricao consultada — View vai buscar ao estado interno do Model
    private void MostrarInscricaoConsultada(object? sender, InscricaoAlunoConsultadaEventArgs e)
    {
        var i = e.Inscricao;

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
        var c = e.Classificacao;

        if (c == null)
        {
            Console.WriteLine("Classificacao nao encontrada.");
            return;
        }

        Console.WriteLine($"\n=== CLASSIFICACAO ===");
        Console.WriteLine($"Nota:     {c.NotaValor}");
        Console.WriteLine($"Aprovado: {c.Aprovado}");
    }

    /// Mostra instituição guardada
    private void MostrarInstituicaoGuardada(object? sender, InstituicaoEventArgs e)
    {
        var i = e.Instituicao;

        Console.WriteLine("\n=== INSTITUICAO ===");
        Console.WriteLine($"ID: {i.IdInstituicao}");
        Console.WriteLine($"Nome: {i.NomeInstituicao}");
        Console.WriteLine($"Cidade: {i.Cidade}");
        Console.WriteLine($"Pais: {i.Pais}");
    }

    /// Mostra curso criado
    private void MostrarCursoCriado(object? sender, CursoEventArgs e)
    {
        var c = e.Curso;

        Console.WriteLine("\n=== CURSO ===");
        Console.WriteLine($"ID: {c.IdCurso}");
        Console.WriteLine($"Nome: {c.NomeCurso}");
        Console.WriteLine($"Grau: {c.GrauAcademico}");
    }

    /// Mostra edição criada
    private void MostrarEdicaoCriada(object? sender, EdicaoEventArgs e)
    {
        var ed = e.Edicao;

        Console.WriteLine("\n=== EDICAO ===");
        Console.WriteLine($"ID: {ed.IdEdicao}");
        Console.WriteLine($"Ano Letivo: {ed.AnoLetivo}");
        Console.WriteLine($"Estado: {ed.Estado}");
    }

    /// Mostra alteração de estado
    private void MostrarEstadoEdicaoAlterado(object? sender, EdicaoEventArgs e)
    {
        var ed = e.Edicao;

        Console.WriteLine("\n=== ESTADO ALTERADO ===");
        Console.WriteLine($"Edicao: {ed.IdEdicao}");
        Console.WriteLine($"Novo Estado: {ed.Estado}");
    }

    private void MostrarInstituicaoConsultada(object? sender, InstituicaoConsultadaEventArgs e)
    {
        var i = e.Instituicao;

        if (i == null)
        {
            Console.WriteLine("Instituição não encontrada.");
            return;
        }

        Console.WriteLine("\n=== INSTITUIÇÃO CONSULTADA ===");
        Console.WriteLine($"ID: {i.IdInstituicao}");
        Console.WriteLine($"Nome: {i.NomeInstituicao}");
        Console.WriteLine($"Cidade: {i.Cidade}");
        Console.WriteLine($"País: {i.Pais}");
    }

    private void MostrarCursoConsultado(object? sender, CursoConsultadoEventArgs e)
    {
        var c = e.Curso;

        if (c == null)
        {
            Console.WriteLine("Curso não encontrado.");
            return;
        }

        Console.WriteLine("\n=== CURSO CONSULTADO ===");
        Console.WriteLine($"ID: {c.IdCurso}");
        Console.WriteLine($"Nome: {c.NomeCurso}");
        Console.WriteLine($"Grau: {c.GrauAcademico}");
        Console.WriteLine($"Instituição: {c.Instituicao.NomeInstituicao}");
    }

    private void MostrarEdicaoConsultada(object? sender, EdicaoConsultadaEventArgs e)
    {
        var ed = e.Edicao;

        if (ed == null)
        {
            Console.WriteLine("Edição não encontrada.");
            return;
        }

        Console.WriteLine("\n=== EDIÇÃO CONSULTADA ===");
        Console.WriteLine($"ID: {ed.IdEdicao}");
        Console.WriteLine($"Instituição: {ed.Curso.Instituicao.NomeInstituicao}");
        Console.WriteLine($"Curso: {ed.Curso.NomeCurso}");
        Console.WriteLine($"Ano Letivo: {ed.AnoLetivo}");
        Console.WriteLine($"Data Início: {ed.DataInicio:yyyy-MM-dd}");
        Console.WriteLine($"Data Fim: {ed.DataFim:yyyy-MM-dd}");
        Console.WriteLine($"Modalidade: {ed.Modalidade}");
        Console.WriteLine($"Estado: {ed.Estado}");
    }


}