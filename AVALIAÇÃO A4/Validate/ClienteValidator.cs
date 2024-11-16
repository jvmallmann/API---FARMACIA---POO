using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.Exceptions;

namespace AVALIAÇÃO_A4.Validate
{
    public class ClienteValidator
    {
        public void Validar(ClienteDTO clienteDto)
        {
            if (clienteDto == null)
                throw new ClienteValidationException("Os dados do cliente não podem ser nulos.");

            if (string.IsNullOrWhiteSpace(clienteDto.Nome))
                throw new ClienteValidationException("O nome do cliente é obrigatório.");

            if (string.IsNullOrWhiteSpace(clienteDto.CPF))
                throw new ClienteValidationException("O CPF do cliente é obrigatório.");

            if (clienteDto.CPF.Length != 11)
                throw new ClienteValidationException("O CPF deve conter 11 dígitos.");

            if (!ValidarCPF(clienteDto.CPF))
                throw new ClienteValidationException("O CPF informado é inválido.");

            // Valida o campo PossuiReceita
            if (clienteDto.PossuiReceita && string.IsNullOrWhiteSpace(clienteDto.CPF))
                throw new ClienteValidationException("Clientes que possuem receita devem ter um CPF válido.");
        }

        private bool ValidarCPF(string cpf)
        {
            // Adicione aqui uma validação básica ou completa para CPF
            // Exemplo simples para garantir apenas que é numérico:
            return cpf.All(char.IsDigit);
        }
    }
}
