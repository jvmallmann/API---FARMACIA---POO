using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.Exceptions;
using AVALIAÇÃO_A4.Service;
using Microsoft.AspNetCore.Mvc;

namespace AVALIAÇÃO_A4.Controllers
{
    [Route("api/recipes")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly RecipeService _service;
        private readonly ILogger<RecipeController> _logger;

        // construtor para service e o logger
        public RecipeController(DbContextToMemory context, ILogger<RecipeController> logger, ILogger<RecipeService> serviceLogger)
        {
            _service = new RecipeService(context, serviceLogger); // inicializa o service de receitas
            _logger = logger; // inicializa o logger do controller
        }

        // endpoint para listar todas as receitas
        [HttpGet]
        public ActionResult<IEnumerable<RecipeDTO>> GetAll()
        {
            _logger.LogInformation("listando todas as receitas."); // log de início
            try
            {
                var receitas = _service.GetAll(); // lista as receitas
                _logger.LogInformation("listagem de receitas concluída."); // log de sucesso
                return Ok(receitas); // retorna a lista com status 200
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro ao listar receitas: {ex.Message}"); // log de erro
                return StatusCode(500, "erro ao listar receitas."); // retorna status 500
            }
        }

        // endpoint para obter uma receita por id
        [HttpGet("{id}")]
        public ActionResult<RecipeDTO> GetId(int id)
        {
            _logger.LogInformation($"buscando receita com ID {id}."); // log de início
            try
            {
                var receita = _service.GetId(id); // obtem receita por id 
                if (receita == null)
                {
                    _logger.LogWarning($"receita com ID {id} não encontrada."); // log de receita não encontrada
                    return NotFound($"receita com ID {id} não encontrada."); // retorna 404
                }

                _logger.LogInformation($"receita com ID {id} encontrada."); // log de sucesso
                return Ok(receita); // retorna a receita com status 200
            }
            catch (RecipeNotFoundException ex)
            {
                _logger.LogWarning(ex.Message); // log de exceção de não encontrada
                return NotFound(ex.Message); // retorna 404
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro ao buscar receita com ID {id}: {ex.Message}"); // log de erro
                return StatusCode(500, $"erro ao buscar receita com ID {id}."); // retorna status 500
            }
        }

        // endpoint para adicionar uma nova receita
        [HttpPost]
        public IActionResult Add([FromBody] RecipeDTO receitaDto)
        {
            _logger.LogInformation("adicionando nova receita."); // log de início
            try
            {
                _service.Add(receitaDto); // adiciona uma receita
                _logger.LogInformation($"receita adicionada com sucesso. ID: {receitaDto.Id}"); // log de sucesso
                return CreatedAtAction(nameof(GetId), new { id = receitaDto.Id }, receitaDto); // retorna 201 com a nova receita
            }
            catch (RecipeValidationException ex)
            {
                _logger.LogWarning($"erro de validação: {ex.Message}"); // log de erro de validação
                return BadRequest(ex.Message); // retorna 400
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro ao adicionar receita: {ex.Message}"); // log de erro
                return StatusCode(500, "erro ao adicionar receita."); // retorna status 500
            }
        }

        // endpoint para atualizar uma receita
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] RecipeDTO receitaDto)
        {
            _logger.LogInformation($"atualizando receita com ID {id}."); // log de início
            try
            {
                receitaDto.Id = id; // garante que o id no dto seja o mesmo do parâmetro
                _service.Update(receitaDto); // atualiza a receita
                _logger.LogInformation($"receita com ID {id} atualizada com sucesso."); // log de sucesso
                return NoContent(); // retorna 204
            }
            catch (RecipeNotFoundException ex)
            {
                _logger.LogWarning(ex.Message); // log de exceção de não encontrada
                return NotFound(ex.Message); // retorna 404
            }
            catch (RecipeValidationException ex)
            {
                _logger.LogWarning(ex.Message); // log de erro de validação
                return BadRequest(ex.Message); // retorna 400
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro ao atualizar receita com ID {id}: {ex.Message}"); // log de erro
                return StatusCode(500, "erro ao atualizar receita."); // retorna status 500
            }
        }

        // endpoint para remover uma receita pelo id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"removendo receita com ID {id}."); // log de início
            try
            {
                _service.Delete(id); // remove a receita
                _logger.LogInformation($"receita com ID {id} removida com sucesso."); // log de sucesso
                return NoContent(); // retorna 204
            }
            catch (RecipeNotFoundException ex)
            {
                _logger.LogWarning(ex.Message); // log de exceção de não encontrada
                return NotFound(ex.Message); // retorna 404
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro ao remover receita com ID {id}: {ex.Message}"); // log de erro
                return StatusCode(500, "erro ao remover receita."); // retorna status 500
            }
        }
    }
}
