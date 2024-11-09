using Microsoft.AspNetCore.Mvc;
using AVALIAÇÃO_A4.Validate;
using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.Interface;

namespace AVALIAÇÃO_A4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly ClienteValidator _validator;

        public ClienteController(IClienteService clienteService, ClienteValidator validator)
        {
            _clienteService = clienteService;
            _validator = validator;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClienteDTO>> ListarTodos()
        {
            try
            {
                var clienteDtos = _clienteService.ListarTodos();
                return Ok(clienteDtos);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ClienteDTO> ObterPorId(int id)
        {
            try
            {
                var clienteDto = _clienteService.ObterPorId(id);
                if (clienteDto == null)
                    return NotFound("Cliente não encontrado.");

                return Ok(clienteDto);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] ClienteDTO clienteDto)
        {
            try
            {
                _validator.Validar(clienteDto);

                _clienteService.Adicionar(clienteDto); // Chamando o método sem atribuir
                return CreatedAtAction(nameof(ObterPorId), new { id = clienteDto.Id }, clienteDto);
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
        public IActionResult Atualizar(int id, [FromBody] ClienteDTO clienteDto)
        {
            try
            {
                clienteDto.Id = id;

                _validator.Validar(clienteDto);

                _clienteService.Atualizar(clienteDto); // Chamando o método sem atribuir
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
                _clienteService.Remover(id); // Chamando o método sem atribuir
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
