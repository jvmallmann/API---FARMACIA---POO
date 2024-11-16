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
    // responsável por gerenciar as operações relacionadas aos clientes
    // implementa a interface icustomerservice para garantir consistência e contrato nos métodos
    public class CustomerService : ICustomerService
    {
        private readonly CustomerRepository _clienteRepository;
        private readonly CustomerValidator _validator;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerService> _logger;

        // construtor que inicializa o repositório, validador, logger e mapper
        public CustomerService(DbContextToMemory context, ILogger<CustomerService> logger)
        {
            _clienteRepository = new CustomerRepository(context);
            _logger = logger;
            _validator = new CustomerValidator();

            // configuração do automapper para mapeamento entre entidades e dtos
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            config.AssertConfigurationIsValid(); // valida a configuração do mapper
            _mapper = config.CreateMapper();
        }

        // método para listar todos os clientes
        public IEnumerable<CustomerDTO> GetAll()
        {
            _logger.LogInformation("Listando todos os clientes.");

            // recupera a lista de clientes do repositório
            var clientes = _clienteRepository.GetAll();

            // verifica se a lista está vazia
            if (clientes == null || !clientes.Any())
            {
                _logger.LogWarning("Nenhum cliente encontrado.");
                return Enumerable.Empty<CustomerDTO>(); // retorna uma lista vazia
            }

            // mapeia as entidades para dtos e retorna
            return _mapper.Map<IEnumerable<CustomerDTO>>(clientes);
        }

        // método para obter um cliente específico pelo id
        public CustomerDTO GetId(int id)
        {
            _logger.LogInformation($"Buscando cliente com ID {id}.");
            var cliente = _clienteRepository.GetId(id);

            if (cliente == null)
            {
                _logger.LogWarning($"Cliente com ID {id} não encontrado.");
                return null;
            }

            return _mapper.Map<CustomerDTO>(cliente);
        }

        // método para adicionar um novo cliente
        public void Add(CustomerDTO clienteDto)
        {
            _logger.LogInformation("Adicionando novo cliente.");
            _logger.LogInformation($"DTO recebido: {clienteDto.Nome}, {clienteDto.CPF}, {clienteDto.PossuiReceita}");

            // valida o cliente dto
            _validator.Validar(clienteDto);
            _logger.LogInformation("DTO validado com sucesso.");

            // mapeia o dto para entidade e adiciona ao repositório
            var cliente = _mapper.Map<Customer>(clienteDto);
            _logger.LogInformation($"Mapeamento realizado. Cliente: {cliente.Nome}, {cliente.CPF}, {cliente.PossuiReceita}");

            _clienteRepository.Add(cliente);

            _logger.LogInformation($"Cliente adicionado com sucesso. ID: {cliente.Id}");
        }

        // método para atualizar um cliente existente
        public void Update(CustomerDTO clienteDto)
        {
            _logger.LogInformation($"Atualizando cliente com ID {clienteDto.Id}.");
            var clienteExistente = _clienteRepository.GetId(clienteDto.Id);

            // verifica se o cliente existe
            if (clienteExistente == null)
            {
                _logger.LogWarning($"Cliente com ID {clienteDto.Id} não encontrado.");
                return;
            }

            // valida os dados de entrada
            _validator.Validar(clienteDto);

            // atualiza os campos necessários
            clienteExistente.Nome = clienteDto.Nome ?? clienteExistente.Nome;
            clienteExistente.CPF = clienteDto.CPF ?? clienteExistente.CPF;

            _clienteRepository.Update(clienteExistente);
            _logger.LogInformation($"Cliente com ID {clienteDto.Id} atualizado com sucesso.");
        }

        // método para remover um cliente
        public void Delete(int id)
        {
            _logger.LogInformation($"Removendo cliente com ID {id}.");
            var cliente = _clienteRepository.GetId(id);

            // verifica se o cliente existe antes de remover
            if (cliente == null)
            {
                _logger.LogWarning($"Cliente com ID {id} não encontrado.");
                return;
            }

            _clienteRepository.Delete(id);
            _logger.LogInformation($"Cliente com ID {id} removido com sucesso.");
        }
    }
}
