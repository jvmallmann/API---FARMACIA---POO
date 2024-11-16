using System;

namespace AVALIAÇÃO_A4.Exceptions
{
    public class VendaNotFoundException : Exception
    {
        public VendaNotFoundException() 
            : base("Venda não encontrada.") { }

        public VendaNotFoundException(string message) 
            : base(message) { }

        public VendaNotFoundException(string message, Exception innerException) 
            : base(message, innerException) { }

    }
}
