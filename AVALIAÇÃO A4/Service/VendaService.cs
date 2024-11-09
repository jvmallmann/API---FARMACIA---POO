using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.DataBase.Models;
using AVALIAÇÃO_A4.Interface;
using AVALIAÇÃO_A4.Validate;
using AutoMapper;

namespace AVALIAÇÃO_A4.Service
{
    public class VendaService : IVendaService
    {
        private readonly IRepository<Venda> _vendaRepository;
        private readonly VendaValidator _validator;
        private readonly IMapper _mapper;

        public VendaService(IRepository<Venda> vendaRepository, VendaValidator validator, IMapper mapper)
        {
            _vendaRepository = vendaRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public IEnumerable<VendaDTO> ListarTodas()
        {
            var vendas = _vendaRepository.ListarTodos();
            return _mapper.Map<IEnumerable<VendaDTO>>(vendas);
        }

        public VendaDTO ObterPorId(int id)
        {
            var venda = _vendaRepository.ObterPorId(id);
            return venda == null ? null : _mapper.Map<VendaDTO>(venda);
        }

        public void Adicionar(VendaDTO vendaDto)
        {
            // Validação antes de salvar
            _validator.Validar(vendaDto);

            // Mapeia o DTO para a entidade Venda
            var venda = _mapper.Map<Venda>(vendaDto);
            _vendaRepository.Adicionar(venda);
        }

        public void Atualizar(VendaDTO vendaDto)
        {
            var vendaExistente = _vendaRepository.ObterPorId(vendaDto.Id);
            if (vendaExistente == null)
                throw new ArgumentException("Venda não encontrada.");

            // Validação antes de atualizar
            _validator.Validar(vendaDto);

            // Atualiza os campos da venda existente com os valores do DTO
            vendaExistente.DataVenda = vendaDto.DataVenda;
            vendaExistente.Quantidade = vendaDto.Quantidade;

            _vendaRepository.Atualizar(vendaExistente);
        }

        public void Remover(int id)
        {
            var venda = _vendaRepository.ObterPorId(id);
            if (venda == null)
                throw new ArgumentException("Venda não encontrada.");

            _vendaRepository.Remover(id);
        }
    }
}
