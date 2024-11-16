namespace AVALIAÇÃO_A4.Exceptions
{
    // exceção personalizada para erros de validação na venda
    public class SaleValidationException : Exception
    {
        // construtor padrão com mensagem genérica
        public SaleValidationException()
            : base("Erro de validação na venda.") { }

        // construtor que aceita uma mensagem personalizada
        public SaleValidationException(string message)
            : base(message) { }

        // construtor que aceita mensagem personalizada e uma exceção interna para detalhamento
        public SaleValidationException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
