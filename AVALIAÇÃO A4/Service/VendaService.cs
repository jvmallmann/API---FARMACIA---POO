using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.DataBase.Models;
using AVALIAÇÃO_A4.Repository;
using AVALIAÇÃO_A4.Validate;
using AutoMapper;
using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.Interface;
using AVALIAÇÃO_A4.Mappings;
using AVALIAÇÃO_A4.Exceptions;

namespace AVALIAÇÃO_A4.Service
{
    public class VendaService : IVendaService
    {
        private readonly ClienteRepository _clienteRepository;
        private readonly RemedioRepository _remedioRepository;
        private readonly VendaRepository _vendaRepository;
        private readonly VendaValidator _validator;
        private readonly IMapper _mapper;
        private readonly ILogger<VendaService> _logger;

        public VendaService(DbContextToMemory context, ILogger<VendaService> logger)
        {
            _vendaRepository = new VendaRepository(context);
            _clienteRepository = new ClienteRepository(context); // Adiciona o repositório de clientes
            _remedioRepository = new RemedioRepository(context);
            _logger = logger;

            // Adicionando repositórios necessários para o VendaValidator
            var clienteRepository = new ClienteRepository(context);
            var remedioRepository = new RemedioRepository(context);
            var receitaRepository = new ReceitaRepository(context);
            var estoqueValidator = new EstoqueRemedioValidator(new EstoqueRemedioRepository(context));

            _validator = new VendaValidator(clienteRepository, remedioRepository, receitaRepository, estoqueValidator);

            // Configurando o AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            config.AssertConfigurationIsValid();
            _mapper = config.CreateMapper();
        }

        public IEnumerable<VendaDTO> ListarTodas()
        {
            _logger.LogInformation("Listando todas as vendas.");

            // Recupera as vendas do repositório
            var vendas = _vendaRepository.ListarTodos();

            // Verifica se as vendas são nulas ou vazias
            if (vendas == null || !vendas.Any())
            {
                _logger.LogWarning("Nenhuma venda encontrada.");
                return Enumerable.Empty<VendaDTO>(); // Retorna uma lista vazia
            }

            // Mapeia as vendas para VendaDTO usando o AutoMapper
            return _mapper.Map<IEnumerable<VendaDTO>>(vendas);
        }

        public VendaDTO ObterPorId(int id)
        {
            _logger.LogInformation($"Buscando venda com ID {id}.");
            var venda = _vendaRepository.ObterPorId(id);

            if (venda == null)
            {
                _logger.LogWarning($"Venda com ID {id} não encontrada.");
                return null;
            }

            return _mapper.Map<VendaDTO>(venda);
        }

        public void Adicionar(VendaDTO vendaDto)
        {
            _logger.LogInformation("Adicionando nova venda.");

            // Busca o cliente e o remédio pelo ID
            var cliente = _clienteRepository.ObterPorId(vendaDto.ClienteId);
            if (cliente == null)
            {
                _logger.LogWarning($"Cliente com ID {vendaDto.ClienteId} não encontrado.");
                throw new VendaValidationException("Cliente não encontrado.");
            }

            var remedio = _remedioRepository.ObterPorId(vendaDto.RemedioId);
            if (remedio == null)
            {
                _logger.LogWarning($"Remédio com ID {vendaDto.RemedioId} não encontrado.");
                throw new VendaValidationException("Remédio não encontrado.");
            }

            // Valida a venda
            _validator.Validar(vendaDto, cliente, remedio);

            // Mapeia a venda e adiciona no repositório
            var venda = _mapper.Map<Venda>(vendaDto);
            _vendaRepository.Adicionar(venda);

            _logger.LogInformation($"Venda adicionada com sucesso. ID: {venda.Id}");
        }


        public void Atualizar(VendaDTO vendaDto)
        {
            _logger.LogInformation($"Atualizando venda com ID {vendaDto.Id}");

            // Verifica se a venda existe
            var vendaExistente = _vendaRepository.ObterPorId(vendaDto.Id);
            if (vendaExistente == null)
            {
                _logger.LogWarning($"Venda com ID {vendaDto.Id} não encontrada.");
                throw new ArgumentException("Venda não encontrada.");
            }

            // Busca o cliente e o remédio pelo ID
            var cliente = _clienteRepository.ObterPorId(vendaDto.ClienteId);
            if (cliente == null)
            {
                _logger.LogWarning($"Cliente com ID {vendaDto.ClienteId} não encontrado.");
                throw new VendaValidationException("Cliente não encontrado.");
            }

            var remedio = _remedioRepository.ObterPorId(vendaDto.RemedioId);
            if (remedio == null)
            {
                _logger.LogWarning($"Remédio com ID {vendaDto.RemedioId} não encontrado.");
                throw new VendaValidationException("Remédio não encontrado.");
            }

            // Valida os dados antes de atualizar
            _validator.Validar(vendaDto, cliente, remedio);

            // Atualiza os campos necessários
            vendaExistente.DataVenda = vendaDto.DataVenda;
            vendaExistente.Quantidade = vendaDto.Quantidade;

            _vendaRepository.Atualizar(vendaExistente);
            _logger.LogInformation($"Venda com ID {vendaDto.Id} atualizada com sucesso.");
        }



        public void Remover(int id)
        {
            _logger.LogInformation($"Removendo venda com ID {id}.");
            var venda = _vendaRepository.ObterPorId(id);

            if (venda == null)
            {
                _logger.LogWarning($"Venda com ID {id} não encontrada.");
                throw new ArgumentException("Venda não encontrada.");
            }

            _vendaRepository.Remover(id);
            _logger.LogInformation($"Venda com ID {id} removida com sucesso.");
        }
    }
}
