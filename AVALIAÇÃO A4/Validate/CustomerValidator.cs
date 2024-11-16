using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.Exceptions;

namespace AVALIAÇÃO_A4.Validate
{
    public class CustomerValidator
    {
        public void Validar(CustomerDTO clienteDto)
        {
            // Verifica se o DTO do cliente é nulo
            if (clienteDto == null)
                throw new CustomerValidationException("Os dados do cliente não podem ser nulos.");

            // Verifica se o nome foi preenchido
            if (string.IsNullOrWhiteSpace(clienteDto.Nome))
                throw new CustomerValidationException("O nome do cliente é obrigatório.");

            // Valida o CPF apenas se o cliente possui receita
            if (clienteDto.PossuiReceita)
            {
                if (string.IsNullOrWhiteSpace(clienteDto.CPF))
                    throw new CustomerValidationException("O CPF é obrigatório para clientes que possuem receita.");

                if (clienteDto.CPF.Length != 11)
                    throw new CustomerValidationException("O CPF deve conter 11 dígitos.");

                if (!ValidarCPF(clienteDto.CPF))
                    throw new CustomerValidationException("O CPF informado é inválido.");
            }
        }

        // Método auxiliar para validar o CPF
        private bool ValidarCPF(string cpf)
        {
            // Validação básica: verifica se todos os caracteres são dígitos
            return cpf.All(char.IsDigit);
        }
    }
}
