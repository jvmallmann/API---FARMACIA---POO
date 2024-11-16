namespace AVALIAÇÃO_A4.Exceptions
{
    public class ReceitaValidationException : Exception
    {
        public ReceitaValidationException(string message)
            : base($"Erro de validação: {message}") { }
    }
}
