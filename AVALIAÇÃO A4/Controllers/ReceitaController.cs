using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.Exceptions;
using AVALIAÇÃO_A4.Service;
using Microsoft.AspNetCore.Mvc;

namespace AVALIAÇÃO_A4.Controllers
{
    [Route("api/receitas")]
    [ApiController]
    public class ReceitaController : ControllerBase
    {
        private readonly ReceitaService _service;
        private readonly ILogger<ReceitaController> _logger;

        public ReceitaController(DbContextToMemory context, ILogger<ReceitaController> logger, ILogger<ReceitaService> serviceLogger)
        {
            _service = new ReceitaService(context, serviceLogger);
            _logger = logger; // Sem necessidade de validação explícita
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReceitaDTO>> GetReceitas()
        {
            try
            {
                return Ok(_service.ListarTodos());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao listar receitas: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ReceitaDTO> GetReceitaById(int id)
        {
            try
            {
                var receita = _service.ObterPorId(id);
                if (receita == null)
                    return NotFound($"Receita com ID {id} não encontrada.");

                return Ok(receita);
            }
            catch (ReceitaNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter receita com ID {id}: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpPost]
        public IActionResult AddReceita([FromBody] ReceitaDTO receitaDto)
        {
            try
            {
                _service.Adicionar(receitaDto);
                return CreatedAtAction(nameof(GetReceitaById), new { id = receitaDto.Id }, receitaDto);
            }
            catch (ReceitaValidationException ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao adicionar receita: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateReceita(int id, [FromBody] ReceitaDTO receitaDto)
        {
            try
            {
                _service.Atualizar(receitaDto);
                return Ok();
            }
            catch (ReceitaNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }
            catch (ReceitaValidationException ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar receita com ID {id}: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReceita(int id)
        {
            try
            {
                _service.Remover(id);
                return NoContent();
            }
            catch (ReceitaNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao deletar receita com ID {id}: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
}
