using System.ComponentModel.DataAnnotations;

namespace AVALIAÇÃO_A4.Classes
{
    public class Cliente
    {
        public int Id { get; set; } 
        [Required]
        public string Nome { get; set; } 
        [Required]
        public string CPF { get; set; } 
    }
}
