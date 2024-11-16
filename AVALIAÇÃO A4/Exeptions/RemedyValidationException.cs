namespace AVALIAÇÃO_A4.Exceptions
{
    // exceção personalizada para validações relacionadas ao remédio
    public class RemedyValidationException : Exception
    {
        // construtor que aceita uma mensagem personalizada para detalhar o erro de validação
        public RemedyValidationException(string message)
            : base($"Erro de validação no remédio: {message}") { }
    }
}
