using System.ComponentModel.DataAnnotations;

namespace AVALIAÇÃO_A4.DataBase.DTO
{
    public class CustomerDTO
    {
        public int Id { get; set; } // id do cliente
        public string Nome { get; set; } // Nome do cliente
        public string CPF { get; set; } // CPF do cliente 
        public bool PossuiReceita { get; set; } // indica se o cliente possui receita
    }
}
