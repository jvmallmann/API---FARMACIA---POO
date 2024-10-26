using AVALIAÇÃO_A4.Classes;
using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AVALIAÇÃO_A4.Repositorio
{
    public class ReceitaRepositorio : IRepository<Receita>
    {
        private readonly DbContextToMemory _context;

        public ReceitaRepositorio(DbContextToMemory context)
        {
            _context = context;
        }

        public IEnumerable<Receita> ListarTodos()
        {
            return _context.Receita.ToList(); // Recupera todas as receitas de forma síncrona
        }

        public Receita ObterPorId(int id)
        {
            return _context.Receita.Find(id); // Recupera uma receita pelo ID de forma síncrona
        }

        public void Adicionar(Receita receita)
        {
            _context.Receita.Add(receita);
            _context.SaveChanges(); // Salva mudanças de forma síncrona
        }

        public void Atualizar(Receita receita)
        {
            _context.Receita.Update(receita);
            _context.SaveChanges(); // Salva mudanças de forma síncrona
        }

        public void Remover(int id)
        {
            var receita = _context.Receita.Find(id);
            if (receita != null)
            {
                _context.Receita.Remove(receita);
                _context.SaveChanges(); // Salva mudanças de forma síncrona
            }
        }
    }
}
