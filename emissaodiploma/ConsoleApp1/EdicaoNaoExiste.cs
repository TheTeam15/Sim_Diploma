using System;

public class EdicaoNaoExisteException : Exception
{
    public EdicaoNaoExisteException()
         : base("A edição indicada não existe.")
    {
    }

    public EdicaoNaoExisteException(string mensagem)
         : base(mensagem)
    {
    }
}