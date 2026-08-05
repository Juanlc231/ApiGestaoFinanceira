using ApiGestaoFinanceira.Dto.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiGestaoFinanceira.Connection
{
    public class ConnectionContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ConnectionContext(IConfiguration configuration) => _configuration = configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));

        public DbSet<User> Users { get; set; }
        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<Goals> Goals { get; set; }
    }
}
