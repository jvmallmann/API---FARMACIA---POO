using AVALIAÇÃO_A4.DataBase.DTO;

namespace AVALIAÇÃO_A4.Validate
{
    public class ClienteValidator
    {
        public bool Validar(ClienteDTO clienteDto)
        {
            if (string.IsNullOrWhiteSpace(clienteDto.Nome))
            {
                throw new ArgumentException("O nome do cliente não pode ser vazio.");
            }

            if (string.IsNullOrWhiteSpace(clienteDto.CPF))
            {
                throw new ArgumentException("O CPF do cliente não pode ser vazio.");
            }

            if (clienteDto.CPF.Length != 11)
            {
                throw new ArgumentException("O CPF do cliente deve conter 11 dígitos.");
            }

            return true;
        }
    }
}
