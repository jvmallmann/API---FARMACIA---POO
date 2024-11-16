using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.Exceptions;

namespace AVALIAÇÃO_A4.Validate
{
    // classe responsável por validar os dados do cliente
    public class CustomerValidator
    {
        // método para validar os dados do cliente fornecidos no DTO
        public void Validar(CustomerDTO clienteDto)
        {
            // verifica se os dados do cliente estão nulos
            if (clienteDto == null)
                throw new CustomerValidationException("Os dados do cliente não podem ser nulos.");

            // validação do campo Nome - obrigatório
            if (string.IsNullOrWhiteSpace(clienteDto.Nome))
                throw new CustomerValidationException("O nome do cliente é obrigatório.");

            // validação do campo CPF - obrigatório
            if (string.IsNullOrWhiteSpace(clienteDto.CPF))
                throw new CustomerValidationException("O CPF do cliente é obrigatório.");

            // validação do tamanho do CPF - deve conter 11 dígitos
            if (clienteDto.CPF.Length != 11)
                throw new CustomerValidationException("O CPF deve conter 11 dígitos.");

            // validação para clientes que possuem receita: CPF é obrigatório
            if (clienteDto.PossuiReceita && string.IsNullOrWhiteSpace(clienteDto.CPF))
                throw new CustomerValidationException("Clientes que possuem receita devem ter um CPF válido.");
        }

        // método auxiliar para validar a estrutura do CPF
        private bool ValidarCPF(string cpf)
        {
            // valida se o CPF contém apenas números
            return cpf.All(char.IsDigit);
        }
    }
}
