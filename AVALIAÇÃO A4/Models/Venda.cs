namespace AVALIAÇÃO_A4.Classes
{
    public class Venda
    {
        public int Id { get; set; } 
        public int RemedioId { get; set; } 
        public int? ReceitaId { get; set; } 
        public int ClienteId { get; set; } 

        public Remedio Remedio { get; set; } 
        public Receita Receita { get; set; } 
        public Cliente Cliente { get; set; } 
    }
}
