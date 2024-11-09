namespace AVALIAÇÃO_A4.DataBase.Models

{
    public class Venda
    {
        public int Id { get; set; }
        public int ClienteId { get; set; } // Relacionamento com Cliente
        public int RemedioId { get; set; } // Relacionamento com Remédio
        public DateTime DataVenda { get; set; }
        public int Quantidade { get; set; }
    }
}
