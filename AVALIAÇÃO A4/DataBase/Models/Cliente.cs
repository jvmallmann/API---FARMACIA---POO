using System.ComponentModel.DataAnnotations;

namespace AVALIAÇÃO_A4.DataBase.Models

{
    public class Cliente
    {
        public int Id { get; set; } 
        public string Nome { get; set; } 
        public string CPF { get; set; } 
    }
}
