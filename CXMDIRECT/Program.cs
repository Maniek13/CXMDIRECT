using CXMDIRECT.Data;
using Microsoft.EntityFrameworkCore;

namespace CXMDIRECT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMvc();
            builder.Services.AddMvcCore();

            builder.Services.AddDbContext<CXMDIRECTDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("CXMDIRECTConnection")));

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapControllerRoute(
              name: "default",
              pattern: "{controller=TreeTests}/{action=Index}"
            );


            app.Run();
        }
    }
}