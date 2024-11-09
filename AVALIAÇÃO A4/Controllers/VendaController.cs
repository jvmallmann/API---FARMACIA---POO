using Microsoft.AspNetCore.Mvc;
using AVALIAÇÃO_A4.Validate;
using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.Interface;

namespace AVALIAÇÃO_A4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendaController : ControllerBase
    {
        private readonly IVendaService _vendaService;
        private readonly VendaValidator _validator;

        public VendaController(IVendaService vendaService, VendaValidator validator)
        {
            _vendaService = vendaService;
            _validator = validator;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VendaDTO>> ListarTodas()
        {
            try
            {
                var vendaDtos = _vendaService.ListarTodas();
                return Ok(vendaDtos);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<VendaDTO> ObterPorId(int id)
        {
            try
            {
                var vendaDto = _vendaService.ObterPorId(id);
                if (vendaDto == null)
                    return NotFound("Venda não encontrada.");

                return Ok(vendaDto);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] VendaDTO vendaDto)
        {
            try
            {
                _validator.Validar(vendaDto);

                _vendaService.Adicionar(vendaDto); // Chamando o método sem atribuir
                return CreatedAtAction(nameof(ObterPorId), new { id = vendaDto.Id }, vendaDto);
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
        public IActionResult Atualizar(int id, [FromBody] VendaDTO vendaDto)
        {
            try
            {
                vendaDto.Id = id;

                _validator.Validar(vendaDto);

                _vendaService.Atualizar(vendaDto); // Chamando o método sem atribuir
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
                _vendaService.Remover(id); // Chamando o método sem atribuir
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
