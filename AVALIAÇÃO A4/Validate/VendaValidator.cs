using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.DataBase.Models;
using AVALIAÇÃO_A4.Interface;

namespace AVALIAÇÃO_A4.Validate
{
    public class VendaValidator
    {
        private readonly IRepository<Cliente> _clienteRepository;
        private readonly IRepository<Remedio> _remedioRepository;

        public VendaValidator(IRepository<Cliente> clienteRepository, IRepository<Remedio> remedioRepository)
        {
            _clienteRepository = clienteRepository;
            _remedioRepository = remedioRepository;
        }

        public void Validar(VendaDTO vendaDto)
        {
            // Verifica se o Cliente existe
            var cliente = _clienteRepository.ObterPorId(vendaDto.ClienteId);
            if (cliente == null)
                throw new ArgumentException("Cliente não encontrado.");

            // Verifica se o Remedio existe
            var remedio = _remedioRepository.ObterPorId(vendaDto.RemedioId);
            if (remedio == null)
                throw new ArgumentException("Remédio não encontrado.");

            // Verifica a quantidade
            if (vendaDto.Quantidade <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");
        }
    }
}
