using System;

namespace AVALIAÇÃO_A4.DataBase.Models
{
    public class EstoqueRemedio
    {
        public int Id { get; set; }               // ID do estoque
        public int RemedioId { get; set; }        // Chave estrangeira para Remedios
        public int Quantidade { get; set; }       // Quantidade em estoque
        public DateTime DataValidade { get; set; } // Data de validade do lote
        public string Lote { get; set; }          // Número do lote

        // Relacionamento com o Remédio
        public Remedio Remedio { get; set; }      // Propriedade de navegação para o Remédio
    }
}
