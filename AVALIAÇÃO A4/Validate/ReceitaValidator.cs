using AVALIAÇÃO_A4.DataBase.DTO;

namespace AVALIAÇÃO_A4.Validate
{
    public class ReceitaValidator
    {
        public bool Validar(ReceitaDTO receitaDto)
        {
            // Verifica se o número da receita não está vazio
            if (string.IsNullOrWhiteSpace(receitaDto.Numero))
            {
                return false;
            }

            // Verifica se o nome do médico não está vazio
            if (string.IsNullOrWhiteSpace(receitaDto.Medico))
            {
                return false;
            }

            return true; // A receita é válida
        }
    }
}
