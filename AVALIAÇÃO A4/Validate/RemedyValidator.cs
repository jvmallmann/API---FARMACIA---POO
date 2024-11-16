using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.Exceptions;

namespace AVALIAÇÃO_A4.Validate
{
    // classe responsável por validar os dados de um remédio
    public class RemedyValidator
    {
        // método para validar os dados fornecidos no DTO do remédio
        public void Validar(RemedyDTO remedioDto)
        {
            // verifica se o nome do remédio está vazio ou nulo
            if (string.IsNullOrWhiteSpace(remedioDto.Nome))
            {
                throw new RemedyValidationException("O nome do remédio não pode ser vazio.");
            }

            // verifica se o nome do remédio tem pelo menos 3 caracteres
            if (remedioDto.Nome.Length < 3)
            {
                throw new RemedyValidationException("O nome do remédio deve ter pelo menos 3 caracteres.");
            }

            // valida se a unidade de medida está vazia ou nula
            if (string.IsNullOrWhiteSpace(remedioDto.UnidadeMedida))
            {
                throw new RemedyValidationException("A unidade de medida do remédio não pode ser vazia.");
            }

            // verifica se a unidade de medida é válida (mg, ml, g, kg, unidade)
            if (!new[] { "mg", "ml", "g", "kg", "unidade" }.Contains(remedioDto.UnidadeMedida.ToLower()))
            {
                throw new RemedyValidationException("A unidade de medida deve ser 'mg', 'ml', 'g', 'kg' ou 'unidade'.");
            }

            // valida se o campo PrecisaReceita é um valor booleano válido
            if (remedioDto.PrecisaReceita != true && remedioDto.PrecisaReceita != false)
            {
                throw new RemedyValidationException("O campo 'PrecisaReceita' deve ser verdadeiro ou falso.");
            }
        }
    }
}
