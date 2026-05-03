using System;

public class EdicaoDuplicadaException : Exception
{
    public EdicaoDuplicadaException(string mensagem) : base(mensagem)
    {
    }
    public EdicaoDuplicadaException()
         : base("Já existe uma edição com esse código nesse curso.")
    {
    }
}
