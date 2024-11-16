using System;

namespace AVALIAÇÃO_A4.Exceptions
{
    public class EstoqueValidationException : Exception
    {
        public EstoqueValidationException() 
            : base("Erro de validação do estoque.") {}

        public EstoqueValidationException(string message) 
            : base(message) {}

        public EstoqueValidationException(string message, Exception innerException) 
            : base(message, innerException) {}
    }
}
