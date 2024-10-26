using AVALIAÇÃO_A4.Classes;
using Microsoft.EntityFrameworkCore;

namespace AVALIAÇÃO_A4.DataBase
{
    public class DbContextToMemory : DbContext
    {

        public DbContextToMemory(DbContextOptions<DbContextToMemory> options)
    : base(options)
        {
        }
        public virtual DbSet<Remedio> Remedio { get; set; }
        public virtual DbSet<Receita> Receita { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Venda> Venda { get; set; }

    }
}
