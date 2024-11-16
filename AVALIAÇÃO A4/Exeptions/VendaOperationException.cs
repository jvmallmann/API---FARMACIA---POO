using System;

namespace AVALIAÇÃO_A4.Exceptions
{
    public class VendaOperationException : Exception
    {
        public VendaOperationException() 
            : base("Erro ao processar a operação de venda.") { }

        public VendaOperationException(string message) 
            : base(message) { }

        public VendaOperationException(string message, Exception innerException) 
            : base(message, innerException) { }

    }
}
