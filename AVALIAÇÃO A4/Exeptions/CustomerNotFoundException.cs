namespace AVALIAÇÃO_A4.Exceptions
{
    // exceção personalizada para indicar que um cliente não foi encontrado
    public class CustomerNotFoundException : Exception
    {
        // construtor que aceita o id do cliente não encontrado
        public CustomerNotFoundException(int id)
            : base($"Cliente com ID {id} não foi encontrado.") { }
    }
}
