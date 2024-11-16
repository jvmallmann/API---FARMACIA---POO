using System;

namespace AVALIAÇÃO_A4.Exceptions
{
    // exceção personalizada para erros de validação de cliente
    public class CustomerValidationException : Exception
    {
        // construtor que aceita uma mensagem de erro específica
        public CustomerValidationException(string message)
            : base($"Erro de validação do cliente: {message}") { }
    }
}
