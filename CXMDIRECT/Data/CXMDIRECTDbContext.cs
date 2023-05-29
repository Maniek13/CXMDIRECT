using CXMDIRECT.DbModels;
using Microsoft.EntityFrameworkCore;

namespace CXMDIRECT.Data
{
    public class CXMDIRECTDbContext : DbContext
    {
        private readonly string _connectionString;
        public CXMDIRECTDbContext(string connectionString) 
        {
            _connectionString = connectionString;
        }
        public CXMDIRECTDbContext(DbContextOptions options) : base(options){ }
        public DbSet<ExceptionLogDbModel> ExceptionsLogs { get; set; }
        public DbSet<NodeDbModel> Nodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.EnableSensitiveDataLogging();

                var builder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
                IConfiguration configuration = builder.Build();

                string connection = configuration.GetConnectionString(_connectionString) ?? _connectionString;

                optionsBuilder.UseSqlServer(connection);
            }
        }
    }
}