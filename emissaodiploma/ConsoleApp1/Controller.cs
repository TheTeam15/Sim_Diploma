using System;

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
        view.OnGuardarInstituicao += GuardarInstituicao;
        view.OnAlterarInstituicao += AlterarInstituicao;
        view.OnApagarInstituicao += ApagarInstituicao;
        view.OnCriarCurso += CriarCurso;
        view.OnAlterarCurso += AlterarCurso;
        view.OnApagarCurso += ApagarCurso;
        view.OnCriarEdicao += CriarEdicao;
        view.OnAlterarEstadoEdicao += AlterarEstadoEdicao;
        view.OnAlterarEdicao += AlterarEdicao;
        view.OnApagarEdicao += ApagarEdicao;
        view.OnConsultarInstituicao += ConsultarInstituicao;
        view.OnConsultarCurso += ConsultarCurso;
        view.OnConsultarEdicao += ConsultarEdicao;
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

    public void Classificar(int alunoId, string edicao, double valorNota)
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

    // GESTÃO DE INSTITUIÇÕES 

    public void GuardarInstituicao(int idInstituicao, string nomeInstituicao, string cidade, string pais)
    {
        try
        {
            _model.GuardarInstituicao(idInstituicao, nomeInstituicao, cidade, pais);
        }
        catch (Exception e)
        {
            TratarErro(e);
        }
    }

    public void AlterarInstituicao(int idInstituicao, string nomeInstituicao, string cidade, string pais)
    {
        try
        {
            _model.AlterarInstituicao(idInstituicao, nomeInstituicao, cidade, pais);
        }
        catch (Exception e)
        {
            TratarErro(e);
        }
    }

    public void ApagarInstituicao(int idInstituicao)
    {
        try
        {
            _model.ApagarInstituicao(idInstituicao);
        }
        catch (Exception e)
        {
            TratarErro(e);
        }
    }

    public void ConsultarInstituicao(int idInstituicao)
    {
        try
        {
            _model.ConsultarInstituicao(idInstituicao);
        }
        catch (Exception e)
        {
            TratarErro(e);
        }
    }

    // GESTÃO DE CURSOS 

    public void CriarCurso(
    int idCurso,
    int idInstituicao,
    string nomeCurso,
    string grauAcademico,
    string descricao,
    string estrutura)
    {
        try
        {
            _model.CriarCurso(
                idCurso,
                idInstituicao,
                nomeCurso,
                grauAcademico,
                descricao,
                estrutura);
        }
        catch (Exception e)
        {
            TratarErro(e);
        }
    }

    public void AlterarCurso(
        int idCurso,
        string nomeCurso,
        string grauAcademico,
        string descricao,
        string estrutura)
    {
        try
        {
            _model.AlterarCurso(
                idCurso,
                nomeCurso,
                grauAcademico,
                descricao,
                estrutura);
        }
        catch (Exception e)
        {
            TratarErro(e);
        }
    }

    public void ApagarCurso(int idCurso)
    {
        try
        {
            _model.ApagarCurso(idCurso);
        }
        catch (Exception e)
        {
            TratarErro(e);
        }
    }

    public void ConsultarCurso(int idCurso)
    {
        try
        {
            _model.ConsultarCurso(idCurso);
        }
        catch (Exception e)
        {
            TratarErro(e);
        }
    }


    // GESTÃO DE EDIÇÕES

    public void CriarEdicao(
        int idEdicao,
        int idCurso,
        string anoLetivo,
        DateTime dataInicio,
        DateTime dataFim,
        string modalidade)
    {
        try
        {
            _model.CriarEdicao(
                idEdicao,
                idCurso,
                anoLetivo,
                dataInicio,
                dataFim,
                modalidade);
        }
        catch (Exception e)
        {
            TratarErro(e);
        }
    }

    public void AlterarEdicao(
        int idEdicao,
        string anoLetivo,
        DateTime dataInicio,
        DateTime dataFim,
        string modalidade)
    {
        try
        {
            _model.AlterarEdicao(
                idEdicao,
                anoLetivo,
                dataInicio,
                dataFim,
                modalidade);
        }
        catch (Exception e)
        {
            TratarErro(e);
        }
    }

    public void AlterarEstadoEdicao(int idEdicao, EstadoEdicao novoEstado)
    {
        try
        {
            _model.AlterarEstadoEdicao(idEdicao, novoEstado);
        }
        catch (Exception e)
        {
            TratarErro(e);
        }
    }

    public void ApagarEdicao(int idEdicao)
    {
        try
        {
            _model.ApagarEdicao(idEdicao);
        }
        catch (Exception e)
        {
            TratarErro(e);
        }
    }

    public void ConsultarEdicao(int idEdicao)
    {
        try
        {
            _model.ConsultarEdicao(idEdicao);
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