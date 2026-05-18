using System;

// ==========================================================
// EVENTO DE VALIDAÇÃO
// ==========================================================
// Esta classe transporta informação sobre o resultado
// da validação académica.
//
// É utilizada pelo Model para comunicar com o Controller.
// ==========================================================

public class ValidacaoEventArgs : EventArgs
{
    public bool Elegivel { get; set; }

    public required string Mensagem { get; set; }
}
