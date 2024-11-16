using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.DataBase.Models;
using AVALIAÇÃO_A4.Interface;
using AVALIAÇÃO_A4.Validate;
using AutoMapper;
using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.Repository;
using AVALIAÇÃO_A4.Mappings;

namespace AVALIAÇÃO_A4.Service
{
    public class ClienteService : IClienteService
    {
        private readonly ClienteRepository _clienteRepository;
        private readonly ClienteValidator _validator;
        private readonly IMapper _mapper;
        private readonly ILogger<ClienteService> _logger;


        public ClienteService(DbContextToMemory context, ILogger<ClienteService> logger)
        {
            _clienteRepository = new ClienteRepository(context);
            _logger = logger;
            _validator = new ClienteValidator();

            // Faço a instância do mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            // Validação do mapper
            config.AssertConfigurationIsValid();
            _mapper = config.CreateMapper();
        }
        public IEnumerable<ClienteDTO> ListarTodos()
        {
            _logger.LogInformation("Listando todos os clientes.");

            // Recupera a lista de clientes do repositório
            var clientes = _clienteRepository.ListarTodos();

            // Verifica se a lista de clientes é nula ou vazia (opcional)
            if (clientes == null || !clientes.Any())
            {
                _logger.LogWarning("Nenhum cliente encontrado.");
                return Enumerable.Empty<ClienteDTO>(); // Retorna uma lista vazia
            }

            // Mapeia os objetos Cliente para ClienteDTO
            return _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
        }


        public ClienteDTO ObterPorId(int id)
        {
            _logger.LogInformation($"Buscando cliente com ID {id}.");
            var cliente = _clienteRepository.ObterPorId(id);

            if (cliente == null)
            {
                _logger.LogWarning($"Cliente com ID {id} não encontrado.");
                return null;
            }

            return _mapper.Map<ClienteDTO>(cliente);
        }

        public void Adicionar(ClienteDTO clienteDto)
        {
            _logger.LogInformation("Adicionando novo cliente.");
            _logger.LogInformation($"DTO recebido: {clienteDto.Nome}, {clienteDto.CPF}, {clienteDto.PossuiReceita}");

            _validator.Validar(clienteDto);

            _logger.LogInformation("DTO validado com sucesso.");

            var cliente = _mapper.Map<Cliente>(clienteDto);
            _logger.LogInformation($"Mapeamento realizado. Cliente: {cliente.Nome}, {cliente.CPF}, {cliente.PossuiReceita}");

            _clienteRepository.Adicionar(cliente);

            _logger.LogInformation($"Cliente adicionado com sucesso. ID: {cliente.Id}");
        }


        public void Atualizar(ClienteDTO clienteDto)
        {
            _logger.LogInformation($"Atualizando cliente com ID {clienteDto.Id}.");
            var clienteExistente = _clienteRepository.ObterPorId(clienteDto.Id);

            if (clienteExistente == null)
            {
                _logger.LogWarning($"Cliente com ID {clienteDto.Id} não encontrado.");
                return;
            }

            _validator.Validar(clienteDto);

            clienteExistente.Nome = clienteDto.Nome ?? clienteExistente.Nome;
            clienteExistente.CPF = clienteDto.CPF ?? clienteExistente.CPF;

            _clienteRepository.Atualizar(clienteExistente);
            _logger.LogInformation($"Cliente com ID {clienteDto.Id} atualizado com sucesso.");
        }

        public void Remover(int id)
        {
            _logger.LogInformation($"Removendo cliente com ID {id}.");
            var cliente = _clienteRepository.ObterPorId(id);

            if (cliente == null)
            {
                _logger.LogWarning($"Cliente com ID {id} não encontrado.");
                return;
            }

            _clienteRepository.Remover(id);
            _logger.LogInformation($"Cliente com ID {id} removido com sucesso.");
        }
    }
}
