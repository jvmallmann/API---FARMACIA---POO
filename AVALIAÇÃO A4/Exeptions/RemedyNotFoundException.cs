namespace AVALIAÇÃO_A4.Exceptions
{
    // exceção personalizada para casos onde um remédio não é encontrado
    public class RemedyNotFoundException : Exception
    {
        // construtor que aceita o id do remédio ausente e gera a mensagem de erro
        public RemedyNotFoundException(int id)
            : base($"Remédio com ID {id} não encontrado.") { }
    }
}
