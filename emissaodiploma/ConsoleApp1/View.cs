using System;
using System.Globalization;
using System.IO;
using System.Text;

/// <summary>
/// View da aplicação SimDiploma.
/// 
/// Responsabilidades:
/// - Apresentar menus e mensagens ao utilizador;
/// - Recolher dados introduzidos no console;
/// - Validar apenas dados de entrada simples;
/// - Comunicar as opções do utilizador ao Controller através de eventos;
/// - Receber notificações do Model através de eventos e apresentar os resultados.
/// 
/// A View não contém regras de negócio e não chama diretamente o Model.
/// </summary>
public class View
{
    private const string FormatoData = "yyyy-MM-dd";
    private const string NomeFicheiroDiploma = "diploma.pdf";
    
    // ============================================================
    // EVENTOS ENVIADOS PARA O CONTROLLER
    // ============================================================
    public event Action<int, string>?         OnCriarAluno;
    public event Action<int, int>?         OnCriarInscricao;
    public event Action<int, int>?         OnConcluirInscricao;
    public event Action<int, int, double>? OnClassificar;

    public event Action<int>?                 OnConsultarAluno;
    public event Action<int, int>?         OnConsultarInscricao;
    public event Action<int, int>?         OnConsultarClassificacao;

    public event Action<int, string, string, string>?   OnGuardarInstituicao;
    public event Action<int, string, string, string>?   OnAlterarInstituicao;
    public event Action<int>?                           OnApagarInstituicao;
    public event Action<int>?                           OnConsultarInstituicao;

    public event Action<int, int, string, string, string, string>?  OnCriarCurso;
    public event Action<int, string, string, string, string>?       OnAlterarCurso;
    public event Action<int>?                                       OnApagarCurso;
    public event Action<int>?                                       OnConsultarCurso;

    public event Action<int, int, string, DateTime, DateTime, string>?  OnCriarEdicao;
    public event Action<int, string, DateTime, DateTime, string>?       OnAlterarEdicao;
    public event Action<int>?                                           OnApagarEdicao;
    public event Action<int, EstadoEdicao>?                             OnAlterarEstadoEdicao;
    public event Action<int>?                                           OnConsultarEdicao;

    public event Action<int, int>? OnEmitirDiploma;

    /// <summary>
    /// Construtor da View.
    /// Define a codificação para permitir melhor apresentação de acentos no console.
    /// </summary>
    public View()
    {
        Console.OutputEncoding = Encoding.UTF8;
    }

    // ============================================================
    // SUBSCRIÇÃO AOS EVENTOS DO MODEL
    // ============================================================

    /// <summary>
    /// Subscreve os eventos do Model para que a View possa apresentar os resultados.
    /// </summary>
    /// <param name="model">Interface de eventos disponibilizada pelo Model.</param>
    public void Subscrever(IModelEventos model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }
        
        model.Resultado += MostrarResultado;
        model.InscricaoCriada += MostrarInscricaoCriada;
        model.ClassificacaoCriada += MostrarClassificacaoCriada;

        model.AlunoConsultado += MostrarAluno;
        model.InscricaoConsultada += MostrarInscricaoConsultada;
        model.ClassificacaoConsultada += MostrarClassificacaoConsultada;

        model.InstituicaoGuardada += MostrarInstituicaoGuardada;
        model.InstituicaoAlterada += MostrarInstituicaoAlterada;

        model.CursoCriado += MostrarCursoCriado;
        model.CursoAlterado += MostrarCursoAlterado;

        model.EdicaoCriada += MostrarEdicaoCriada;
        model.EdicaoAlterada += MostrarEdicaoAlterada;
        model.EstadoEdicaoAlterado += MostrarEstadoEdicaoAlterado;

        model.InstituicaoConsultada += MostrarInstituicaoConsultada;
        model.CursoConsultado += MostrarCursoConsultado;
        model.EdicaoConsultada += MostrarEdicaoConsultada;

