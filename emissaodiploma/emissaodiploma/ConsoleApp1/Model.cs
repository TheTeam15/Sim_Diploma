using System;

// ==========================================================
// MODEL
// ==========================================================
// O Model contém:
// - lógica de negócio;
// - validação;
// - coordenação da emissão do diploma.
//
// O Model NÃO conhece a View.
// A comunicação com a interface é feita apenas através
// do Controller.
//
// O Model depende apenas da interface IGeradorDiploma.
// ==========================================================

public class Model
{
    // Dependência abstraída através de interface
    private readonly IGeradorDiploma gerador;

    // ======================================================
    // EVENTOS
    // ======================================================
    // O Model comunica resultados através de eventos.
    // O Controller será responsável por os tratar.
    // ======================================================

    public event EventHandler<ValidacaoEventArgs>? OnValidacao;

    public event EventHandler<DiplomaEmitidoEventArgs>? OnDiplomaEmitido;

    // ======================================================
    // CONSTRUTOR
    // ======================================================

    public Model(IGeradorDiploma gerador)
    {
        this.gerador = gerador;
    }

    // ======================================================
    // MÉTODO PRINCIPAL DE EMISSÃO
    // ======================================================

    public void EmitirDiploma(string nomeAluno, string curso)
    {
        // Simulação de validação académica
        bool elegivel = ValidarElegibilidade(nomeAluno);

        // Comunicação do resultado da validação
        OnValidacao?.Invoke(
            this,
            new ValidacaoEventArgs
            {
                Elegivel = elegivel,
                Mensagem = elegivel
                    ? "Aluno elegível para diploma."
                    : "Aluno não elegível."
            }
        );

        // Caso o aluno não seja elegível,
        // o processo termina aqui.
        if (!elegivel)
            return;

        // ==================================================
        // GERAÇÃO DO PDF
        // ==================================================

        byte[] pdf = gerador.Gerar(nomeAluno, curso);

        // ==================================================
        // COMUNICAÇÃO DO RESULTADO
        // ==================================================

        OnDiplomaEmitido?.Invoke(
            this,
            new DiplomaEmitidoEventArgs
            {
                Pdf = pdf
            }
        );
    }

    // ======================================================
    // VALIDAÇÃO ACADÉMICA
    // ======================================================
    // Simulação simples de validação.
    // ======================================================

    private bool ValidarElegibilidade(string nomeAluno)
    {
        return !string.IsNullOrWhiteSpace(nomeAluno);
    }
}
