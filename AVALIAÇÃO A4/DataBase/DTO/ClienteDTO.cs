using System.ComponentModel.DataAnnotations;

namespace AVALIAÇÃO_A4.DataBase.DTO
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } // Nome do cliente
        public string CPF { get; set; } // CPF do cliente (obrigatório em caso de receita)

        public bool PossuiReceita { get; set; } // Novo campo
    }
}
