using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.Exceptions;

namespace AVALIAÇÃO_A4.Validate
{
    // classe responsável por validar os dados da receita
    public class RecipeValidator
    {
        // método para validar os dados da receita fornecidos no DTO
        public void Validar(RecipeDTO receitaDto)
        {
            // valida se o número da receita está vazio ou nulo
            if (string.IsNullOrWhiteSpace(receitaDto.Numero))
            {
                throw new RecipeValidationException("O número da receita não pode ser vazio.");
            }

            // valida se o nome do médico está vazio ou nulo
            if (string.IsNullOrWhiteSpace(receitaDto.Medico))
            {
                throw new RecipeValidationException("O nome do médico não pode ser vazio.");
            }

            // verifica se o número da receita tem pelo menos 5 caracteres
            if (receitaDto.Numero.Length < 5)
            {
                throw new RecipeValidationException("O número da receita deve conter pelo menos 5 caracteres.");
            }
        }
    }
}
