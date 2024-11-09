using AutoMapper;
using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.DataBase.Models;
using AVALIAÇÃO_A4.Interface;
using AVALIAÇÃO_A4.Validate;

namespace AVALIAÇÃO_A4.Service
{
    public class ClienteService : IClienteService
    {
        private readonly IRepository<Cliente> _repository;
        private readonly ClienteValidator _validator;
        private readonly IMapper _mapper;

        public ClienteService(IRepository<Cliente> repository, ClienteValidator validator, IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public IEnumerable<ClienteDTO> ListarTodos()
        {
            var clientes = _repository.ListarTodos();
            return _mapper.Map<IEnumerable<ClienteDTO>>(clientes); // Mapeamento automático de Cliente para ClienteDTO
        }

        public ClienteDTO ObterPorId(int id)
        {
            var cliente = _repository.ObterPorId(id);
            return cliente == null ? null : _mapper.Map<ClienteDTO>(cliente); // Mapeamento automático de Cliente para ClienteDTO
        }

        public void Adicionar(ClienteDTO clienteDto)
        {
            try
            {
                _validator.Validar(clienteDto);

                // Mapeamento de ClienteDTO para Cliente
                var cliente = _mapper.Map<Cliente>(clienteDto);

                _repository.Adicionar(cliente);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro ao adicionar cliente: {ex.Message}");
            }
        }

        public void Atualizar(ClienteDTO clienteDto)
        {
            try
            {
                var cliente = _repository.ObterPorId(clienteDto.Id);
                if (cliente == null) 

                _validator.Validar(clienteDto);

                // Atualiza o cliente usando o mapeamento do DTO
                _mapper.Map(clienteDto, cliente);

                _repository.Atualizar(cliente);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro ao atualizar cliente: {ex.Message}");
            }
        }

        public void Remover(int id)
        {
            var cliente = _repository.ObterPorId(id);
            if (cliente == null)

            _repository.Remover(id);
           
        }
    }
}
