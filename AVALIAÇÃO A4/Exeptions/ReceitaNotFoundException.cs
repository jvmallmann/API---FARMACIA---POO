namespace AVALIAÇÃO_A4.Exceptions
{
    public class ReceitaNotFoundException : Exception
    {
        public ReceitaNotFoundException(int id)
            : base($"Receita com ID {id} não encontrada.") { }
    }
}