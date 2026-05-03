using System;

public class CursoDuplicadoException : Exception
{
    public CursoDuplicadoException(string mensagem) : base(mensagem)
    {
    }
    public CursoDuplicadoException()
         : base("Já existe um curso com esse código.")
    {
    }
}