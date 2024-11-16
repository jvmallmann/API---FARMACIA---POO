using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.Exceptions;
using AVALIAÇÃO_A4.Service;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

[ApiController]
[Route("api/vendas")]
public class VendaController : ControllerBase
{
    private readonly VendaService _service;
    private readonly ILogger<VendaController> _logger;

    public VendaController(DbContextToMemory context, ILogger<VendaController> logger, ILogger<VendaService> serviceLogger)
    {
        _service = new VendaService(context, serviceLogger);
        _logger = logger;
    }

    [HttpGet("{id}")]
    public ActionResult<VendaDTO> ObterPorId(int id)
    {
        _logger.LogInformation($"Recebida solicitação para obter a venda com ID {id}.");
        try
        {
            var venda = _service.ObterPorId(id);
            return Ok(venda);
        }
        catch (VendaNotFoundException ex)
        {
            _logger.LogWarning(ex.Message);
            return NotFound(ex.Message); // 404 Not Found
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao buscar venda com ID {id}: {ex.Message}");
            return StatusCode(500, "Erro interno ao buscar venda."); // 500 Internal Server Error
        }
    }

    [HttpPost]
    public IActionResult AdicionarVenda([FromBody] VendaDTO vendaDto)
    {
        _logger.LogInformation("Recebida solicitação para adicionar uma nova venda.");
        try
        {
            _service.Adicionar(vendaDto);
            _logger.LogInformation("Venda adicionada com sucesso.");
            return Ok("Venda realizada com sucesso.");
        }
        catch (VendaValidationException ex)
        {
            _logger.LogWarning($"Erro de validação ao realizar a venda: {ex.Message}");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao realizar a venda: {ex.Message}");
            return StatusCode(500, "Erro interno ao realizar a venda.");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Remover(int id)
    {
        _logger.LogInformation($"Recebida solicitação para remover a venda com ID {id}.");
        try
        {
            _service.Remover(id);
            _logger.LogInformation($"Venda com ID {id} removida com sucesso.");
            return NoContent(); // 204 No Content
        }
        catch (VendaNotFoundException ex)
        {
            _logger.LogWarning(ex.Message);
            return NotFound(ex.Message); // 404 Not Found
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao remover venda com ID {id}: {ex.Message}");
            return StatusCode(500, "Erro interno ao remover venda."); // 500 Internal Server Error
        }
    }
}
