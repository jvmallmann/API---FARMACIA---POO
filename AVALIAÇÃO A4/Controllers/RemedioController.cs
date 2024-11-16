using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.Exceptions;
using AVALIAÇÃO_A4.Service;
using Microsoft.AspNetCore.Mvc;

namespace AVALIAÇÃO_A4.Controllers
{
    [Route("api/remedios")]
    [ApiController]
    public class RemedioController : ControllerBase
    {
        private readonly RemedioService _service;
        private readonly ILogger<RemedioController> _logger;

        public RemedioController(DbContextToMemory context, ILogger<RemedioController> logger, ILogger<RemedioService> serviceLogger)
        {
            _service = new RemedioService(context, serviceLogger);
            _logger = logger; // Sem necessidade de validação explícita

        }

        [HttpGet]
        public ActionResult<IEnumerable<RemedioDTO>> ListarTodos()
        {
            try
            {
                var remedios = _service.ListarTodos();
                _logger.LogInformation("Listagem de remédios concluída com sucesso.");
                return Ok(remedios);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao listar remédios: {ex.Message}");
                return StatusCode(500, "Erro ao listar remédios.");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<RemedioDTO> ObterPorId(int id)
        {
            _logger.LogInformation($"Recebida solicitação para obter o remédio com ID {id}.");
            try
            {
                var remedio = _service.ObterPorId(id);
                if (remedio == null)
                {
                    _logger.LogWarning($"Remédio com ID {id} não encontrado.");
                    return NotFound($"Remédio com ID {id} não encontrado.");
                }

                _logger.LogInformation($"Remédio com ID {id} encontrado com sucesso.");
                return Ok(remedio);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter remédio com ID {id}: {ex.Message}");
                return StatusCode(500, $"Erro ao obter remédio com ID {id}.");
            }
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] RemedioDTO remedioDto)
        {
            _logger.LogInformation("Recebida solicitação para adicionar um novo remédio.");
            try
            {
                _service.Adicionar(remedioDto);
                _logger.LogInformation($"Remédio adicionado com sucesso. ID: {remedioDto.Id}");
                return CreatedAtAction(nameof(ObterPorId), new { id = remedioDto.Id }, remedioDto);
            }
            catch (RemedioValidationException ex)
            {
                _logger.LogWarning($"Erro de validação ao adicionar remédio: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao adicionar remédio: {ex.Message}");
                return StatusCode(500, "Erro ao adicionar remédio.");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult Atualizar(int id, [FromBody] RemedioDTO remedioDto)
        {
            _logger.LogInformation($"Recebida solicitação para atualizar parcialmente o remédio com ID {id}.");
            try
            {
                _service.Atualizar( id, remedioDto);
                _logger.LogInformation($"Remédio com ID {id} atualizado com sucesso.");
                return NoContent();
            }
            catch (RemedioNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }
            catch (RemedioValidationException ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar remédio com ID {id}: {ex.Message}");
                return StatusCode(500, "Erro ao atualizar remédio.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            _logger.LogInformation($"Recebida solicitação para remover o remédio com ID {id}.");
            try
            {
                _service.Remover(id);
                _logger.LogInformation($"Remédio com ID {id} removido com sucesso.");
                return NoContent();
            }
            catch (RemedioNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao remover remédio com ID {id}: {ex.Message}");
                return StatusCode(500, "Erro ao remover remédio.");
            }
        }
    }
}