        model.OnValidacao += MostrarValidacao;
        model.OnDiplomaEmitido += MostrarDiploma;
    }

    // ============================================================
    // MENU PRINCIPAL
    // ============================================================

    /// <summary>
    /// Apresenta o menu principal.
    /// </summary>
    /// <returns>
    /// true se o utilizador escolher sair;
    /// false se a aplicação deve continuar.
    /// </returns>
    public bool Menu()
    {
        EscreverTitulo("MENU PRINCIPAL");

        Console.WriteLine("1  - Criar Aluno");
        Console.WriteLine("2  - Inscrever Aluno");
        Console.WriteLine("3  - Concluir Inscrição");
        Console.WriteLine("4  - Classificação");
        Console.WriteLine("5  - Consultar Aluno");
        Console.WriteLine("6  - Consultar Inscrição");
        Console.WriteLine("7  - Consultar Classificação");
        Console.WriteLine("8  - Gestão de Instituições");
        Console.WriteLine("9  - Gestão de Cursos");
        Console.WriteLine("10 - Gestão de Edições");
        Console.WriteLine("11 - Emitir Diploma");
        Console.WriteLine("0  - Sair");

        switch (LerOpcao())
        {
            case "1": CriarAluno(); return false;
            case "2": CriarInscricao(); return false;
            case "3": ConcluirInscricao(); return false;
            case "4": Classificar(); return false;
            case "5": ConsultarAluno(); return false;
            case "6": ConsultarInscricao(); return false;
            case "7": ConsultarClassificacao(); return false;
            case "8": MenuInstituicoes(); return false;
            case "9": MenuCursos(); return false;
            case "10": MenuEdicoes(); return false;
            case "11": PedirEmissaoDiploma(); return false;
            case "0": Console.WriteLine("O sistema foi encerrado."); return true;
            default:
                Console.WriteLine("Opção inválida. Tente novamente.");
                return false;
        }
    }

    // ============================================================
    // MENU DE INSTITUIÇÕES
    // ============================================================

    private void MenuInstituicoes()
    {
        bool voltar = false;

        while (!voltar)
        {
            EscreverTitulo("GESTÃO DE INSTITUIÇÕES");

            Console.WriteLine("1 - Guardar instituição");
            Console.WriteLine("2 - Alterar instituição");
            Console.WriteLine("3 - Consultar instituição");
            Console.WriteLine("4 - Apagar instituição");
            Console.WriteLine("0 - Voltar");

            switch (LerOpcao())
            {
                case "1":
                    GuardarInstituicao();
                    break;

                case "2":
                    AlterarInstituicao();
                    break;

                case "3":
                    ConsultarInstituicao();
                    break;

                case "4":
                    ApagarInstituicao();
                    break;

                case "0":
                    voltar = true;
                    break;

                default:
                    MostrarResultado(false, "Opção inválida.");
                    break;
            }
        }
    }

    // ============================================================
    // MENU DE CURSOS
    // ============================================================

    private void MenuCursos()
    {
        bool voltar = false;

        while (!voltar)
        {
            EscreverTitulo("GESTÃO DE CURSOS");

            Console.WriteLine("1 - Criar curso");
            Console.WriteLine("2 - Alterar curso");
            Console.WriteLine("3 - Consultar curso");
            Console.WriteLine("4 - Apagar curso");
            Console.WriteLine("0 - Voltar");

            switch (LerOpcao())
            {
                case "1":
                    CriarCurso();
                    break;

                case "2":
                    AlterarCurso();
                    break;

                case "3":
                    ConsultarCurso();
                    break;

                case "4":
                    ApagarCurso();
                    break;

                case "0":
                    voltar = true;
                    break;

                default:
                    MostrarResultado(false, "Opção inválida.");
                    break;
            }
        }
    }

    // ============================================================
    // MENU DE EDIÇÕES
    // ============================================================

    private void MenuEdicoes()
    {
        bool voltar = false;

        while (!voltar)
        {
            EscreverTitulo("GESTÃO DE EDIÇÕES");

            Console.WriteLine("1 - Criar edição");
            Console.WriteLine("2 - Alterar edição");
            Console.WriteLine("3 - Alterar estado da edição");
            Console.WriteLine("4 - Consultar edição");
            Console.WriteLine("5 - Apagar edição");
            Console.WriteLine("0 - Voltar");

            switch (LerOpcao())
            {
                case "1":
                    CriarEdicao();
                    break;

                case "2":
                    AlterarEdicao();
                    break;

                case "3":
                    AlterarEstadoEdicao();
                    break;

                case "4":
                    ConsultarEdicao();
                    break;

                case "5":
                    ApagarEdicao();
                    break;

                case "0":
                    voltar = true;
                    break;

                default:
                    MostrarResultado(false, "Opção inválida.");
                    break;
            }
        }
    }

    // ============================================================
    // OPERAÇÕES DE ALUNOS, INSCRIÇÕES E CLASSIFICAÇÕES
    // ============================================================

    private void CriarAluno()
    {
        if (!LerInteiro("ID do aluno", out int id))
        {
            return;
        }

        if (!LerTextoObrigatorio("Nome do aluno", out string nome))
        {
            return;
        }

        OnCriarAluno?.Invoke(id, nome);
    }

    private void CriarInscricao()
    {
        if (!LerInteiro("ID do aluno", out int alunoId))
        {
            return;
        }

        if (!LerInteiro("ID da edição", out int edicaoId))
        {
            return;
        }

        OnCriarInscricao?.Invoke(alunoId, edicaoId);
    }

    private void ConcluirInscricao()
    {
        if (!LerInteiro("ID do aluno", out int alunoId))
        {
            return;
        }

        if (!LerInteiro("ID da edição", out int edicaoId))
        {
            return;
        }

        OnConcluirInscricao?.Invoke(alunoId, edicaoId);
    }

    private void Classificar()
    {
        if (!LerInteiro("ID do aluno", out int alunoId))
        {
            return;
        }

        if (!LerInteiro("ID da edição", out int edicaoId))
        {
            return;
        }

        if (!LerDouble("Nota", 0, 20, out double valorNota))
        {
            return;
        }

        OnClassificar?.Invoke(alunoId, edicaoId, valorNota);
    }

    private void ConsultarAluno()
    {
        if (!LerInteiro("ID do aluno", out int id))
        {
            return;
        }

        OnConsultarAluno?.Invoke(id);
    }

    private void ConsultarInscricao()
    {
        if (!LerInteiro("ID do aluno", out int alunoId))
        {
            return;
        }

        if (!LerInteiro("ID da edição", out int edicaoId))
        {
            return;
        }

        OnConsultarInscricao?.Invoke(alunoId, edicaoId);
    }

    private void ConsultarClassificacao()
    {
        if (!LerInteiro("ID do aluno", out int alunoId))
        {
            return;
        }

        if (!LerInteiro("ID da edição", out int edicaoId))
        {
            return;
        }

        OnConsultarClassificacao?.Invoke(alunoId, edicaoId);
    }

    // ============================================================
    // OPERAÇÕES DE INSTITUIÇÕES
    // ============================================================

    private void GuardarInstituicao()
    {
        if (!LerInteiro("ID da instituição", out int idInstituicao))
        {
            return;
        }

        if (!LerTextoObrigatorio("Nome da instituição", out string nome))
        {
            return;
        }

        if (!LerTextoObrigatorio("Cidade", out string cidade))
        {
            return;
        }

        if (!LerTextoObrigatorio("País", out string pais))
        {
            return;
        }

        OnGuardarInstituicao?.Invoke(idInstituicao, nome, cidade, pais);
    }

    private void AlterarInstituicao()
    {
        if (!LerInteiro("ID da instituição", out int idInstituicao))
        {
            return;
        }

        if (!LerTextoObrigatorio("Novo nome da instituição", out string nome))
        {
            return;
        }

        if (!LerTextoObrigatorio("Nova cidade", out string cidade))
        {
            return;
        }

        if (!LerTextoObrigatorio("Novo país", out string pais))
        {
            return;
        }

        OnAlterarInstituicao?.Invoke(idInstituicao, nome, cidade, pais);
    }

    private void ApagarInstituicao()
    {
        if (!LerInteiro("ID da instituição", out int idInstituicao))
        {
            return;
        }

        if (!ConfirmarAcao("Confirma que pretende apagar esta instituição?"))
        {
            MostrarInformacao("Operação cancelada.");
            return;
        }

        OnApagarInstituicao?.Invoke(idInstituicao);
    }

    private void ConsultarInstituicao()
    {
        if (!LerInteiro("ID da instituição", out int idInstituicao))
        {
            return;
        }

        OnConsultarInstituicao?.Invoke(idInstituicao);
    }

    // ============================================================
    // OPERAÇÕES DE CURSOS
    // ============================================================

    private void CriarCurso()
    {
        if (!LerInteiro("ID do curso", out int idCurso))
        {
            return;
        }

        if (!LerInteiro("ID da instituição", out int idInstituicao))
        {
            return;
        }

        if (!LerTextoObrigatorio("Nome do curso", out string nomeCurso))
        {
            return;
        }

        if (!LerTextoObrigatorio("Grau académico", out string grauAcademico))
        {
            return;
        }

        if (!LerTextoObrigatorio("Descrição", out string descricao))
        {
            return;
        }

        if (!LerTextoObrigatorio("Estrutura", out string estrutura))
        {
            return;
        }

        OnCriarCurso?.Invoke(
            idCurso,
            idInstituicao,
            nomeCurso,
            grauAcademico,
            descricao,
            estrutura);
    }

    private void AlterarCurso()
    {
        if (!LerInteiro("ID do curso", out int idCurso))
        {
            return;
        }

        if (!LerTextoObrigatorio("Novo nome do curso", out string nomeCurso))
        {
            return;
        }

        if (!LerTextoObrigatorio("Novo grau académico", out string grauAcademico))
        {
            return;
        }

        if (!LerTextoObrigatorio("Nova descrição", out string descricao))
        {
            return;
        }

        if (!LerTextoObrigatorio("Nova estrutura", out string estrutura))
        {
            return;
        }

        OnAlterarCurso?.Invoke(
            idCurso,
            nomeCurso,
            grauAcademico,
            descricao,
            estrutura);
    }

    private void ApagarCurso()
    {
        if (!LerInteiro("ID do curso", out int idCurso))
        {
            return;
        }

        if (!ConfirmarAcao("Confirma que pretende apagar este curso?"))
        {
            MostrarInformacao("Operação cancelada.");
            return;
        }

        OnApagarCurso?.Invoke(idCurso);
    }

    private void ConsultarCurso()
    {
        if (!LerInteiro("ID do curso", out int idCurso))
        {
            return;
        }

        OnConsultarCurso?.Invoke(idCurso);
    }

    // ============================================================
    // OPERAÇÕES DE EDIÇÕES
    // ============================================================

    private void CriarEdicao()
    {
        if (!LerInteiro("ID da edição", out int idEdicao))
        {
            return;
        }

        if (!LerInteiro("ID do curso", out int idCurso))
        {
            return;
        }

        if (!LerTextoObrigatorio("Ano letivo", out string anoLetivo))
        {
            return;
        }

        if (!LerData("Data de início", out DateTime dataInicio))
        {
            return;
        }

        if (!LerData("Data de fim", out DateTime dataFim))
        {
            return;
        }

        if (!ValidarPeriodo(dataInicio, dataFim))
        {
            return;
        }

        if (!LerTextoObrigatorio("Modalidade", out string modalidade))
        {
            return;
        }

        OnCriarEdicao?.Invoke(
            idEdicao,
            idCurso,
            anoLetivo,
            dataInicio,
            dataFim,
            modalidade);
    }

    private void AlterarEdicao()
    {
        if (!LerInteiro("ID da edição", out int idEdicao))
        {
            return;
        }

        if (!LerTextoObrigatorio("Ano letivo", out string anoLetivo))
        {
            return;
        }

        if (!LerData("Data de início", out DateTime dataInicio))
        {
            return;
        }

        if (!LerData("Data de fim", out DateTime dataFim))
        {
            return;
        }

        if (!ValidarPeriodo(dataInicio, dataFim))
        {
            return;
        }

        if (!LerTextoObrigatorio("Modalidade", out string modalidade))
        {
            return;
        }

        OnAlterarEdicao?.Invoke(
            idEdicao,
            anoLetivo,
            dataInicio,
            dataFim,
            modalidade);
    }

    private void AlterarEstadoEdicao()
    {
        if (!LerInteiro("ID da edição", out int idEdicao))
        {
            return;
        }

        if (!LerEstadoEdicao(out EstadoEdicao estado))
        {
            return;
        }

        OnAlterarEstadoEdicao?.Invoke(idEdicao, estado);
    }

    private void ApagarEdicao()
    {
        if (!LerInteiro("ID da edição", out int idEdicao))
        {
            return;
        }

        if (!ConfirmarAcao("Confirma que pretende apagar esta edição?"))
        {
            MostrarInformacao("Operação cancelada.");
            return;
        }

        OnApagarEdicao?.Invoke(idEdicao);
    }

    /// <summary>
    /// Apresenta os dados de uma edição alterada.
    /// </summary>
    private void MostrarEdicaoAlterada(object? sender, EdicaoEventArgs e)
    {
        var edicao = e.Edicao;

        EscreverTitulo("EDIÇÃO ALTERADA");
        Console.WriteLine($"ID:          {edicao.IdEdicao}");
        Console.WriteLine($"Curso:       {edicao.Curso.NomeCurso}");
        Console.WriteLine($"Ano letivo:  {edicao.AnoLetivo}");
        Console.WriteLine($"Data início: {edicao.DataInicio:yyyy-MM-dd}");
        Console.WriteLine($"Data fim:    {edicao.DataFim:yyyy-MM-dd}");
        Console.WriteLine($"Modalidade:  {edicao.Modalidade}");
        Console.WriteLine($"Estado:      {edicao.Estado}");
    }

    private void ConsultarEdicao()
    {
        if (!LerInteiro("ID da edição", out int idEdicao))
        {
            return;
        }

        OnConsultarEdicao?.Invoke(idEdicao);
    }

    // ============================================================
    // EMISSÃO DE DIPLOMAS
    // ============================================================

    private void PedirEmissaoDiploma()
    {
        if (!LerInteiro("ID do aluno", out int alunoId))
        {
            return;
        }

        if (!LerInteiro("ID da edição", out int idEdicao))
        {
            return;
        }

        OnEmitirDiploma?.Invoke(alunoId, idEdicao);
    }

    // ============================================================
    // MÉTODOS AUXILIARES DE LEITURA E VALIDAÇÃO
    // ============================================================

    private static string LerOpcao()
    {
        Console.Write("Opção: ");
        return (Console.ReadLine() ?? string.Empty).Trim();
    }

    private bool LerInteiro(string campo, out int valor)
    {
        Console.Write($"{campo}: ");
        string texto = Console.ReadLine() ?? string.Empty;

        if (!int.TryParse(texto, out valor))
        {
            MostrarResultado(false, $"{campo} inválido. Introduza um número inteiro.");
            return false;
        }

        return true;
    }

    private bool LerTextoObrigatorio(string campo, out string valor)
    {
        Console.Write($"{campo}: ");
        valor = (Console.ReadLine() ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(valor))
        {
            MostrarResultado(false, $"{campo} não pode ser vazio.");
            return false;
        }

        return true;
    }

    private bool LerDouble(string campo, double minimo, double maximo, out double valor)
    {
        Console.Write($"{campo} ({minimo}-{maximo}): ");
        string texto = (Console.ReadLine() ?? string.Empty).Trim();

        bool valido =
            double.TryParse(texto, NumberStyles.Float, CultureInfo.CurrentCulture, out valor) ||
            double.TryParse(texto.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out valor);

        if (!valido)
        {
            MostrarResultado(false, $"{campo} inválido. Introduza um número.");
            return false;
        }

        if (valor < minimo || valor > maximo)
        {
            MostrarResultado(false, $"{campo} deve estar entre {minimo} e {maximo}.");
            return false;
        }

        return true;
    }

    private bool LerData(string campo, out DateTime data)
    {
        Console.Write($"{campo} ({FormatoData}): ");
        string texto = (Console.ReadLine() ?? string.Empty).Trim();

        bool valido = DateTime.TryParseExact(
            texto,
            FormatoData,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out data);

        if (!valido)
        {
            MostrarResultado(false, $"{campo} inválida. Use o formato {FormatoData}.");
            return false;
        }

        data = data.Date;
        return true;
    }

    private bool LerEstadoEdicao(out EstadoEdicao estado)
    {
        Console.WriteLine("0 - Planeada");
        Console.WriteLine("1 - Aberta");
        Console.WriteLine("2 - Encerrada");
        Console.WriteLine("3 - Cancelada");

        Console.Write("Estado: ");
        string texto = Console.ReadLine() ?? string.Empty;

        if (!int.TryParse(texto, out int valor) || valor < 0 || valor > 3)
        {
            estado = default;
            MostrarResultado(false, "Estado inválido.");
            return false;
        }

        estado = (EstadoEdicao)valor;
        return true;
    }

    private bool ValidarPeriodo(DateTime dataInicio, DateTime dataFim)
    {
        if (dataFim <= dataInicio)
        {
            MostrarResultado(false, "A data de fim deve ser posterior à data de início.");
            return false;
        }

        return true;
    }

    private static bool ConfirmarAcao(string mensagem)
    {
        Console.Write($"{mensagem} (s/n): ");
        string resposta = (Console.ReadLine() ?? string.Empty).Trim().ToLowerInvariant();

        return resposta == "s" || resposta == "sim";
    }

    // ============================================================
    // MÉTODOS AUXILIARES DE APRESENTAÇÃO
    // ============================================================

    private static void EscreverTitulo(string titulo)
    {
        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine(titulo);
        Console.WriteLine("==================================================");
    }

    private static void MostrarInformacao(string mensagem)
    {
        Console.WriteLine($"INFO: {mensagem}");
    }

    /// <summary>
    /// Apresenta o resultado genérico de uma operação.
    /// Este método também é usado pelo Controller para mostrar erros.
    /// </summary>
    public void MostrarResultado(bool sucesso, string mensagem)
    {
        string prefixo = sucesso ? "OK" : "ERRO";
        Console.WriteLine($"{prefixo}: {mensagem}");
    }

    private void MostrarResultado(object? sender, ResultadoEventArgs e)
    {
        MostrarResultado(e.Sucesso, e.Mensagem);
    }

    // ============================================================
    // OUTPUT DE ALUNOS, INSCRIÇÕES E CLASSIFICAÇÕES
    // ============================================================

    private void MostrarInscricaoCriada(object? sender, InscricaoAlunoEventArgs e)
    {
        var inscricao = e.Inscricao;

        EscreverTitulo("INSCRIÇÃO CRIADA");
        Console.WriteLine($"Aluno:             {inscricao.AlunoId}");
        Console.WriteLine($"Edição:            {inscricao.EdicaoId}");
        Console.WriteLine($"Ativa:             {inscricao.Ativa}");
        Console.WriteLine($"Tem classificação: {inscricao.TemClassificacao}");
    }

    private void MostrarClassificacaoCriada(object? sender, ClassificacaoEventArgs e)
    {
        var classificacao = e.Classificacao;

        EscreverTitulo("CLASSIFICAÇÃO LANÇADA");
        Console.WriteLine($"Nota:     {classificacao.NotaValor:0.##}");
        Console.WriteLine($"Aprovado: {classificacao.Aprovado}");
    }

    private void MostrarAluno(object? sender, AlunoEventArgs e)
    {
        var aluno = e.Aluno;

        if (aluno == null)
        {
            MostrarResultado(false, "Aluno não encontrado.");
            return;
        }

        EscreverTitulo("ALUNO");
        Console.WriteLine($"ID:   {aluno.Id}");
        Console.WriteLine($"Nome: {aluno.Nome}");
    }

    private void MostrarInscricaoConsultada(object? sender, InscricaoAlunoConsultadaEventArgs e)
    {
        var inscricao = e.Inscricao;

        if (inscricao == null)
        {
            MostrarResultado(false, "Inscrição não encontrada.");
            return;
        }

        EscreverTitulo("INSCRIÇÃO");
        Console.WriteLine($"Aluno:             {inscricao.AlunoId}");
        Console.WriteLine($"Edição:            {inscricao.EdicaoId}");
        Console.WriteLine($"Ativa:             {inscricao.Ativa}");
        Console.WriteLine($"Tem classificação: {inscricao.TemClassificacao}");
    }

    private void MostrarClassificacaoConsultada(object? sender, ClassificacaoConsultadaEventArgs e)
    {
        var classificacao = e.Classificacao;

        if (classificacao == null)
        {
            MostrarResultado(false, "Classificação não encontrada.");
            return;
        }

        EscreverTitulo("CLASSIFICAÇÃO");
        Console.WriteLine($"Nota:     {classificacao.NotaValor:0.##}");
        Console.WriteLine($"Aprovado: {classificacao.Aprovado}");
    }

    // ============================================================
    // OUTPUT DE INSTITUIÇÕES, CURSOS E EDIÇÕES
    // ============================================================

    private void MostrarInstituicaoGuardada(object? sender, InstituicaoEventArgs e)
    {
        var instituicao = e.Instituicao;

        EscreverTitulo("INSTITUIÇÃO GUARDADA");
        Console.WriteLine($"ID:     {instituicao.IdInstituicao}");
        Console.WriteLine($"Nome:   {instituicao.NomeInstituicao}");
        Console.WriteLine($"Cidade: {instituicao.Cidade}");
        Console.WriteLine($"País:   {instituicao.Pais}");
    }

    private void MostrarInstituicaoAlterada(object? sender, InstituicaoEventArgs e)
    {
        var instituicao = e.Instituicao;

        EscreverTitulo("INSTITUIÇÃO ALTERADA");
        Console.WriteLine($"ID:     {instituicao.IdInstituicao}");
        Console.WriteLine($"Nome:   {instituicao.NomeInstituicao}");
        Console.WriteLine($"Cidade: {instituicao.Cidade}");
        Console.WriteLine($"País:   {instituicao.Pais}");
    }

    private void MostrarCursoCriado(object? sender, CursoEventArgs e)
    {
        var curso = e.Curso;

        EscreverTitulo("CURSO CRIADO");
        Console.WriteLine($"ID:   {curso.IdCurso}");
        Console.WriteLine($"Nome: {curso.NomeCurso}");
        Console.WriteLine($"Grau: {curso.GrauAcademico}");
    }

    private void MostrarCursoAlterado(object? sender, CursoEventArgs e)
    {
        var curso = e.Curso;

        EscreverTitulo("CURSO ALTERADO");
        Console.WriteLine($"ID:   {curso.IdCurso}");
        Console.WriteLine($"Nome: {curso.NomeCurso}");
        Console.WriteLine($"Grau: {curso.GrauAcademico}");
    }

    private void MostrarEdicaoCriada(object? sender, EdicaoEventArgs e)
    {
        var edicao = e.Edicao;

        EscreverTitulo("EDIÇÃO CRIADA");
        Console.WriteLine($"ID:         {edicao.IdEdicao}");
        Console.WriteLine($"Ano letivo: {edicao.AnoLetivo}");
        Console.WriteLine($"Estado:     {edicao.Estado}");
    }

    private void MostrarEstadoEdicaoAlterado(object? sender, EdicaoEventArgs e)
    {
        var edicao = e.Edicao;

        EscreverTitulo("ESTADO DA EDIÇÃO ALTERADO");
        Console.WriteLine($"Edição:      {edicao.IdEdicao}");
        Console.WriteLine($"Novo estado: {edicao.Estado}");
    }

    private void MostrarInstituicaoConsultada(object? sender, InstituicaoConsultadaEventArgs e)
    {
        var instituicao = e.Instituicao;

        if (instituicao == null)
        {
            MostrarResultado(false, "Instituição não encontrada.");
            return;
        }

        EscreverTitulo("INSTITUIÇÃO CONSULTADA");
        Console.WriteLine($"ID:     {instituicao.IdInstituicao}");
        Console.WriteLine($"Nome:   {instituicao.NomeInstituicao}");
        Console.WriteLine($"Cidade: {instituicao.Cidade}");
        Console.WriteLine($"País:   {instituicao.Pais}");
    }

    private void MostrarCursoConsultado(object? sender, CursoConsultadoEventArgs e)
    {
        var curso = e.Curso;

        if (curso == null)
        {
            MostrarResultado(false, "Curso não encontrado.");
            return;
        }

        EscreverTitulo("CURSO CONSULTADO");
        Console.WriteLine($"ID:          {curso.IdCurso}");
        Console.WriteLine($"Nome:        {curso.NomeCurso}");
        Console.WriteLine($"Grau:        {curso.GrauAcademico}");
        Console.WriteLine($"Instituição: {curso.Instituicao?.NomeInstituicao ?? "Não definida"}");
    }

    private void MostrarEdicaoConsultada(object? sender, EdicaoConsultadaEventArgs e)
    {
        var edicao = e.Edicao;

        if (edicao == null)
        {
            MostrarResultado(false, "Edição não encontrada.");
            return;
        }

        EscreverTitulo("EDIÇÃO CONSULTADA");
        Console.WriteLine($"ID:          {edicao.IdEdicao}");
        Console.WriteLine($"Instituição: {edicao.Curso?.Instituicao?.NomeInstituicao ?? "Não definida"}");
        Console.WriteLine($"Curso:       {edicao.Curso?.NomeCurso ?? "Não definido"}");
        Console.WriteLine($"Ano letivo:  {edicao.AnoLetivo}");
        Console.WriteLine($"Data início: {edicao.DataInicio:yyyy-MM-dd}");
        Console.WriteLine($"Data fim:    {edicao.DataFim:yyyy-MM-dd}");
        Console.WriteLine($"Modalidade:  {edicao.Modalidade}");
        Console.WriteLine($"Estado:      {edicao.Estado}");
    }

    // ============================================================
    // OUTPUT DE VALIDAÇÃO E DIPLOMA
    // ============================================================

    private void MostrarValidacao(object? sender, ValidacaoEventArgs e)
    {
        Console.WriteLine($"VALIDAÇÃO: {e.Mensagem}");
    }

    private void MostrarDiploma(object? sender, DiplomaEmitidoEventArgs e)
    {
        if (e.PdfBytes == null || e.PdfBytes.Length == 0)
        {
            MostrarResultado(false, "O diploma foi emitido, mas não contém dados.");
            return;
        }

        try
        {
            string caminho = Path.Combine(Environment.CurrentDirectory, NomeFicheiroDiploma);
            File.WriteAllBytes(caminho, e.PdfBytes);

            EscreverTitulo("DIPLOMA EMITIDO");
            Console.WriteLine("Diploma gerado com sucesso.");
            Console.WriteLine($"Ficheiro: {caminho}");
            Console.WriteLine($"Tamanho:  {e.PdfBytes.Length} bytes");
        }
        catch (IOException ex)
        {
            MostrarResultado(false, $"Diploma gerado, mas não foi possível gravar o ficheiro: {ex.Message}");
        }
        catch (UnauthorizedAccessException ex)
        {
            MostrarResultado(false, $"Diploma gerado, mas sem permissões para gravar o ficheiro: {ex.Message}");
        }
    }
}