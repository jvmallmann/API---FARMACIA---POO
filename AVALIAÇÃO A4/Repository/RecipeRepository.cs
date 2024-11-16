using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.DataBase.Models;
using AVALIAÇÃO_A4.Interface;
using Microsoft.EntityFrameworkCore;

namespace AVALIAÇÃO_A4.Repository
{
    // repositório para a recipe
    // implementa a interface genérica irepository para realizar operações no banco de dados
    public class RecipeRepository : IRepository<Recipe>
    {
        private readonly DbContextToMemory _context;

        public RecipeRepository(DbContextToMemory context)
        {
            _context = context;
        }

        // método para buscar todas as receitas no banco
        public IEnumerable<Recipe> GetAll()
        {
            // retorna todas as receitas disponíveis no dbset recipe
            return _context.Recipe.ToList();
        }

        // método para buscar uma receita pelo id
        public Recipe GetId(int id)
        {
            // busca a receita pelo id utilizando o método find
            return _context.Recipe.Find(id);
        }

        // método para adicionar uma nova receita
        public void Add(Recipe receita)
        {
            // adiciona a receita ao dbset e salva as alterações no banco
            _context.Recipe.Add(receita);
            _context.SaveChanges();
        }

        // método para atualizar uma receita existente
        public void Update(Recipe receita)
        {
            // atualiza os campos da receita e persiste as mudanças no banco
            _context.Recipe.Update(receita);
            _context.SaveChanges();
        }

        // método para remover uma receita pelo id
        public void Delete(int id)
        {
            // busca a receita no banco pelo id
            var receita = _context.Recipe.Find(id);

            // verifica se a receita existe antes de remover
            if (receita != null)
            {
                _context.Recipe.Remove(receita);
                _context.SaveChanges();
            }
        }

        // método para buscar todas as receitas associadas a um cliente específico
        public IEnumerable<Recipe> RecipePerCustomer(int clienteId)
        {
            // retorna as receitas filtradas pelo id do cliente
            return _context.Recipe.Where(r => r.ClienteId == clienteId).ToList();
        }
    }
}
