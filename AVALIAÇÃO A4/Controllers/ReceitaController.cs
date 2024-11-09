using Microsoft.AspNetCore.Mvc;
using AVALIAÇÃO_A4.Validate;
using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.Interface;

namespace AVALIAÇÃO_A4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceitaController : ControllerBase
    {
        private readonly IReceitaService _receitaService;
        private readonly ReceitaValidator _validator;

        public ReceitaController(IReceitaService receitaService, ReceitaValidator validator)
        {
            _receitaService = receitaService;
            _validator = validator;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReceitaDTO>> ListarTodas()
        {
            try
            {
                var receitaDtos = _receitaService.ListarTodas();
                return Ok(receitaDtos);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ReceitaDTO> ObterPorId(int id)
        {
            try
            {
                var receitaDto = _receitaService.ObterPorId(id);
                if (receitaDto == null)
                    return NotFound("Receita não encontrada.");

                return Ok(receitaDto);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] ReceitaDTO receitaDto)
        {
            try
            {
                // Validação antes de chamar o serviço
                _validator.Validar(receitaDto);

                _receitaService.Adicionar(receitaDto); // Chama o método sem atribuir
                return CreatedAtAction(nameof(ObterPorId), new { id = receitaDto.Id }, receitaDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Erro de validação: {ex.Message}");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] ReceitaDTO receitaDto)
        {
            try
            {
                receitaDto.Id = id;

                // Validação antes de chamar o serviço
                _validator.Validar(receitaDto);

                _receitaService.Atualizar(receitaDto); // Chama o método sem atribuir
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Erro de validação: {ex.Message}");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            try
            {
                _receitaService.Remover(id); // Chama o método sem atribuir
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
