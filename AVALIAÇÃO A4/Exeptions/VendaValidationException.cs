using System;

namespace AVALIAÇÃO_A4.Exceptions
{
    public class VendaValidationException : Exception
    {
        public VendaValidationException()
            : base("Erro de validação na venda.") { }
  

        public VendaValidationException(string message) 
            : base(message) { }


        public VendaValidationException(string message, Exception innerException) 
            : base(message, innerException) { }

    }
}
