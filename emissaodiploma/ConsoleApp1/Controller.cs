/// CONTROLLER
/// Responsável por coordenar a interação.
/// 
/// Função:
/// - Recebe input
/// - Invoca o Model
/// 
/// IMPORTANTE:
/// - Não contém lógica de negócio
/// - Não processa dados
public class Controller
{
    private readonly Model _model;
    private readonly View _view;

    public Controller(Model model, View view)
    {
        _model = model;
        _view = view;

        LigarEventosDaView(view);
    }

    private void LigarEventosDaView(View view)
    {
        view.OnCriarAluno += CriarAluno;
        view.OnCriarInscricao += CriarInscricao;
        view.OnConcluirInscricao += ConcluirInscricao;
        view.OnClassificar += Classificar;
        view.OnConsultarAluno += ConsultarAluno;
        view.OnConsultarInscricao += ConsultarInscricao;
        view.OnConsultarClassificacao += ConsultarClassificacao;
        view.OnEmitirDiploma += EmitirDiploma;
    }

    private void TratarErro(Exception e)
    {
        _view.MostrarResultado(false, e.Message);
    }

    public void CriarAluno(int id, string nome)
    {
        try
        {
            _model.RegistarAluno(id, nome);
        }
        catch (Exception e)
        {
            TratarErro(e);
        }
    }

    public void CriarInscricao(int alunoId, string edicao)
    {
        try
        {
            _model.InscreverAluno(alunoId, edicao);
        }
        catch (Exception e)
        {
            TratarErro(e);
        }
    }

    public void ConcluirInscricao(int alunoId, string edicao)
    {
        try
        {
            _model.ConcluirInscricao(alunoId, edicao);
        }
        catch (Exception e)
        {
            TratarErro(e);
        }
    }

    public void Classificar(int alunoId, string edicao, Nota nota)
    {
        try
        {
            Nota nota = new Nota(valorNota);
            _model.LancarClassificacao(alunoId, edicao, nota);
        }
        catch (Exception e)
        {
            TratarErro(e);
        }
    }

    public void ConsultarAluno(int id)
    {
        try
        {
            _model.ConsultarAluno(id);
        }
        catch (Exception e)
        {
            TratarErro(e);
        }
    }

    public void ConsultarInscricao(int alunoId, string edicao)
    {
        try
        {
            _model.ConsultarInscricao(alunoId, edicao);
        }
        catch (Exception e)
        {
            TratarErro(e);
        }
    }

    public void ConsultarClassificacao(int alunoId, string edicao)
    {
        try
        {
            _model.ConsultarClassificacao(alunoId, edicao);
        }
        catch (Exception e)
        {
            TratarErro(e);
        }
    }

    /// Método que inicia o processo de emissão
    public void EmitirDiploma(string nomeAluno, string curso)
    {
        try
        {
            _model.EmitirDiploma(nomeAluno, curso);
        }
        catch (Exception e)
        {
            TratarErro(e);
        }
    }
}