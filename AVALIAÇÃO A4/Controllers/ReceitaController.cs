using Microsoft.AspNetCore.Mvc;
using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.DataBase.Models;

namespace AVALIAÇÃO_A4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceitaController : ControllerBase
    {
        private readonly DbContextToMemory _dbContext;

        public ReceitaController(DbContextToMemory dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReceitaDTO>> ListarTodas()
        {
            var receitas = _dbContext.Receita.Select(r => new ReceitaDTO
            {
                Id = r.Id,
                Numero = r.Numero,
                Medico = r.Medico
            }).ToList();

            return Ok(receitas); // Retorna 200 OK com a lista de receitas
        }

        [HttpGet("{id}")]
        public ActionResult<ReceitaDTO> ObterPorId(int id)
        {
            var receita = _dbContext.Receita.FirstOrDefault(r => r.Id == id);
            if (receita == null)
                return NotFound("Receita não encontrada."); // Retorna 404 Not Found se a receita não existir

            var receitaDto = new ReceitaDTO
            {
                Id = receita.Id,
                Numero = receita.Numero,
                Medico = receita.Medico
            };

            return Ok(receitaDto); // Retorna 200 OK com a receita encontrada
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] ReceitaDTO receitaDto)
        {
            // Validação básica
            if (string.IsNullOrWhiteSpace(receitaDto.Numero) || string.IsNullOrWhiteSpace(receitaDto.Medico))
            {
                return BadRequest("Número e Médico são obrigatórios."); // Retorna 400 Bad Request se os dados forem inválidos
            }

            var receita = new Receita
            {
                Numero = receitaDto.Numero,
                Medico = receitaDto.Medico
            };

            _dbContext.Receita.Add(receita);
            _dbContext.SaveChanges();
            receitaDto.Id = receita.Id; // Atribui o ID gerado

            return CreatedAtAction(nameof(ObterPorId), new { id = receitaDto.Id }, receitaDto); // Retorna 201 Created com a nova receita
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] ReceitaDTO receitaDto)
        {
            var receita = _dbContext.Receita.Find(id);
            if (receita == null)
                return NotFound("Receita não encontrada."); // Retorna 404 Not Found se a receita não existir

            receita.Numero = receitaDto.Numero;
            receita.Medico = receitaDto.Medico;

            _dbContext.SaveChanges();
            return NoContent(); // Retorna 204 No Content após atualização bem-sucedida
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var receita = _dbContext.Receita.Find(id);
            if (receita == null)
                return NotFound("Receita não encontrada."); // Retorna 404 Not Found se a receita não existir

            _dbContext.Receita.Remove(receita);
            _dbContext.SaveChanges();
            return NoContent(); // Retorna 204 No Content após remoção bem-sucedida
        }
    }
}
