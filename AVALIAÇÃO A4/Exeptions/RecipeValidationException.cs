namespace AVALIAÇÃO_A4.Exceptions
{
    // exceção personalizada para erros de validação relacionados a receitas
    public class RecipeValidationException : Exception
    {
        // construtor que aceita uma mensagem detalhando o erro de validação
        public RecipeValidationException(string message)
            : base($"Erro de validação na receita: {message}") { }
    }
}
