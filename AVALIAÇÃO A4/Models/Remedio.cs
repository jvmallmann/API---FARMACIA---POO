namespace AVALIAÇÃO_A4.Classes
{
    public class Remedio
    {
        public int Id { get; set; } // Identificador único do remédio
        public string Nome { get; set; } // Nome do remédio
        public string UnidadeMedida { get; set; } // Unidade de medida do remédio (ex: mg, ml)
        public bool PrecisaReceita { get; set; } // Indica se o remédio precisa de receita
    }
}
