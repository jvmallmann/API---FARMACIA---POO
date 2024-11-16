namespace AVALIAÇÃO_A4.DataBase.Models

{
    public class Receita
    {
        public int Id { get; set; }
        public string Numero { get; set; } 
        public string Medico { get; set; }

        public List<Remedio> Remedios { get; set; } = new List<Remedio>();
        public int ClienteId { get; set; } // Propriedade para o ID do Cliente
    }
}
