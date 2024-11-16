using AVALIAÇÃO_A4.DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace AVALIAÇÃO_A4.DataBase
{
    public class DbContextToMemory : DbContext
    {

        public DbContextToMemory(DbContextOptions<DbContextToMemory> options)
    : base(options)
        {
        }
        public virtual DbSet<Remedy> Remedy { get; set; }
        public virtual DbSet<Recipe> Recipe { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Sale> Sale { get; set; }

    }
}
