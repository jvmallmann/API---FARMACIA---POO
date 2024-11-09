using Microsoft.AspNetCore.Mvc;
using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.DataBase.Models;

namespace AVALIAÇÃO_A4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendaController : ControllerBase
    {
        private readonly DbContextToMemory _dbContext;

        public VendaController(DbContextToMemory dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VendaDTO>> ListarTodas()
        {
            var vendas = _dbContext.Venda.Select(v => new VendaDTO
            {
                Id = v.Id,
                ClienteId = v.ClienteId,
                RemedioId = v.RemedioId,
                DataVenda = v.DataVenda,
                Quantidade = v.Quantidade
            }).ToList();

            return Ok(vendas); // Retorna 200 OK com a lista de vendas
        }

        [HttpGet("{id}")]
        public ActionResult<VendaDTO> ObterPorId(int id)
        {
            var venda = _dbContext.Venda.FirstOrDefault(v => v.Id == id);
            if (venda == null)
                return NotFound("Venda não encontrada."); // Retorna 404 Not Found se a venda não existir

            var vendaDto = new VendaDTO
            {
                Id = venda.Id,
                ClienteId = venda.ClienteId,
                RemedioId = venda.RemedioId,
                DataVenda = venda.DataVenda,
                Quantidade = venda.Quantidade
            };

            return Ok(vendaDto); // Retorna 200 OK com a venda encontrada
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] VendaDTO vendaDto)
        {
            // Validação básica
            if (vendaDto.ClienteId <= 0 || vendaDto.RemedioId <= 0 || vendaDto.Quantidade <= 0)
            {
                return BadRequest("Cliente, Remédio e Quantidade são obrigatórios e devem ser válidos.");
            }

            var venda = new Venda
            {
                ClienteId = vendaDto.ClienteId,
                RemedioId = vendaDto.RemedioId,
                DataVenda = vendaDto.DataVenda == default ? DateTime.Now : vendaDto.DataVenda,
                Quantidade = vendaDto.Quantidade
            };

            _dbContext.Venda.Add(venda);
            _dbContext.SaveChanges();
            vendaDto.Id = venda.Id;

            return CreatedAtAction(nameof(ObterPorId), new { id = vendaDto.Id }, vendaDto); // Retorna 201 Created com a nova venda
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] VendaDTO vendaDto)
        {
            var venda = _dbContext.Venda.Find(id);
            if (venda == null)
                return NotFound("Venda não encontrada."); // Retorna 404 Not Found se a venda não existir

            venda.ClienteId = vendaDto.ClienteId;
            venda.RemedioId = vendaDto.RemedioId;
            venda.DataVenda = vendaDto.DataVenda;
            venda.Quantidade = vendaDto.Quantidade;

            _dbContext.SaveChanges();
            return NoContent(); // Retorna 204 No Content após atualização bem-sucedida
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var venda = _dbContext.Venda.Find(id);
            if (venda == null)
                return NotFound("Venda não encontrada."); // Retorna 404 Not Found se a venda não existir

            _dbContext.Venda.Remove(venda);
            _dbContext.SaveChanges();
            return NoContent(); // Retorna 204 No Content após remoção bem-sucedida
        }
    }
}
