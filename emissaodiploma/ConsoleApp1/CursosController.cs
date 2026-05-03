public class CursosController
{
    private readonly CursosModel _model;

    // Construtor com apenas 1 parâmetro
    public CursosController(CursosModel model)
    {
        _model = model;
    }

    public void GuardarInstituicao(string nome)
        => _model.GuardarInstituicao(nome);

    public void CriarCurso(string codigoCurso)
        => _model.CriarCurso(codigoCurso);

    public void CriarEdicao(string codigoCurso, string codigoEdicao)
        => _model.CriarEdicao(codigoCurso, codigoEdicao);

    public void AlterarEstadoEdicao(string codigoCurso, string codigoEdicao, EstadoEdicao novoEstado)
        => _model.AlterarEstadoEdicao(codigoCurso, codigoEdicao, novoEstado);

    public bool ExisteCurso(string codigoCurso)
        => _model.ExisteCurso(codigoCurso);

    public bool ExisteEdicao(string codigoCurso, string codigoEdicao)
        => _model.ExisteEdicao(codigoCurso, codigoEdicao);
}
