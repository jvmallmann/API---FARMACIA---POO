namespace AVALIAÇÃO_A4.Exceptions
{
    public class RemedioNotFoundException : Exception
    {
        public RemedioNotFoundException(int id)
            : base($"Remédio com ID {id} não encontrado.") { }
    }
}
