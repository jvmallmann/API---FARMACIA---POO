namespace AVALIAÇÃO_A4.DataBase.Models

{
    public class Venda
    {
        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public int Quantidade { get; set; }

        // Chaves estrangeiras
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public int RemedioId { get; set; }
        public Remedio Remedio { get; set; }
    }

}
