namespace AVALIAÇÃO_A4.Exceptions
{
    // exceção personalizada para quando uma receita não é encontrada
    public class RecipeNotFoundException : Exception
    {
        // construtor que aceita o id da receita que não foi encontrada
        public RecipeNotFoundException(int id)
            : base($"Receita com ID {id} não encontrada.") { }
    }
}
