using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.DataBase.Models;
using AVALIAÇÃO_A4.Interface;

namespace AVALIAÇÃO_A4.Repository
{
    public class RemedioRepository : IRepository<Remedio>
    {
        private readonly DbContextToMemory _context;

        public RemedioRepository(DbContextToMemory context)
        {
            _context = context;
        }

        public IEnumerable<Remedio> ListarTodos()
        {
            return _context.Remedio.ToList();
        }

        public Remedio ObterPorId(int id)
        {
            return _context.Remedio.Find(id);
        }

        public void Adicionar(Remedio remedio)
        {
            _context.Remedio.Add(remedio);
            _context.SaveChanges();
        }

        public void Atualizar(Remedio remedio)
        {
            _context.Remedio.Update(remedio);
            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            var remedio = _context.Remedio.Find(id);
            if (remedio != null)
            {
                _context.Remedio.Remove(remedio);
                _context.SaveChanges();
            }
        }
    }
}
