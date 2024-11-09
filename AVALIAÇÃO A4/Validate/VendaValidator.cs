using AVALIAÇÃO_A4.DataBase.DTO;

namespace AVALIAÇÃO_A4.Validate
{
    public class VendaValidator
    {
        public bool Validar(VendaDTO vendaDto)
        {
            if (vendaDto.ClienteId <= 0)
                throw new ArgumentException("O ID do cliente é obrigatório e deve ser válido.");

            if (vendaDto.RemedioId <= 0)
                throw new ArgumentException("O ID do remédio é obrigatório e deve ser válido.");

            if (vendaDto.Quantidade <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");

            return true;
        }
    }
}
