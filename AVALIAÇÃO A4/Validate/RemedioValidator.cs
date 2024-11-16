using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.Exceptions;

namespace AVALIAÇÃO_A4.Validate
{
    public class RemedioValidator
    {
        public void Validar(RemedioDTO remedioDto)
        {
            if (string.IsNullOrWhiteSpace(remedioDto.Nome))
            {
                throw new RemedioValidationException("O nome do remédio não pode ser vazio.");
            }

            if (remedioDto.Nome.Length < 3)
            {
                throw new RemedioValidationException("O nome do remédio deve ter pelo menos 3 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(remedioDto.UnidadeMedida))
            {
                throw new RemedioValidationException("A unidade de medida do remédio não pode ser vazia.");
            }

            if (!new[] { "mg", "ml", "g", "kg", "unidade" }.Contains(remedioDto.UnidadeMedida.ToLower()))
            {
                throw new RemedioValidationException("A unidade de medida deve ser 'mg', 'ml', 'g', 'kg' ou 'unidade'.");
            }

            if (remedioDto.PrecisaReceita != true && remedioDto.PrecisaReceita != false)
            {
                throw new RemedioValidationException("O campo 'PrecisaReceita' deve ser verdadeiro ou falso.");
            }
        }
    }
}
