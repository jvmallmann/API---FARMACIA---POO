using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.DataBase.Models;
using AVALIAÇÃO_A4.Repository;
using AVALIAÇÃO_A4.Validate;
using AutoMapper;
using AVALIAÇÃO_A4.Interface;
using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.Mappings;
using AVALIAÇÃO_A4.Exceptions;

namespace AVALIAÇÃO_A4.Service
{
    // serviço responsável por operações relacionadas a vendas
    public class SaleService : ISaleService
    {
        private readonly CustomerRepository _clienteRepository; // repositório de clientes
        private readonly RemedyRepository _remedioRepository; // repositório de remédios
        private readonly SaleRepository _vendaRepository; // repositório de vendas
        private readonly SaleValidator _validator; // validador de vendas
        private readonly IMapper _mapper; // responsável por mapear entidades para DTOs
        private readonly ILogger<SaleService> _logger; // logs para monitoramento

        // construtor inicializando dependências e configurando o AutoMapper
        public SaleService(DbContextToMemory context, ILogger<SaleService> logger)
        {
            _vendaRepository = new SaleRepository(context);
            _clienteRepository = new CustomerRepository(context);
            _remedioRepository = new RemedyRepository(context);
            _logger = logger;

            _validator = new SaleValidator();

            // configuração do AutoMapper para mapeamento entre DTOs e entidades
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            config.AssertConfigurationIsValid();
            _mapper = config.CreateMapper();
        }

        // retorna todas as vendas como uma lista de DTOs
        public IEnumerable<SaleDTO> GetAll()
        {
            _logger.LogInformation("Listando todas as vendas.");

            // recupera todas as vendas do repositório
            var vendas = _vendaRepository.GetAll();

            // verifica se há vendas disponíveis
            if (vendas == null || !vendas.Any())
            {
                _logger.LogWarning("Nenhuma venda encontrada.");
                return Enumerable.Empty<SaleDTO>();
            }

            // mapeia entidades de vendas para DTOs e retorna
            return _mapper.Map<IEnumerable<SaleDTO>>(vendas);
        }

        // retorna uma venda específica pelo ID
        public SaleDTO GetId(int id)
        {
            _logger.LogInformation($"Buscando venda com ID {id}.");
            var venda = _vendaRepository.GetId(id);

            if (venda == null)
            {
                _logger.LogWarning($"Venda com ID {id} não encontrada.");
                return null;
            }

            // mapeia entidade para DTO
            return _mapper.Map<SaleDTO>(venda);
        }

        // adiciona uma nova venda no sistema
        public void Add(SaleDTO vendaDto)
        {
            _logger.LogInformation("Adicionando nova venda.");

            // busca o cliente associado à venda
            var cliente = _clienteRepository.GetId(vendaDto.ClienteId);
            if (cliente == null)
            {
                _logger.LogWarning($"Cliente com ID {vendaDto.ClienteId} não encontrado.");
                throw new SaleValidationException("Cliente não encontrado.");
            }

            // busca o remédio associado à venda
            var remedio = _remedioRepository.GetId(vendaDto.RemedioId);
            if (remedio == null)
            {
                _logger.LogWarning($"Remédio com ID {vendaDto.RemedioId} não encontrado.");
                throw new SaleValidationException("Remédio não encontrado.");
            }

            // valida os dados da venda antes de salvar
            _validator.Validar(vendaDto, cliente, remedio);

            // mapeia DTO para entidade e salva no repositório
            var venda = _mapper.Map<Sale>(vendaDto);
            _vendaRepository.Add(venda);

            _logger.LogInformation($"Venda adicionada com sucesso. ID: {venda.Id}");
        }

        // atualiza uma venda existente
        public void Update(SaleDTO vendaDto)
        {
            _logger.LogInformation($"Atualizando venda com ID {vendaDto.Id}");

            // verifica se a venda existe
            var vendaExistente = _vendaRepository.GetId(vendaDto.Id);
            if (vendaExistente == null)
            {
                _logger.LogWarning($"Venda com ID {vendaDto.Id} não encontrada.");
                throw new ArgumentException("Venda não encontrada.");
            }

            // busca o cliente e o remédio associados à venda
            var cliente = _clienteRepository.GetId(vendaDto.ClienteId);
            if (cliente == null)
            {
                _logger.LogWarning($"Cliente com ID {vendaDto.ClienteId} não encontrado.");
                throw new SaleValidationException("Cliente não encontrado.");
            }

            var remedio = _remedioRepository.GetId(vendaDto.RemedioId);
            if (remedio == null)
            {
                _logger.LogWarning($"Remédio com ID {vendaDto.RemedioId} não encontrado.");
                throw new SaleValidationException("Remédio não encontrado.");
            }

            // valida os dados antes de atualizar
            _validator.Validar(vendaDto, cliente, remedio);

            // atualiza os campos necessários
            vendaExistente.DataVenda = vendaDto.DataVenda;
            vendaExistente.Quantidade = vendaDto.Quantidade;

            _vendaRepository.Update(vendaExistente);
            _logger.LogInformation($"Venda com ID {vendaDto.Id} atualizada com sucesso.");
        }

        // remove uma venda pelo ID
        public void Delete(int id)
        {
            _logger.LogInformation($"Removendo venda com ID {id}.");
            var venda = _vendaRepository.GetId(id);

            if (venda == null)
            {
                _logger.LogWarning($"Venda com ID {id} não encontrada.");
                throw new ArgumentException("Venda não encontrada.");
            }

            _vendaRepository.Delete(id);
            _logger.LogInformation($"Venda com ID {id} removida com sucesso.");
        }
    }
}
