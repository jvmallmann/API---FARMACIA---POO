using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.DataBase.Models;
using AVALIAÇÃO_A4.Interface;


namespace AVALIAÇÃO_A4.Repository
{
    public class VendaRepository : IRepository<Venda>
    {
        private readonly DbContextToMemory _context;

        public VendaRepository(DbContextToMemory context)
        {
            _context = context;
        }

        public IEnumerable<Venda> ListarTodos()
        {
            return _context.Venda.ToList();
        }

        public Venda ObterPorId(int id)
        {
            return _context.Venda.Find(id);
        }

        public void Adicionar(Venda venda)
        {
            _context.Venda.Add(venda);
            _context.SaveChanges();
        }

        public void Atualizar(Venda venda)
        {
            _context.Venda.Update(venda);
            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            var venda = _context.Venda.Find(id);
            if (venda != null)
            {
                _context.Venda.Remove(venda);
                _context.SaveChanges();
            }
        }
    }
}
