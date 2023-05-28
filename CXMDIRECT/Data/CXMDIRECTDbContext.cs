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
        public DbSet<ExceptionLogDbModel> ExceptionsLogs { get; set; }
        public DbSet<NodeDbModel> Nodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=RAKIETA\SQLEXPRESS; Database=CXMDIRECT;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }
    }
}