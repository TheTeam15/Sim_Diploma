using System;

public class CursoNaoExisteException : Exception
{
    public CursoNaoExisteException(string mensagem) : base(mensagem)
    {
    }
    public CursoNaoExisteException()
         : base("O curso indicado não existe.")
    {
    }
}