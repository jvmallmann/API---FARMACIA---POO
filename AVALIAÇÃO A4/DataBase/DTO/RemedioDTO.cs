﻿namespace AVALIAÇÃO_A4.DataBase.DTO

{
    public class RemedioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } // Nome do remédio
        public string UnidadeMedida { get; set; } // Unidade de medida do remédio (ex: mg, ml)
        public bool PrecisaReceita { get; set; } // Indica se o remédio precisa de receita
    }
}
