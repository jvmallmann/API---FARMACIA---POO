namespace AVALIAÇÃO_A4.DataBase.DTO

{
    public class VendaDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; } // ID do cliente associado à venda
        public int RemedioId { get; set; } // ID do remédio associado à venda
        public DateTime DataVenda { get; set; }
        public int Quantidade { get; set; }
    }
}
