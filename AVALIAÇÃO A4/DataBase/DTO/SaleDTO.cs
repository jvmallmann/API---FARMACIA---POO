namespace AVALIAÇÃO_A4.DataBase.DTO
{
    public class SaleDTO
    {
        public int Id { get; set; } // id da venda
        public int ClienteId { get; set; } // id do cliente que realizou a venda
        public int RemedioId { get; set; } // id do remédio vendido
        public DateTime DataVenda { get; set; } // data da venda
        public int Quantidade { get; set; } // quantidade de remédios vendidos
    }
}
