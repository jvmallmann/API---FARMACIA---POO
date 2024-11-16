using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.DataBase.Models;
using AVALIAÇÃO_A4.Exceptions;
using AVALIAÇÃO_A4.Interface;

namespace AVALIAÇÃO_A4.Validate
{
    public class VendaValidator
    {
        private readonly IRepository<Cliente> _clienteRepository;
        private readonly IRepository<Remedio> _remedioRepository;
        private readonly IRepository<Receita> _receitaRepository;
        private readonly EstoqueRemedioValidator _estoqueValidator;

        public VendaValidator(
            IRepository<Cliente> clienteRepository,
            IRepository<Remedio> remedioRepository,
            IRepository<Receita> receitaRepository,
            EstoqueRemedioValidator estoqueValidator)
        {
            _clienteRepository = clienteRepository;
            _remedioRepository = remedioRepository;
            _receitaRepository = receitaRepository;
            _estoqueValidator = estoqueValidator;
        }

        public void Validar(VendaDTO vendaDto, Cliente cliente, Remedio remedio)
        {
            if (cliente == null)
                throw new VendaValidationException("Cliente não encontrado.");

            if (remedio == null)
                throw new VendaValidationException("Remédio não encontrado.");

            if (vendaDto.Quantidade <= 0)
                throw new VendaValidationException("A quantidade deve ser maior que zero.");

            // Verifica se o remédio precisa de receita e se o cliente possui receita
            if (remedio.PrecisaReceita && !cliente.PossuiReceita)
            {
                throw new VendaValidationException("O cliente não possui uma receita válida para este remédio.");
            }
        }
    }
}
