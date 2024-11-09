using Microsoft.AspNetCore.Mvc;
using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.DataBase.Models;

namespace AVALIAÇÃO_A4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RemedioController : ControllerBase
    {
        private readonly DbContextToMemory _dbContext;

        public RemedioController(DbContextToMemory dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RemedioDTO>> ListarTodos()
        {
            var remedios = _dbContext.Remedio.Select(r => new RemedioDTO
            {
                Id = r.Id,
                Nome = r.Nome,
                UnidadeMedida = r.UnidadeMedida,
                PrecisaReceita = r.PrecisaReceita
            }).ToList();

            return Ok(remedios);
        }

        [HttpGet("{id}")]
        public ActionResult<RemedioDTO> ObterPorId(int id)
        {
            var remedio = _dbContext.Remedio.FirstOrDefault(r => r.Id == id);
            if (remedio == null)
                return NotFound("Remédio não encontrado.");

            var remedioDto = new RemedioDTO
            {
                Id = remedio.Id,
                Nome = remedio.Nome,
                UnidadeMedida = remedio.UnidadeMedida,
                PrecisaReceita = remedio.PrecisaReceita
            };

            return Ok(remedioDto);
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] RemedioDTO remedioDto)
        {
            if (string.IsNullOrWhiteSpace(remedioDto.Nome) || string.IsNullOrWhiteSpace(remedioDto.UnidadeMedida))
            {
                return BadRequest("Nome e Unidade de Medida são obrigatórios.");
            }

            var remedio = new Remedio
            {
                Nome = remedioDto.Nome,
                UnidadeMedida = remedioDto.UnidadeMedida,
                PrecisaReceita = remedioDto.PrecisaReceita
            };

            _dbContext.Remedio.Add(remedio);
            _dbContext.SaveChanges();
            remedioDto.Id = remedio.Id;

            return CreatedAtAction(nameof(ObterPorId), new { id = remedioDto.Id }, remedioDto);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] RemedioDTO remedioDto)
        {
            var remedio = _dbContext.Remedio.Find(id);
            if (remedio == null)
                return NotFound("Remédio não encontrado.");

            remedio.Nome = remedioDto.Nome;
            remedio.UnidadeMedida = remedioDto.UnidadeMedida;
            remedio.PrecisaReceita = remedioDto.PrecisaReceita;

            _dbContext.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var remedio = _dbContext.Remedio.Find(id);
            if (remedio == null)
                return NotFound("Remédio não encontrado.");

            _dbContext.Remedio.Remove(remedio);
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
