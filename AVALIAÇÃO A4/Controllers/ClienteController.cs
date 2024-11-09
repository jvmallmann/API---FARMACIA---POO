using Microsoft.AspNetCore.Mvc;
using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.DataBase.Models;

namespace AVALIAÇÃO_A4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly DbContextToMemory _dbContext;

        public ClienteController(DbContextToMemory dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClienteDTO>> ListarTodos()
        {
            var clientes = _dbContext.Cliente.Select(c => new ClienteDTO
            {
                Id = c.Id,
                Nome = c.Nome,
                CPF = c.CPF
            }).ToList();

            return Ok(clientes); // Retorna 200 OK com a lista de clientes
        }

        [HttpGet("{id}")]
        public ActionResult<ClienteDTO> ObterPorId(int id)
        {
            var cliente = _dbContext.Cliente.FirstOrDefault(c => c.Id == id);
            if (cliente == null)
                return NotFound("Cliente não encontrado."); // Retorna 404 Not Found se o cliente não existir

            var clienteDto = new ClienteDTO
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                CPF = cliente.CPF
            };

            return Ok(clienteDto); // Retorna 200 OK com o cliente encontrado
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] ClienteDTO clienteDto)
        {
            if (string.IsNullOrWhiteSpace(clienteDto.Nome) || string.IsNullOrWhiteSpace(clienteDto.CPF))
            {
                return BadRequest("Nome e CPF são obrigatórios."); // Retorna 400 Bad Request se os dados forem inválidos
            }

            var cliente = new Cliente
            {
                Nome = clienteDto.Nome,
                CPF = clienteDto.CPF
            };

            _dbContext.Cliente.Add(cliente);
            _dbContext.SaveChanges();
            clienteDto.Id = cliente.Id;

            return CreatedAtAction(nameof(ObterPorId), new { id = clienteDto.Id }, clienteDto); // Retorna 201 Created com o novo cliente
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] ClienteDTO clienteDto)
        {
            var cliente = _dbContext.Cliente.Find(id);
            if (cliente == null)
                return NotFound("Cliente não encontrado."); // Retorna 404 Not Found se o cliente não existir

            cliente.Nome = clienteDto.Nome;
            cliente.CPF = clienteDto.CPF;

            _dbContext.SaveChanges();
            return NoContent(); // Retorna 204 No Content após atualização bem-sucedida
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var cliente = _dbContext.Cliente.Find(id);
            if (cliente == null)
                return NotFound("Cliente não encontrado."); // Retorna 404 Not Found se o cliente não existir

            _dbContext.Cliente.Remove(cliente);
            _dbContext.SaveChanges();
            return NoContent(); // Retorna 204 No Content após remoção bem-sucedida
        }
    }
}
