using System;

// ==========================================================
// EVENTO DE EMISSÃO DE DIPLOMA
// ==========================================================
// Transporta o PDF gerado pelo sistema.
//
// O Model comunica o resultado ao Controller usando
// esta classe.
// ==========================================================

public class DiplomaEmitidoEventArgs : EventArgs
{
    public required byte[] Pdf { get; set; }
}
