using System;

namespace AVALIAÇÃO_A4.Exceptions
{
    public class RemedioValidationException : Exception
    {
        public RemedioValidationException(string message)
            : base($"Erro de validação no remédio: {message}") { }
    }
}
