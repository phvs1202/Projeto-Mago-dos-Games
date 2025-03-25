using APIJogos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Org.BouncyCastle.Asn1.Crmf;

namespace APIJogos.Repository
{
    public class DBUserContext : DbContext
    {
        public DBUserContext(DbContextOptions<DBUserContext> options) : base(options) 
        {
        }

        public DbSet<user> user { get; set; }
    }
}
