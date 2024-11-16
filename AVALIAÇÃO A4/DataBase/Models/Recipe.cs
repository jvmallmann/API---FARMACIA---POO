namespace AVALIAÇÃO_A4.DataBase.Models

{
    public class Recipe
    {
        public int Id { get; set; }// id da receita
        public string Numero { get; set; } // Número da receita
        public string Medico { get; set; } // Nome do médico que emitiu a receita

        public int ClienteId { get; set; } // Propriedade para o ID do Cliente
    }
}
