using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.DataBase.Models;
using AVALIAÇÃO_A4.Interface;

namespace AVALIAÇÃO_A4.Repository
{
    public class ReceitaRepository : IRepository<Receita>
    {
        private readonly DbContextToMemory _context;

        public ReceitaRepository(DbContextToMemory context)
        {
            _context = context;
        }

        public IEnumerable<Receita> ListarTodos()
        {
            return _context.Receita.ToList();
        }

        public Receita ObterPorId(int id)
        {
            return _context.Receita.Find(id);
        }

        public void Adicionar(Receita receita)
        {
            _context.Receita.Add(receita);
            _context.SaveChanges();
        }

        public void Atualizar(Receita receita)
        {
            _context.Receita.Update(receita);
            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            var receita = _context.Receita.Find(id);
            if (receita != null)
            {
                _context.Receita.Remove(receita);
                _context.SaveChanges();
            }
        }
    }
}
