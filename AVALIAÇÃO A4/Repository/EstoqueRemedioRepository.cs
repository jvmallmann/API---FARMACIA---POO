using System.Collections.Generic;
using System.Linq;
using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.DataBase.Models;
using AVALIAÇÃO_A4.Interface;
using Microsoft.EntityFrameworkCore;

namespace AVALIAÇÃO_A4.Repository
{
    public class EstoqueRemedioRepository : IRepository<EstoqueRemedio>
    {
        private readonly DbContextToMemory _context;

        public EstoqueRemedioRepository(DbContextToMemory context)
        {
            _context = context;
        }

        public IEnumerable<EstoqueRemedio> ListarTodos()
        {
            return _context.EstoqueRemedio.ToList();
        }

        public EstoqueRemedio ObterPorId(int id)
        {
            return _context.EstoqueRemedio.Find(id);
        }

        public void Adicionar(EstoqueRemedio estoqueRemedio)
        {
            _context.EstoqueRemedio.Add(estoqueRemedio);
            _context.SaveChanges();
        }

        public void Atualizar(EstoqueRemedio estoqueRemedio)
        {
            _context.EstoqueRemedio.Update(estoqueRemedio);
            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            var estoqueRemedio = ObterPorId(id);
            if (estoqueRemedio != null)
            {
                _context.EstoqueRemedio.Remove(estoqueRemedio);
                _context.SaveChanges();
            }
        }

        // Método adicional específico para EstoqueRemedios
        public IEnumerable<EstoqueRemedio> ObterEstoquePorRemedioId(int remedioId)
        {
            return _context.EstoqueRemedio
                           .Where(e => e.RemedioId == remedioId)
                           .ToList();
        }
    }
}
