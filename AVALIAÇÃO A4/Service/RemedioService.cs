using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.DataBase.Models;
using AVALIAÇÃO_A4.Repository;
using AVALIAÇÃO_A4.Validate;
using AutoMapper;
using AVALIAÇÃO_A4.Mappings;

namespace AVALIAÇÃO_A4.Service
{
    public class RemedioService
    {
        private readonly RemedioRepository _remedioRepository;
        private readonly RemedioValidator _validator;
        private readonly IMapper _mapper;
        private readonly ILogger<RemedioService> _logger;

        public RemedioService(DbContextToMemory context, ILogger<RemedioService> logger)
        {
            _remedioRepository = new RemedioRepository(context);
            _logger = logger; // Simplesmente atribuímos o logger
            _validator = new RemedioValidator();


            // Faço a instância do mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            // Validação do mapper
            config.AssertConfigurationIsValid();
            _mapper = config.CreateMapper();
        }

        public IEnumerable<RemedioDTO> ListarTodos()
        {
            _logger.LogInformation("Iniciando a listagem de todos os remédios.");

            try
            {
                // Recupera os remédios do repositório
                var remedios = _remedioRepository.ListarTodos();

                // Verifica se a lista de remédios está vazia
                if (remedios == null || !remedios.Any())
                {
                    _logger.LogWarning("Nenhum remédio encontrado no banco de dados.");
                    return Enumerable.Empty<RemedioDTO>();
                }

                // Realiza o mapeamento dos remédios para DTO
                var remediosDto = _mapper.Map<IEnumerable<RemedioDTO>>(remedios);

                _logger.LogInformation("Listagem de remédios concluída com sucesso.");
                return remediosDto;
            }
            catch (Exception ex)
            {
                // Registra o erro e propaga a exceção
                _logger.LogError($"Erro ao listar todos os remédios: {ex.Message}", ex);
                throw new ApplicationException("Erro ao listar os remédios. Por favor, tente novamente mais tarde.");
            }
        }


        public RemedioDTO ObterPorId(int id)
        {
            try
            {
                var remedio = _remedioRepository.ObterPorId(id);
                return remedio == null ? null : _mapper.Map<RemedioDTO>(remedio);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter remédio com ID {id}: {ex.Message}");
                throw;
            }
        }

        public RemedioDTO Adicionar(RemedioDTO remedioDto)
        {
            try
            {
                _validator.Validar(remedioDto);
                var remedio = _mapper.Map<Remedio>(remedioDto);
                _remedioRepository.Adicionar(remedio);
                return _mapper.Map<RemedioDTO>(remedio);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao adicionar remédio: {ex.Message}");
                throw;
            }
        }

        public RemedioDTO Atualizar(int id, RemedioDTO remedioDto)
        {
            try
            {
                var remedioExistente = _remedioRepository.ObterPorId(id);
                if (remedioExistente == null)
                {
                    _logger.LogWarning($"Remédio com ID {id} não encontrado.");
                    throw new ArgumentException("Remédio não encontrado.");
                }

                if (!string.IsNullOrEmpty(remedioDto.Nome))
                    remedioExistente.Nome = remedioDto.Nome;

                if (remedioDto.UnidadeMedida != null)
                    remedioExistente.UnidadeMedida = remedioDto.UnidadeMedida;

                if (remedioDto.PrecisaReceita != remedioExistente.PrecisaReceita)
                    remedioExistente.PrecisaReceita = remedioDto.PrecisaReceita;

                _validator.Validar(_mapper.Map<RemedioDTO>(remedioExistente));
                _remedioRepository.Atualizar(remedioExistente);

                return _mapper.Map<RemedioDTO>(remedioExistente);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar parcialmente o remédio com ID {id}: {ex.Message}");
                throw;
            }
        }

        public void Remover(int id)
        {
            try
            {
                var remedio = _remedioRepository.ObterPorId(id);
                if (remedio == null)
                {
                    _logger.LogWarning($"Remédio com ID {id} não encontrado.");
                    throw new ArgumentException("Remédio não encontrado.");
                }

                _remedioRepository.Remover(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao remover remédio com ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}
