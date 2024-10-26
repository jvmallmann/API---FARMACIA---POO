using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AVALIAÇÃO_A4.Classes;
using AVALIAÇÃO_A4.Interface;

namespace AVALIAÇÃO_A4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceitaController : ControllerBase
    {
        private readonly IReceitaService _receitaService;

        public ReceitaController(IReceitaService receitaService)
        {
            _receitaService = receitaService;
        }

        // GET: api/Receita
        [HttpGet]
        public ActionResult<IEnumerable<Receita>> ListarTodos()
        {
            var receitas = _receitaService.ListarTodos();
            return Ok(receitas);
        }

        // GET: api/Receita/5
        [HttpGet("{id}")]
        public ActionResult<Receita> ObterPorId(int id)
        {
            var receita = _receitaService.ObterPorId(id);
            if (receita == null)
            {
                return NotFound();
            }
            return Ok(receita);
        }

        // POST: api/Receita
        [HttpPost]
        public IActionResult Adicionar([FromBody] Receita receita)
        {
            if (receita == null)
            {
                return BadRequest("Dados da receita inválidos.");
            }
            _receitaService.Adicionar(receita);
            return CreatedAtAction(nameof(ObterPorId), new { id = receita.Id }, receita);
        }

        // PUT: api/Receita/5
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] Receita receita)
        {
            if (receita == null || receita.Id != id)
            {
                return BadRequest("Dados da receita inválidos.");
            }
            var receitaExistente = _receitaService.ObterPorId(id);
            if (receitaExistente == null)
            {
                return NotFound();
            }
            _receitaService.Atualizar(id, receita);
            return NoContent();
        }

        // DELETE: api/Receita/5
        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var receitaExistente = _receitaService.ObterPorId(id);
            if (receitaExistente == null)
            {
                return NotFound();
            }
            _receitaService.Remover(id);
            return NoContent();
        }
    }
}
