using AutoMapper;
using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.Exceptions;
using AVALIAÇÃO_A4.Service;
using Microsoft.AspNetCore.Mvc;

namespace AVALIAÇÃO_A4.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _service;
        private readonly ILogger<ClienteController> _logger;

        public ClienteController(DbContextToMemory context, ILogger<ClienteController> logger, ILogger<ClienteService> serviceLogger)
        {
            _service = new ClienteService(context, serviceLogger);
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClienteDTO>> ListarTodos()
        {
            _logger.LogInformation("Recebida solicitação para listar todos os clientes.");
            try
            {
                var clientes = _service.ListarTodos();
                _logger.LogInformation("Listagem de clientes concluída com sucesso.");
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao listar clientes: {ex.Message}");
                return StatusCode(500, "Erro ao listar clientes.");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ClienteDTO> ObterPorId(int id)
        {
            _logger.LogInformation($"Recebida solicitação para obter o cliente com ID {id}.");
            try
            {
                var cliente = _service.ObterPorId(id);
                if (cliente == null)
                {
                    _logger.LogWarning($"Cliente com ID {id} não encontrado.");
                    return NotFound($"Cliente com ID {id} não encontrado.");
                }

                _logger.LogInformation($"Cliente com ID {id} encontrado com sucesso.");
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter cliente com ID {id}: {ex.Message}");
                return StatusCode(500, $"Erro ao obter cliente com ID {id}.");
            }
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] ClienteDTO clienteDto)
        {
            _logger.LogInformation("Recebida solicitação para adicionar um novo cliente.");
            try
            {
                _service.Adicionar(clienteDto);
                _logger.LogInformation($"Cliente adicionado com sucesso. ID: {clienteDto.Id}");
                return CreatedAtAction(nameof(ObterPorId), new { id = clienteDto.Id }, clienteDto);
            }
            catch (ClienteValidationException ex)
            {
                _logger.LogWarning($"Erro de validação ao adicionar cliente: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao adicionar cliente: {ex.Message}");
                return StatusCode(500, "Erro ao adicionar cliente.");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult AtualizarParcial(int id, [FromBody] ClienteDTO clienteDto)
        {
            _logger.LogInformation($"Recebida solicitação para atualizar parcialmente o cliente com ID {id}.");
            try
            {
                clienteDto.Id = id; // Garante que o ID seja atualizado
                _service.Atualizar(clienteDto);
                _logger.LogInformation($"Cliente com ID {id} atualizado com sucesso.");
                return NoContent();
            }
            catch (ClienteNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }
            catch (ClienteValidationException ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar cliente com ID {id}: {ex.Message}");
                return StatusCode(500, "Erro ao atualizar cliente.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            _logger.LogInformation($"Recebida solicitação para remover o cliente com ID {id}.");
            try
            {
                _service.Remover(id);
                _logger.LogInformation($"Cliente com ID {id} removido com sucesso.");
                return NoContent();
            }
            catch (ClienteNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao remover cliente com ID {id}: {ex.Message}");
                return StatusCode(500, "Erro ao remover cliente.");
            }
        }
    }
}
