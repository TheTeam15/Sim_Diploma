using System;

namespace SimDiplomaMVC
{
    /// <summary>
    /// Delegados dos pedidos da View para o Controller.
    /// </summary>
    public delegate void GuardarInstituicaoEventHandler(string nomeInstituicao, string designacaoDiploma, string localidade);
    public delegate void CriarCursoEventHandler(string codigoCurso);
    public delegate void CriarEdicaoEventHandler(string codigoCurso, string codigoEdicao);
    public delegate void AlterarEstadoEdicaoEventHandler(string codigoCurso, string codigoEdicao, string novoEstado);
    public delegate void RegistarAlunoEventHandler(int alunoId, string nomeAluno);
    public delegate void CriarInscricaoEventHandler(int alunoId, string codigoCurso, string codigoEdicao);
    public delegate void LancarClassificacaoEventHandler(int alunoId, string codigoCurso, string codigoEdicao, decimal classificacaoFinal);
    public delegate void ConsultarDadosEventHandler(string tipoConsulta);
    public delegate void EmitirDiplomaEventHandler(string nomeAluno, string curso);
    public delegate void SairEventHandler();

    /// <summary>
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
    /// </summary>
    public class View
    {
        /// <summary>
        /// Eventos que a View poderá disparar para o Controller.
        /// </summary>
        public event GuardarInstituicaoEventHandler OnGuardarInstituicao;
        public event CriarCursoEventHandler OnCriarCurso;
        public event CriarEdicaoEventHandler OnCriarEdicao;
        public event AlterarEstadoEdicaoEventHandler OnAlterarEstadoEdicao;
        public event RegistarAlunoEventHandler OnRegistarAluno;
        public event CriarInscricaoEventHandler OnCriarInscricao;
        public event LancarClassificacaoEventHandler OnLancarClassificacao;
        public event ConsultarDadosEventHandler OnConsultarDados;
        public event EmitirDiplomaEventHandler OnEmitirDiploma;
        public event SairEventHandler OnSair;

        /// <summary>
        /// Subscrição aos eventos do Model.
        /// Liga a View ao fluxo de notificações.
        /// </summary>
        public void Subscrever(Model model)
        {
            model.OnValidacao += MostrarValidacao;
            model.OnDiplomaEmitido += MostrarDiploma;
            model.OnInstituicaoGuardada += MostrarInstituicaoGuardada;
            model.OnCursoCriado += MostrarCursoCriado;
            model.OnEdicaoCriada += MostrarEdicaoCriada;
            model.OnEstadoEdicaoAlterado += MostrarEstadoEdicaoAlterado;
            model.OnAlunoRegistado += MostrarAlunoRegistado;
            model.OnInscricaoCriada += MostrarInscricaoCriada;
            model.OnClassificacaoLancada += MostrarClassificacaoLancada;
            model.OnConsultaRealizada += MostrarConsultaRealizada;
            model.OnSistemaEncerrado += MostrarEncerramento;
        }

        /// <summary>
        /// Apresenta o resultado da validação.
        /// </summary>
        private void MostrarValidacao(object sender, ValidacaoEventArgs e)
        {
            Console.WriteLine("[VIEW] " + e.Mensagem);
        }

        /// <summary>
        /// Apresenta informação sobre o diploma gerado.
        /// </summary>
        private void MostrarDiploma(object sender, DiplomaEmitidoEventArgs e)
        {
            Console.WriteLine("[VIEW] Diploma gerado com sucesso!");
            Console.WriteLine("[VIEW] Tamanho do PDF: " + e.PdfBytes.Length + " bytes");
        }

        /// <summary>
        /// Apresenta os dados da instituição guardada.
        /// </summary>
        private void MostrarInstituicaoGuardada(object sender, InstituicaoGuardadaEventArgs e)
        {
            // TODO:
            // Mostrar os dados da instituição atualizada.
        }

        /// <summary>
        /// Apresenta os dados do curso criado.
        /// </summary>
        private void MostrarCursoCriado(object sender, CursoCriadoEventArgs e)
        {
            // TODO:
            // Mostrar o curso criado.
        }

        /// <summary>
        /// Apresenta os dados da edição criada.
        /// </summary>
        private void MostrarEdicaoCriada(object sender, EdicaoCriadaEventArgs e)
        {
            // TODO:
            // Mostrar a edição criada.
        }

        /// <summary>
        /// Apresenta a alteração de estado de uma edição.
        /// </summary>
        private void MostrarEstadoEdicaoAlterado(object sender, EstadoEdicaoAlteradoEventArgs e)
        {
            // TODO:
            // Mostrar o novo estado da edição.
        }

        /// <summary>
        /// Apresenta os dados do aluno registado.
        /// </summary>
        private void MostrarAlunoRegistado(object sender, AlunoRegistadoEventArgs e)
        {
            // TODO:
            // Mostrar o aluno registado.
        }

        /// <summary>
        /// Apresenta os dados da inscrição criada.
        /// </summary>
        private void MostrarInscricaoCriada(object sender, InscricaoCriadaEventArgs e)
        {
            // TODO:
            // Mostrar a inscrição criada.
        }

        /// <summary>
        /// Apresenta os dados da classificação lançada.
        /// </summary>
        private void MostrarClassificacaoLancada(object sender, ClassificacaoLancadaEventArgs e)
        {
            // TODO:
            // Mostrar a classificação lançada.
        }

        /// <summary>
        /// Apresenta o resultado de uma consulta.
        /// </summary>
        private void MostrarConsultaRealizada(object sender, ConsultaRealizadaEventArgs e)
        {
            // TODO:
            // Mostrar os resultados da consulta.
        }

        /// <summary>
        /// Apresenta a mensagem de encerramento.
        /// </summary>
        private void MostrarEncerramento(object sender, EventArgs e)
        {
            // TODO:
            // Mostrar mensagem de fecho da aplicação.
        }

        /// <summary>
        /// Métodos futuros da View.
        /// Mais tarde irão recolher dados do utilizador e disparar os eventos acima.
        /// </summary>
        public void GuardarInstituicao()
        {
            // TODO:
            // Recolher dados e disparar:
            // OnGuardarInstituicao?.Invoke(...);
        }

        public void CriarCurso()
        {
            // TODO:
            // Recolher dados e disparar:
            // OnCriarCurso?.Invoke(...);
        }

        public void CriarEdicao()
        {
            // TODO:
            // Recolher dados e disparar:
            // OnCriarEdicao?.Invoke(...);
        }

        public void AlterarEstadoEdicao()
        {
            // TODO:
            // Recolher dados e disparar:
            // OnAlterarEstadoEdicao?.Invoke(...);
        }

        public void RegistarAluno()
        {
            // TODO:
            // Recolher dados e disparar:
            // OnRegistarAluno?.Invoke(...);
        }

        public void CriarInscricao()
        {
            // TODO:
            // Recolher dados e disparar:
            // OnCriarInscricao?.Invoke(...);
        }

        public void LancarClassificacao()
        {
            // TODO:
            // Recolher dados e disparar:
            // OnLancarClassificacao?.Invoke(...);
        }

        public void ConsultarDados()
        {
            // TODO:
            // Recolher dados e disparar:
            // OnConsultarDados?.Invoke(...);
        }

        public void EmitirDiploma()
        {
            // TODO:
            // Recolher dados e disparar:
            // OnEmitirDiploma?.Invoke(...);
        }

        public void Sair()
        {
            // TODO:
            // Disparar:
            // OnSair?.Invoke();
        }
    }
}