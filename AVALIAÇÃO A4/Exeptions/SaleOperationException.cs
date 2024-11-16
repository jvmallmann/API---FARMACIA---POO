namespace AVALIAÇÃO_A4.Exceptions
{
    // exceção personalizada para erros relacionados a operações de venda
    public class SaleOperationException : Exception
    {
        // construtor padrão com mensagem genérica
        public SaleOperationException()
            : base("Erro ao processar a operação de venda.") { }

        // construtor que aceita uma mensagem personalizada
        public SaleOperationException(string message)
            : base(message) { }

        // construtor que aceita mensagem personalizada e uma exceção interna para detalhamento
        public SaleOperationException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
