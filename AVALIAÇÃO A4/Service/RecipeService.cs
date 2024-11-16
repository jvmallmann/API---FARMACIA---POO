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
    // responsável pelas operações relacionadas às receitas
    public class RecipeService
    {
        private readonly RecipeRepository _receitaRepository; // acesso ao repositório de receitas
        private readonly RecipeValidator _validator; // responsável pela validação das receitas
        private readonly IMapper _mapper; // realiza mapeamento entre entidades e DTOs
        private readonly ILogger<RecipeService> _logger; // utilizado para logar as operações

        // Construtor inicializa repositório, logger, validador e mapper
        public RecipeService(DbContextToMemory context, ILogger<RecipeService> logger)
        {
            _receitaRepository = new RecipeRepository(context);
            _logger = logger; // registra logs
            _validator = new RecipeValidator(); // instancia o validador

            // configuração do AutoMapper para mapeamento
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            // valida a configuração do mapper e cria o mapeador
            config.AssertConfigurationIsValid();
            _mapper = config.CreateMapper();
        }

        // lista todas as receitas
        public IEnumerable<RecipeDTO> GetAll()
        {
            _logger.LogInformation("Listando todas as receitas.");

            // recupera as receitas do repositório
            var receitas = _receitaRepository.GetAll();

            // verifica se há receitas registradas
            if (receitas == null || !receitas.Any())
            {
                _logger.LogWarning("Nenhuma receita encontrada.");
                return Enumerable.Empty<RecipeDTO>();
            }

            // mapeia entidades para DTOs e retorna
            return _mapper.Map<IEnumerable<RecipeDTO>>(receitas);
        }

        // obtém uma receita específica pelo id
        public RecipeDTO GetId(int id)
        {
            _logger.LogInformation($"Buscando receita com ID {id}.");
            var receita = _receitaRepository.GetId(id);

            // verifica se a receita existe
            if (receita == null)
            {
                _logger.LogWarning($"Receita com ID {id} não encontrada.");
                return null;
            }

            // mapeia a entidade para DTO
            return _mapper.Map<RecipeDTO>(receita);
        }

        // adiciona uma nova receita
        public void Add(RecipeDTO receitaDto)
        {
            _logger.LogInformation("Adicionando nova receita.");

            // valida o DTO antes de salvar
            _validator.Validar(receitaDto);

            // mapeia o DTO para entidade e adiciona no repositório
            var receita = _mapper.Map<Recipe>(receitaDto);
            _receitaRepository.Add(receita);

            _logger.LogInformation($"Receita adicionada com sucesso. ID: {receita.Id}");
        }

        // atualiza uma receita existente
        public void Update(RecipeDTO receitaDto)
        {
            _logger.LogInformation($"Atualizando receita com ID {receitaDto.Id}.");

            // busca a receita pelo ID
            var receitaExistente = _receitaRepository.GetId(receitaDto.Id);

            // verifica se a receita existe
            if (receitaExistente == null)
            {
                _logger.LogWarning($"Receita com ID {receitaDto.Id} não encontrada.");
                return;
            }

            // valida os dados do DTO
            _validator.Validar(receitaDto);

            // realiza atualização parcial nos campos
            receitaExistente.Numero = receitaDto.Numero ?? receitaExistente.Numero;
            receitaExistente.Medico = receitaDto.Medico ?? receitaExistente.Medico;

            // salva a atualização
            _receitaRepository.Update(receitaExistente);
            _logger.LogInformation($"Receita com ID {receitaDto.Id} atualizada com sucesso.");
        }

        // remove uma receita pelo id
        public void Delete(int id)
        {
            _logger.LogInformation($"Removendo receita com ID {id}.");

            // busca a receita pelo id
            var receita = _receitaRepository.GetId(id);

            // verifica se a receita existe
            if (receita == null)
            {
                _logger.LogWarning($"Receita com ID {id} não encontrada.");
                return;
            }

            // remove a receita
            _receitaRepository.Delete(id);
            _logger.LogInformation($"Receita com ID {id} removida com sucesso.");
        }

        // lista receitas associadas a um cliente específico
        public IEnumerable<RecipeDTO> RecipePerCustomer(int clienteId)
        {
            _logger.LogInformation($"Listando receitas para o cliente com ID {clienteId}.");

            // recupera receitas vinculadas ao cliente
            var receitas = _receitaRepository.RecipePerCustomer(clienteId);

            // mapeia as receitas para DTOs
            return _mapper.Map<IEnumerable<RecipeDTO>>(receitas);
        }
    }
}
