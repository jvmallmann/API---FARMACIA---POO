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
    // serviço para operações relacionadas a remédios
    public class RemedyService
    {
        private readonly RemedyRepository _remedioRepository; // repositório de dados de remédios
        private readonly RemedyValidator _validator; // validador de dados para remédios
        private readonly IMapper _mapper; // gerencia mapeamento entre entidades e DTOs
        private readonly ILogger<RemedyService> _logger; // logs para monitoramento

        // inicializa dependências necessárias
        public RemedyService(DbContextToMemory context, ILogger<RemedyService> logger)
        {
            _remedioRepository = new RemedyRepository(context);
            _logger = logger;
            _validator = new RemedyValidator();

            // configura mapeamento de entidades e DTOs
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            config.AssertConfigurationIsValid(); // valida configuração
            _mapper = config.CreateMapper(); // cria instância do mapeador
        }

        // lista todos os remédios disponíveis
        public IEnumerable<RemedyDTO> GetAll()
        {
            _logger.LogInformation("Iniciando a listagem de todos os remédios.");

            try
            {
                var remedios = _remedioRepository.GetAll();

                if (remedios == null || !remedios.Any())
                {
                    _logger.LogWarning("Nenhum remédio encontrado no banco de dados.");
                    return Enumerable.Empty<RemedyDTO>();
                }

                var remediosDto = _mapper.Map<IEnumerable<RemedyDTO>>(remedios);
                _logger.LogInformation("Listagem de remédios concluída com sucesso.");
                return remediosDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao listar todos os remédios: {ex.Message}", ex);
                throw new ApplicationException("Erro ao listar os remédios. Por favor, tente novamente mais tarde.");
            }
        }

        // obtém um remédio específico pelo id
        public RemedyDTO GetId(int id)
        {
            try
            {
                var remedio = _remedioRepository.GetId(id);
                return remedio == null ? null : _mapper.Map<RemedyDTO>(remedio);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter remédio com ID {id}: {ex.Message}");
                throw;
            }
        }

        // adiciona um novo remédio ao sistema
        public RemedyDTO Add(RemedyDTO remedioDto)
        {
            try
            {
                _validator.Validar(remedioDto); // valida os dados fornecidos
                var remedio = _mapper.Map<Remedy>(remedioDto); // converte DTO para entidade
                _remedioRepository.Add(remedio); // salva no banco de dados
                return _mapper.Map<RemedyDTO>(remedio); // retorna entidade salva como DTO
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao adicionar remédio: {ex.Message}");
                throw;
            }
        }

        // atualiza os dados de um remédio existente
        public RemedyDTO Update(int id, RemedyDTO remedioDto)
        {
            try
            {
                var remedioExistente = _remedioRepository.GetId(id);
                if (remedioExistente == null)
                {
                    _logger.LogWarning($"Remédio com ID {id} não encontrado.");
                    throw new ArgumentException("Remédio não encontrado.");
                }

                // atualiza apenas campos modificados
                if (!string.IsNullOrEmpty(remedioDto.Nome))
                    remedioExistente.Nome = remedioDto.Nome;

                if (remedioDto.UnidadeMedida != null)
                    remedioExistente.UnidadeMedida = remedioDto.UnidadeMedida;

                if (remedioDto.PrecisaReceita != remedioExistente.PrecisaReceita)
                    remedioExistente.PrecisaReceita = remedioDto.PrecisaReceita;

                _validator.Validar(_mapper.Map<RemedyDTO>(remedioExistente)); // valida a atualização
                _remedioRepository.Update(remedioExistente); // salva no banco

                return _mapper.Map<RemedyDTO>(remedioExistente);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar parcialmente o remédio com ID {id}: {ex.Message}");
                throw;
            }
        }

        // remove um remédio do sistema
        public void Delete(int id)
        {
            try
            {
                var remedio = _remedioRepository.GetId(id);
                if (remedio == null)
                {
                    _logger.LogWarning($"Remédio com ID {id} não encontrado.");
                    throw new ArgumentException("Remédio não encontrado.");
                }

                _remedioRepository.Delete(id); // remove do banco
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao remover remédio com ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}
