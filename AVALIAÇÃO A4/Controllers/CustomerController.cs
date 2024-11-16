using AutoMapper;
using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.Exceptions;
using AVALIAÇÃO_A4.Service;
using Microsoft.AspNetCore.Mvc;

namespace AVALIAÇÃO_A4.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _service;
        private readonly ILogger<CustomerController> _logger;

        // construtor para service e o logger
        public CustomerController(DbContextToMemory context, ILogger<CustomerController> logger, ILogger<CustomerService> serviceLogger)
        {
            _service = new CustomerService(context, serviceLogger); // inicializa o serviço de cliente
            _logger = logger; // inicializa o logger
        }

        // endpoint para listar todos os clientes
        [HttpGet]
        public ActionResult<IEnumerable<CustomerDTO>> GetAll()
        {
            _logger.LogInformation("listando todos os clientes."); // log de início
            try
            {
                var clientes = _service.GetAll(); // chama o serviço para listar clientes
                _logger.LogInformation("listagem de clientes concluída."); // log de sucesso
                return Ok(clientes); // retorna a lista com status 200
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro ao listar clientes: {ex.Message}"); // log de erro
                return StatusCode(500, "erro ao listar clientes."); // retorna status 500
            }
        }

        // endpoint para obter um cliente pelo id
        [HttpGet("{id}")]
        public ActionResult<CustomerDTO> GetId(int id)
        {
            _logger.LogInformation($"buscando cliente com ID {id}."); // log de início
            try
            {
                var cliente = _service.GetId(id); // busca cliente pelo id
                if (cliente == null)
                {
                    _logger.LogWarning($"cliente com ID {id} não encontrado."); // log de cliente não encontrado
                    return NotFound($"cliente com ID {id} não encontrado."); // retorna 404
                }

                _logger.LogInformation($"cliente com ID {id} encontrado."); // log de sucesso
                return Ok(cliente); // retorna o cliente com status 200
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro ao buscar cliente com ID {id}: {ex.Message}"); // log de erro
                return StatusCode(500, $"erro ao buscar cliente com ID {id}."); // retorna status 500
            }
        }

        // endpoint para adicionar um novo cliente
        [HttpPost]
        public IActionResult Add([FromBody] CustomerDTO clienteDto)
        {
            _logger.LogInformation("adicionando um novo cliente."); // log de início
            try
            {
                _service.Add(clienteDto); // chama o serviço para adicionar cliente
                _logger.LogInformation($"cliente adicionado com sucesso. ID: {clienteDto.Id}"); // log de sucesso
                return CreatedAtAction(nameof(GetId), new { id = clienteDto.Id }, clienteDto); // retorna 201 com o novo cliente
            }
            catch (CustomerValidationException ex)
            {
                _logger.LogWarning($"erro de validação: {ex.Message}"); // log de erro de validação
                return BadRequest(ex.Message); // retorna 400
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro ao adicionar cliente: {ex.Message}"); // log de erro
                return StatusCode(500, "erro ao adicionar cliente."); // retorna status 500
            }
        }

        // endpoint para atualizar parcialmente um cliente
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CustomerDTO clienteDto)
        {
            _logger.LogInformation($"atualizando cliente com ID {id}."); // log de início
            try
            {
                clienteDto.Id = id; // garante que o id no dto seja o mesmo do parâmetro
                _service.Update(clienteDto); // chama o serviço para atualizar cliente
                _logger.LogInformation($"cliente com ID {id} atualizado com sucesso."); // log de sucesso
                return NoContent(); // retorna 204
            }
            catch (CustomerNotFoundException ex)
            {
                _logger.LogWarning(ex.Message); // log de cliente não encontrado
                return NotFound(ex.Message); // retorna 404
            }
            catch (CustomerValidationException ex)
            {
                _logger.LogWarning(ex.Message); // log de erro de validação
                return BadRequest(ex.Message); // retorna 400
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro ao atualizar cliente com ID {id}: {ex.Message}"); // log de erro
                return StatusCode(500, "erro ao atualizar cliente."); // retorna status 500
            }
        }

        // endpoint para remover um cliente pelo id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"removendo cliente com ID {id}."); // log de início
            try
            {
                _service.Delete(id); // remove cliente
                _logger.LogInformation($"cliente com ID {id} removido com sucesso."); // log de sucesso
                return NoContent(); // retorna 204
            }
            catch (CustomerNotFoundException ex)
            {
                _logger.LogWarning(ex.Message); // log de cliente não encontrado
                return NotFound(ex.Message); // retorna 404
            }
            catch (Exception ex)
            {
                _logger.LogError($"erro ao remover cliente com ID {id}: {ex.Message}"); // log de erro
                return StatusCode(500, "erro ao remover cliente."); // retorna status 500
            }
        }
    }
}
