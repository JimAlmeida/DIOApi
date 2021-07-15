using System.Linq;
using Microsoft.EntityFrameworkCore;
using DIOApi.DTOs;
using DIOApi;
using Microsoft.Extensions.Configuration;

namespace DIOApi.DAL
{
    public class GameDbContext: DbContext
    {
        public DbSet<GameItem> Games { get; set; }
        private IConfiguration _configuration;

        public GameDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("sql_server"));
        }
    }
}
