using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.DataBase.Models;
using AVALIAÇÃO_A4.Interface;
using Microsoft.EntityFrameworkCore;

namespace AVALIAÇÃO_A4.Repository
{
    // repositório para  customer
    // implementa a interface genérica irepository para realizar operações no banco de dados
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly DbContextToMemory _context;

        public CustomerRepository(DbContextToMemory context)
        {
            _context = context;
        }

        // método para buscar todos os registros de clientes no banco
        public IEnumerable<Customer> GetAll()
        {
            // utiliza o método tolist para materializar a consulta
            return _context.Customer.ToList();
        }

        // método para buscar um cliente pelo id
        public Customer GetId(int id)
        {
            // utiliza o método find para localizar o registro pelo id
            return _context.Customer.Find(id);
        }

        // método para adicionar um novo cliente
        public void Add(Customer cliente)
        {
            // adiciona o cliente ao dbset e salva as mudanças no banco
            _context.Customer.Add(cliente);
            _context.SaveChanges();
        }

        // método para atualizar um cliente existente
        public void Update(Customer cliente)
        {
            // marca o cliente como modificado e salva as mudanças no banco
            _context.Customer.Update(cliente);
            _context.SaveChanges();
        }

        // método para remover um cliente pelo id
        public void Delete(int id)
        {
            // busca o cliente no banco pelo id
            var cliente = _context.Customer.Find(id);

            // se o cliente existir, remove do dbset e salva as mudanças
            if (cliente != null)
            {
                _context.Customer.Remove(cliente);
                _context.SaveChanges();
            }
        }
    }
}
