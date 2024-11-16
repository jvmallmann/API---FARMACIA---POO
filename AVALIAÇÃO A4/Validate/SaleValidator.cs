using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.DataBase.Models;
using AVALIAÇÃO_A4.Exceptions;
using AVALIAÇÃO_A4.Interface;

namespace AVALIAÇÃO_A4.Validate
{
    // classe responsável por validar os dados de uma venda
    public class SaleValidator
    {
        // método para validar os dados da venda
        public void Validar(SaleDTO vendaDto, Customer cliente, Remedy remedio)
        {
            // verifica se o cliente fornecido é nulo
            if (cliente == null)
                throw new SaleValidationException("Cliente não encontrado.");

            // verifica se o remédio fornecido é nulo
            if (remedio == null)
                throw new SaleValidationException("Remédio não encontrado.");

            // valida se a quantidade na venda é maior que zero
            if (vendaDto.Quantidade <= 0)
                throw new SaleValidationException("A quantidade deve ser maior que zero.");

            // verifica se o remédio exige receita e se o cliente não possui uma receita
            if (remedio.PrecisaReceita && !cliente.PossuiReceita)
            {
                throw new SaleValidationException("O cliente não possui uma receita válida para este remédio.");
            }
        }
    }
}
