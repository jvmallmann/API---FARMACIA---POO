namespace AVALIAÇÃO_A4.DataBase.DTO
{
    public class VendaDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int RemedioId { get; set; }
        public DateTime DataVenda { get; set; }
        public int Quantidade { get; set; }
    }
}
