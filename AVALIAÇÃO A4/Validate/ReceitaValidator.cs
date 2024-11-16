using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.Exceptions;

namespace AVALIAÇÃO_A4.Validate
{
    public class ReceitaValidator
    {
        public void Validar(ReceitaDTO receitaDto)
        {
            if (string.IsNullOrWhiteSpace(receitaDto.Numero))
            {
                throw new ReceitaValidationException("O número da receita não pode ser vazio.");
            }

            if (string.IsNullOrWhiteSpace(receitaDto.Medico))
            {
                throw new ReceitaValidationException("O nome do médico não pode ser vazio.");
            }

            if (receitaDto.Numero.Length < 5)
            {
                throw new ReceitaValidationException("O número da receita deve conter pelo menos 5 caracteres.");
            }
        }
    }
}
