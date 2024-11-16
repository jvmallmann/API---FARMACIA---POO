using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.Exceptions;
using AVALIAÇÃO_A4.Service;
using Microsoft.AspNetCore.Mvc;

namespace AVALIAÇÃO_A4.Controllers
{
    [Route("api/remedys")]
    [ApiController]
    public class RemedyController : ControllerBase
    {
        private readonly RemedyService _service;
        private readonly ILogger<RemedyController> _logger;

        // construtor para service e logger
        public RemedyController(DbContextToMemory context, ILogger<RemedyController> logger, ILogger<RemedyService> serviceLogger)
        {
            _service = new RemedyService(context, serviceLogger); // inicializa o service de remédios
            _logger = logger; // inicializa o logger do controller
        }

        // endpoint para listar todos os remédios
        [HttpGet]
        public ActionResult<IEnumerable<RemedyDTO>> GetAll()
        {
            _logger.LogInformation("listando todos os remédios."); // log de início
            try
            {
                var remedios = _service.GetAll(); // lista os remédios
                _logger.LogInformation("listagem de remédios concluída com sucesso."); // log de sucesso
                return Ok(remedios); // retorna a lista com status 200
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro ao listar remédios: {ex.Message}"); // log de erro
                return StatusCode(500, "erro ao listar remédios."); // retorna status 500
            }
        }

        // endpoint para obter um remédio pelo id
        [HttpGet("{id}")]
        public ActionResult<RemedyDTO> GetId(int id)
        {
            _logger.LogInformation($"buscando remédio com ID {id}."); // log de início
            try
            {
                var remedio = _service.GetId(id); // busca um remédio
                if (remedio == null)
                {
                    _logger.LogWarning($"remédio com ID {id} não encontrado."); // log de não encontrado
                    return NotFound($"remédio com ID {id} não encontrado."); // retorna 404
                }

                _logger.LogInformation($"remédio com ID {id} encontrado com sucesso."); // log de sucesso
                return Ok(remedio); // retorna o remédio com status 200
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro ao obter remédio com ID {id}: {ex.Message}"); // log de erro
                return StatusCode(500, $"erro ao obter remédio com ID {id}."); // retorna status 500
            }
        }

        // endpoint para adicionar um novo remédio
        [HttpPost]
        public IActionResult Add([FromBody] RemedyDTO remedioDto)
        {
            _logger.LogInformation("adicionando novo remédio."); // log de início
            try
            {
                _service.Add(remedioDto); // adiciona um remédio
                _logger.LogInformation($"remédio adicionado com sucesso. ID: {remedioDto.Id}"); // log de sucesso
                return CreatedAtAction(nameof(GetId), new { id = remedioDto.Id }, remedioDto); // retorna 201 com a nova entidade
            }
            catch (RemedyValidationException ex)
            {
                _logger.LogWarning($"erro de validação ao adicionar remédio: {ex.Message}"); // log de erro de validação
                return BadRequest(ex.Message); // retorna 400
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro ao adicionar remédio: {ex.Message}"); // log de erro
                return StatusCode(500, "erro ao adicionar remédio."); // retorna status 500
            }
        }

        // endpoint para atualizar um remédio pelo id
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] RemedyDTO remedioDto)
        {
            _logger.LogInformation($"atualizando remédio com ID {id}."); // log de início
            try
            {
                _service.Update(id, remedioDto); // atualiza o remédio
                _logger.LogInformation($"remédio com ID {id} atualizado com sucesso."); // log de sucesso
                return NoContent(); // retorna 204
            }
            catch (RemedyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message); // log de erro de não encontrado
                return NotFound(ex.Message); // retorna 404
            }
            catch (RemedyValidationException ex)
            {
                _logger.LogWarning(ex.Message); // log de erro de validação
                return BadRequest(ex.Message); // retorna 400
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro ao atualizar remédio com ID {id}: {ex.Message}"); // log de erro
                return StatusCode(500, "erro ao atualizar remédio."); // retorna status 500
            }
        }

        // endpoint para remover um remédio pelo id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"removendo remédio com ID {id}."); // log de início
            try
            {
                _service.Delete(id); // remove o remédio
                _logger.LogInformation($"remédio com ID {id} removido com sucesso."); // log de sucesso
                return NoContent(); // retorna 204
            }
            catch (RemedyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message); // log de erro de não encontrado
                return NotFound(ex.Message); // retorna 404
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro ao remover remédio com ID {id}: {ex.Message}"); // log de erro
                return StatusCode(500, "erro ao remover remédio."); // retorna status 500
            }
        }
    }
}
