using System;

namespace AVALIAÇÃO_A4.Exceptions
{
    public class ClienteValidationException : Exception
    {
        public ClienteValidationException(string message)
            : base($"Erro de validação no cliente: {message}") { }
    }
}
