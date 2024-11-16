using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.Exceptions;
using AVALIAÇÃO_A4.Service;
using Microsoft.AspNetCore.Mvc;

namespace AVALIAÇÃO_A4.Controllers
{
    [ApiController]
    [Route("api/sales")]
    public class SaleController : ControllerBase
    {
        private readonly SaleService _service;
        private readonly ILogger<SaleController> _logger;

        // construtor inicializa o service e o logger
        public SaleController(DbContextToMemory context, ILogger<SaleController> logger, ILogger<SaleService> serviceLogger)
        {
            _service = new SaleService(context, serviceLogger); // instancia o service de vendas
            _logger = logger; // registra as operações no controlador
        }

        // endpoint para buscar uma venda por id
        [HttpGet("{id}")]
        public ActionResult<SaleDTO> GetId(int id)
        {
            _logger.LogInformation($"buscando venda com ID {id}"); // registra a solicitação
            try
            {
                var venda = _service.GetId(id); //  busca a venda pelo id
                return Ok(venda); // retorna a venda encontrada
            }
            catch (SaleNotFoundException ex)
            {
                _logger.LogWarning(ex.Message); // log de venda não encontrada
                return NotFound(ex.Message); // retorna 404
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro ao buscar venda com ID {id}: {ex.Message}"); // log de erro interno
                return StatusCode(500, "erro interno ao buscar venda."); // retorna 500
            }
        }

        // endpoint para adicionar uma nova venda
        [HttpPost]
        public IActionResult Add([FromBody] SaleDTO vendaDto)
        {
            _logger.LogInformation("adicionando nova venda"); // registra a solicitação
            try
            {
                _service.Add(vendaDto); //  adiciona a venda
                _logger.LogInformation("venda adicionada com sucesso."); // log de sucesso
                return Ok("venda realizada com sucesso."); // retorna 200
            }
            catch (SaleValidationException ex)
            {
                _logger.LogWarning($"erro de validação ao realizar a venda: {ex.Message}"); // log de erro de validação
                return BadRequest(ex.Message); // retorna 400
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro ao realizar a venda: {ex.Message}"); // log de erro interno
                return StatusCode(500, "erro interno ao realizar a venda."); // retorna 500
            }
        }

        // endpoint para deletar uma venda
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"removendo venda com ID {id}"); // registra a solicitação
            try
            {
                _service.Delete(id); // remove a venda
                _logger.LogInformation($"venda com ID {id} removida com sucesso."); // log de sucesso
                return NoContent(); // retorna 204
            }
            catch (SaleNotFoundException ex)
            {
                _logger.LogWarning(ex.Message); // log de venda não encontrada
                return NotFound(ex.Message); // retorna 404
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro ao remover venda com ID {id}: {ex.Message}"); // log de erro interno
                return StatusCode(500, "erro interno ao remover venda."); // retorna 500
            }
        }
    }
}
