using APIJogos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace APIJogos.Repository
{
    public class DBJogosContext : DbContext
    {
        public DBJogosContext(DbContextOptions<DBJogosContext> options) : base(options) 
        {
        }

        public DbSet<jogo> jogo { get; set; }
    }
}
