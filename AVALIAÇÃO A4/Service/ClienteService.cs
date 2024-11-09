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

        public ClienteService(IRepository<Cliente> repository, ClienteValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public IEnumerable<ClienteDTO> ListarTodos()
        {
            var clientes = _repository.ListarTodos();
            return clientes.Select(c => new ClienteDTO
            {
                Id = c.Id,
                Nome = c.Nome,
                CPF = c.CPF
            });
        }

        public ClienteDTO ObterPorId(int id)
        {
            var cliente = _repository.ObterPorId(id);
            if (cliente == null) return null;

            return new ClienteDTO
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                CPF = cliente.CPF
            };
        }

        public void Adicionar(ClienteDTO clienteDto)
        {
            try
            {
                _validator.Validar(clienteDto); // Validação com mensagens personalizadas

                var cliente = new Cliente
                {
                    Nome = clienteDto.Nome,
                    CPF = clienteDto.CPF
                };

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

                _validator.Validar(clienteDto); // Validação com mensagens personalizadas

                cliente.Nome = clienteDto.Nome;
                cliente.CPF = clienteDto.CPF;

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
