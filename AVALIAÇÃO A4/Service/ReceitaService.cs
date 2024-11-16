using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.DataBase.Models;
using AVALIAÇÃO_A4.Repository;
using AVALIAÇÃO_A4.Validate;
using AutoMapper;
using AVALIAÇÃO_A4.Interface;
using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.Mappings;

namespace AVALIAÇÃO_A4.Service
{
    public class ReceitaService : IReceitaService
    {
        private readonly ReceitaRepository _receitaRepository;
        private readonly ReceitaValidator _validator;
        private readonly IMapper _mapper;
        private readonly ILogger<ReceitaService> _logger;
        public ReceitaService(DbContextToMemory context, ILogger<ReceitaService> logger)
        {
            _receitaRepository = new ReceitaRepository(context);
            _logger = logger; // Simplesmente atribuímos o logger
            _validator = new ReceitaValidator();
            // Faço a instância do mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            // Validação do mapper
            config.AssertConfigurationIsValid();
            _mapper = config.CreateMapper();
        }

        public IEnumerable<ReceitaDTO> ListarTodos()
        {
            _logger.LogInformation("Listando todas as receitas.");

            // Verifica se o repositório está funcionando corretamente
            var receitas = _receitaRepository.ListarTodos();

            if (receitas == null || !receitas.Any())
            {
                _logger.LogWarning("Nenhuma receita encontrada.");
                return Enumerable.Empty<ReceitaDTO>(); // Retorna uma lista vazia
            }

            // Mapeia as receitas para ReceitaDTO usando o AutoMapper
            return _mapper.Map<IEnumerable<ReceitaDTO>>(receitas);
        }

        public ReceitaDTO ObterPorId(int id)
        {
            _logger.LogInformation($"Buscando receita com ID {id}.");
            var receita = _receitaRepository.ObterPorId(id);

            if (receita == null)
            {
                _logger.LogWarning($"Receita com ID {id} não encontrada.");
                return null;
            }

            return _mapper.Map<ReceitaDTO>(receita);
        }

        public void Adicionar(ReceitaDTO receitaDto)
        {
            _logger.LogInformation("Adicionando nova receita.");

            _validator.Validar(receitaDto);

            var receita = _mapper.Map<Receita>(receitaDto);
            _receitaRepository.Adicionar(receita);

            _logger.LogInformation($"Receita adicionada com sucesso. ID: {receita.Id}");
        }

        public void Atualizar(ReceitaDTO receitaDto)
        {
            _logger.LogInformation($"Atualizando receita com ID {receitaDto.Id}.");
            var receitaExistente = _receitaRepository.ObterPorId(receitaDto.Id);

            if (receitaExistente == null)
            {
                _logger.LogWarning($"Receita com ID {receitaDto.Id} não encontrada.");
                return;
            }

            _validator.Validar(receitaDto);

            // Atualização parcial dos campos
            receitaExistente.Numero = receitaDto.Numero ?? receitaExistente.Numero;
            receitaExistente.Medico = receitaDto.Medico ?? receitaExistente.Medico;

            _receitaRepository.Atualizar(receitaExistente);
            _logger.LogInformation($"Receita com ID {receitaDto.Id} atualizada com sucesso.");
        }

        public void Remover(int id)
        {
            _logger.LogInformation($"Removendo receita com ID {id}.");
            var receita = _receitaRepository.ObterPorId(id);

            if (receita == null)
            {
                _logger.LogWarning($"Receita com ID {id} não encontrada.");
                return;
            }

            _receitaRepository.Remover(id);
            _logger.LogInformation($"Receita com ID {id} removida com sucesso.");
        }

        public IEnumerable<ReceitaDTO> ObterReceitasPorCliente(int clienteId)
        {
            var receitas = _receitaRepository.ObterReceitasPorCliente(clienteId);
            return _mapper.Map<IEnumerable<ReceitaDTO>>(receitas);
        }
    }
}
