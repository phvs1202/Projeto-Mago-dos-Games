using APIJogos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Org.BouncyCastle.Asn1.Crmf;

namespace APIJogos.Repository
{
    public class DBUser_gameContext : DbContext
    {
        public DBUser_gameContext(DbContextOptions<DBUser_gameContext> options) : base(options)
        {
        }

        public DbSet<usergame> usergame { get; set; }
    }
}
