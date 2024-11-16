namespace AVALIAÇÃO_A4.DataBase.DTO
{
    public class EstoqueRemedioDTO
    {
        public int Id { get; set; }
        public int RemedioId { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataValidade { get; set; }
        public string Lote { get; set; }
    }
}
