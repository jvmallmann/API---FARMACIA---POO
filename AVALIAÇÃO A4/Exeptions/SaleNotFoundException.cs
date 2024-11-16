namespace AVALIAÇÃO_A4.Exceptions
{
    // exceção personalizada para indicar que uma venda não foi encontrada
    public class SaleNotFoundException : Exception
    {
        // construtor padrão com mensagem genérica
        public SaleNotFoundException()
            : base("Venda não encontrada.") { }

        // construtor que aceita uma mensagem personalizada
        public SaleNotFoundException(string message)
            : base(message) { }

        // construtor que aceita mensagem personalizada e uma exceção interna para detalhamento
        public SaleNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
