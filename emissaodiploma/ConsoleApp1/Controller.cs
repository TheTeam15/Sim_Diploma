namespace SimDiplomaMVC
{
    /// <summary>
    /// CONTROLLER
    /// Responsável por coordenar a interação.
    ///
    /// Função:
    /// - Recebe pedidos da View
    /// - Invoca o Model
    ///
    /// IMPORTANTE:
    /// - Não contém lógica de negócio
    /// - Não processa dados
    /// </summary>
    public class Controller
    {
        private readonly Model _model;
        private readonly View _view;

        public Controller(Model model, View view)
        {
            _model = model;
            _view = view;

            // Liga os eventos da View aos métodos do Controller.
            _view.OnGuardarInstituicao += GuardarInstituicao;
            _view.OnCriarCurso += CriarCurso;
            _view.OnCriarEdicao += CriarEdicao;
            _view.OnAlterarEstadoEdicao += AlterarEstadoEdicao;
            _view.OnRegistarAluno += RegistarAluno;
            _view.OnCriarInscricao += CriarInscricao;
            _view.OnLancarClassificacao += LancarClassificacao;
            _view.OnConsultarDados += ConsultarDados;
            _view.OnEmitirDiploma += EmitirDiploma;
            _view.OnSair += Sair;
        }

        /// <summary>
        /// Método que inicia o processo de emissão.
        /// </summary>
        public void EmitirDiploma(string nomeAluno, string curso)
        {
            _model.EmitirDiploma(nomeAluno, curso);
        }

        /// <summary>
        /// Encaminha o pedido de guardar instituição.
        /// </summary>
        public void GuardarInstituicao(string nomeInstituicao, string designacaoDiploma, string localidade)
        {
            _model.GuardarInstituicao(nomeInstituicao, designacaoDiploma, localidade);
        }

        /// <summary>
        /// Encaminha o pedido de criar curso.
        /// </summary>
        public void CriarCurso(string codigoCurso)
        {
            _model.CriarCurso(codigoCurso);
        }

        /// <summary>
        /// Encaminha o pedido de criar edição.
        /// </summary>
        public void CriarEdicao(string codigoCurso, string codigoEdicao)
        {
            _model.CriarEdicao(codigoCurso, codigoEdicao);
        }

        /// <summary>
        /// Encaminha o pedido de alterar o estado da edição.
        /// </summary>
        public void AlterarEstadoEdicao(string codigoCurso, string codigoEdicao, string novoEstado)
        {
            _model.AlterarEstadoEdicao(codigoCurso, codigoEdicao, novoEstado);
        }

        /// <summary>
        /// Encaminha o pedido de registar aluno.
        /// </summary>
        public void RegistarAluno(int alunoId, string nomeAluno)
        {
            _model.RegistarAluno(alunoId, nomeAluno);
        }

        /// <summary>
        /// Encaminha o pedido de criar inscrição.
        /// </summary>
        public void CriarInscricao(int alunoId, string codigoCurso, string codigoEdicao)
        {
            _model.CriarInscricao(alunoId, codigoCurso, codigoEdicao);
        }

        /// <summary>
        /// Encaminha o pedido de lançar classificação.
        /// </summary>
        public void LancarClassificacao(int alunoId, string codigoCurso, string codigoEdicao, decimal classificacaoFinal)
        {
            _model.LancarClassificacao(alunoId, codigoCurso, codigoEdicao, classificacaoFinal);
        }

        /// <summary>
        /// Encaminha o pedido de consulta.
        /// </summary>
        public void ConsultarDados(string tipoConsulta)
        {
            _model.ConsultarDados(tipoConsulta);
        }

        /// <summary>
        /// Encaminha o pedido de saída.
        /// </summary>
        public void Sair()
        {
            _model.Sair();
        }

        /// <summary>
        /// Métodos auxiliares para validação antecipada na View.
        /// </summary>
        public bool ExisteCurso(string codigoCurso)
        {
            return _model.ExisteCurso(codigoCurso);
        }

        public bool ExisteEdicao(string codigoCurso, string codigoEdicao)
        {
            return _model.ExisteEdicao(codigoCurso, codigoEdicao);
        }

        public bool ExisteAluno(int alunoId)
        {
            return _model.ExisteAluno(alunoId);
        }
    }
}