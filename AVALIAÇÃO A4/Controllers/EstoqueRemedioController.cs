using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.Service;
using Microsoft.AspNetCore.Mvc;

namespace AVALIAÇÃO_A4.Controllers
{
    [Route("api/estoque")]
    [ApiController]
    public class EstoqueRemedioController : ControllerBase
    {
        private readonly EstoqueRemedioService _service;
        private readonly ILogger<EstoqueRemedioController> _logger;

        public EstoqueRemedioController(EstoqueRemedioService service, ILogger<EstoqueRemedioController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: api/estoque
        [HttpGet]
        public ActionResult<IEnumerable<EstoqueRemedioDTO>> ListarRemediosEmEstoque()
        {
            _logger.LogInformation("Recebida solicitação para listar remédios em estoque.");
            try
            {
                var estoques = _service.ListarRemediosEmEstoque();
                if (!estoques.Any())
                {
                    _logger.LogWarning("Nenhum remédio disponível no estoque.");
                    return NoContent(); // Retorna 204 se o estoque estiver vazio
                }
                return Ok(estoques);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao listar remédios em estoque: {ex.Message}");
                return StatusCode(500, "Erro interno ao listar remédios em estoque.");
            }
        }

        // POST: api/estoque/adicionar
        [HttpPost("adicionar")]
        public IActionResult AdicionarQuantidadeAoEstoque(int remedioId, int quantidade)
        {
            _logger.LogInformation($"Recebida solicitação para adicionar quantidade ao estoque do remédio com ID {remedioId}.");
            try
            {
                _service.AdicionarAoEstoque(remedioId, quantidade);
                _logger.LogInformation($"Quantidade adicionada com sucesso ao remédio com ID {remedioId}.");
                return Ok($"Quantidade adicionada ao remédio com ID {remedioId}. Nova quantidade atualizada.");
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning($"Erro de validação: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao adicionar quantidade ao estoque: {ex.Message}");
                return StatusCode(500, "Erro interno ao adicionar quantidade ao estoque.");
            }
        }
    }
}
