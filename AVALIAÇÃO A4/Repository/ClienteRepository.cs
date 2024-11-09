using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.DataBase.Models;
using AVALIAÇÃO_A4.Interface;

namespace AVALIAÇÃO_A4.Repository
{
    public class ClienteRepository : IRepository<Cliente>
    {
        private readonly DbContextToMemory _context;

        public ClienteRepository(DbContextToMemory context)
        {
            _context = context;
        }

        public IEnumerable<Cliente> ListarTodos()
        {
            return _context.Cliente.ToList();
        }

        public Cliente ObterPorId(int id)
        {
            return _context.Cliente.Find(id);
        }

        public void Adicionar(Cliente cliente)
        {
            _context.Cliente.Add(cliente);
            _context.SaveChanges();
        }

        public void Atualizar(Cliente cliente)
        {
            _context.Cliente.Update(cliente);
            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            var cliente = _context.Cliente.Find(id);
            if (cliente != null)
            {
                _context.Cliente.Remove(cliente);
                _context.SaveChanges();
            }
        }
    }
}
