using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.DataBase.Models;
using AVALIAÇÃO_A4.Repository;
using AVALIAÇÃO_A4.Validate;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AVALIAÇÃO_A4.Service
{
    public class EstoqueRemedioService
    {
        private readonly EstoqueRemedioRepository _estoqueRepository;
        private readonly RemedioRepository _remedioRepository;
        private readonly EstoqueRemedioValidator _validator;
        private readonly IMapper _mapper;
        private readonly ILogger<EstoqueRemedioService> _logger;

        public EstoqueRemedioService(
            EstoqueRemedioRepository estoqueRepository,
            RemedioRepository remedioRepository,
            EstoqueRemedioValidator validator,
            IMapper mapper,
            ILogger<EstoqueRemedioService> logger)
        {
            _estoqueRepository = estoqueRepository;
            _remedioRepository = remedioRepository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        // Listar todos os remédios no estoque
        public IEnumerable<EstoqueRemedioDTO> ListarRemediosEmEstoque()
        {
            _logger.LogInformation("Listando todos os remédios disponíveis em estoque.");

            var estoques = _estoqueRepository.ListarTodos();

            if (estoques == null || !estoques.Any())
            {
                _logger.LogWarning("Nenhum remédio disponível em estoque.");
                return Enumerable.Empty<EstoqueRemedioDTO>();
            }

            return _mapper.Map<IEnumerable<EstoqueRemedioDTO>>(estoques);
        }

        // Adicionar quantidade ao estoque de um remédio existente
        public void AdicionarAoEstoque(int remedioId, int quantidade)
        {
            _logger.LogInformation($"Adicionando quantidade ao estoque para o remédio com ID {remedioId}.");

            // Verifica se o remédio existe no repositório de remédios
            var remedio = _remedioRepository.ObterPorId(remedioId);
            if (remedio == null)
            {
                _logger.LogWarning($"Remédio com ID {remedioId} não encontrado.");
                throw new KeyNotFoundException($"Remédio com ID {remedioId} não encontrado.");
            }

            // Valida os parâmetros
            _validator.ValidarAtualizar(remedioId, quantidade);

            // Recupera o estoque associado ao remédio
            var estoque = _estoqueRepository.ObterPorId(remedioId);

            if (estoque == null)
            {
                // Se o estoque não existir, cria um novo registro
                estoque = new EstoqueRemedio
                {
                    RemedioId = remedioId,
                    Quantidade = quantidade,
                    Lote = $"Lote-{remedioId}-{DateTime.UtcNow:yyyyMMdd}"
                };
                _estoqueRepository.Adicionar(estoque);
                _logger.LogInformation($"Novo estoque criado para o remédio com ID {remedioId}. Quantidade: {quantidade}.");
            }
            else
            {
                // Se o estoque existir, atualiza a quantidade
                estoque.Quantidade += quantidade;
                _estoqueRepository.Atualizar(estoque);
                _logger.LogInformation($"Quantidade atualizada no estoque para o remédio com ID {remedioId}. Nova quantidade: {estoque.Quantidade}.");
            }
        }
    }
}
