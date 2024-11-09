using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.DataBase.Models;
using AVALIAÇÃO_A4.Interface;
using AVALIAÇÃO_A4.Validate;

namespace AVALIAÇÃO_A4.Service
{
    public class VendaService : IVendaService
    {
        private readonly IRepository<Venda> _repository;
        private readonly VendaValidator _validator;

        public VendaService(IRepository<Venda> repository, VendaValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public IEnumerable<VendaDTO> ListarTodas()
        {
            var vendas = _repository.ListarTodos();
            return vendas.Select(v => new VendaDTO
            {
                Id = v.Id,
                ClienteId = v.ClienteId,
                RemedioId = v.RemedioId,
                DataVenda = v.DataVenda,
                Quantidade = v.Quantidade
            });
        }

        public VendaDTO ObterPorId(int id)
        {
            var venda = _repository.ObterPorId(id);
            if (venda == null) return null;

            return new VendaDTO
            {
                Id = venda.Id,
                ClienteId = venda.ClienteId,
                RemedioId = venda.RemedioId,
                DataVenda = venda.DataVenda,
                Quantidade = venda.Quantidade
            };
        }

        public bool Adicionar(VendaDTO vendaDto)
        {
            try
            {
                _validator.Validar(vendaDto);

                var venda = new Venda
                {
                    ClienteId = vendaDto.ClienteId,
                    RemedioId = vendaDto.RemedioId,
                    DataVenda = vendaDto.DataVenda == default ? DateTime.Now : vendaDto.DataVenda,
                    Quantidade = vendaDto.Quantidade
                };

                _repository.Adicionar(venda);
                return true;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro ao adicionar venda: {ex.Message}");
                return false;
            }
        }

        public bool Atualizar(VendaDTO vendaDto)
        {
            try
            {
                var venda = _repository.ObterPorId(vendaDto.Id);
                if (venda == null) return false;

                _validator.Validar(vendaDto);

                venda.ClienteId = vendaDto.ClienteId;
                venda.RemedioId = vendaDto.RemedioId;
                venda.DataVenda = vendaDto.DataVenda;
                venda.Quantidade = vendaDto.Quantidade;

                _repository.Atualizar(venda);
                return true;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro ao atualizar venda: {ex.Message}");
                return false;
            }
        }

        public bool Remover(int id)
        {
            var venda = _repository.ObterPorId(id);
            if (venda == null) return false;

            _repository.Remover(id);
            return true;
        }
    }
}
