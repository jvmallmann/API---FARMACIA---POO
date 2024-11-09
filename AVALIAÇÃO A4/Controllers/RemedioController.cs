using Microsoft.AspNetCore.Mvc;
using AVALIAÇÃO_A4.Validate;
using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.Interface;

namespace AVALIAÇÃO_A4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RemedioController : ControllerBase
    {
        private readonly IRemedioService _remedioService;
        private readonly RemedioValidator _validator;

        public RemedioController(IRemedioService remedioService, RemedioValidator validator)
        {
            _remedioService = remedioService;
            _validator = validator;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RemedioDTO>> ListarTodos()
        {
            try
            {
                var remedioDtos = _remedioService.ListarTodos();
                return Ok(remedioDtos);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<RemedioDTO> ObterPorId(int id)
        {
            try
            {
                var remedioDto = _remedioService.ObterPorId(id);
                if (remedioDto == null)
                    return NotFound("Remédio não encontrado.");

                return Ok(remedioDto);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] RemedioDTO remedioDto)
        {
            try
            {
                _validator.Validar(remedioDto);

                _remedioService.Adicionar(remedioDto); // Chamando o método sem atribuir
                return CreatedAtAction(nameof(ObterPorId), new { id = remedioDto.Id }, remedioDto);
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
        public IActionResult Atualizar(int id, [FromBody] RemedioDTO remedioDto)
        {
            try
            {
                remedioDto.Id = id;

                _validator.Validar(remedioDto);

                _remedioService.Atualizar(remedioDto); // Chamando o método sem atribuir
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
                _remedioService.Remover(id); // Chamando o método sem atribuir
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
