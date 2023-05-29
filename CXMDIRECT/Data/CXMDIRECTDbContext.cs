using CXMDIRECT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace CXMDIRECT.Data
{
    public class CXMDIRECTDbContext : DbContext
    {
        private readonly string _connectionString;
        public CXMDIRECTDbContext(string connectionString) 
        {
            _connectionString = connectionString;
        }

        public CXMDIRECTDbContext(){ }
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