using AVALIAÇÃO_A4.DataBase.DTO;

namespace AVALIAÇÃO_A4.Validate
{
    public class RemedioValidator
    {
        public bool Validar(RemedioDTO remedioDto)
        {
            if (string.IsNullOrWhiteSpace(remedioDto.Nome))
                throw new ArgumentException("O nome do remédio não pode ser vazio.");

            if (string.IsNullOrWhiteSpace(remedioDto.UnidadeMedida))
                throw new ArgumentException("A unidade de medida do remédio não pode ser vazia.");

            return true;
        }
    }
}
