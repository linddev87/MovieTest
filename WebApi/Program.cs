
using Application.Interfaces;
using Application.Services;
using Infrastructure;
using Infrastructure.Repos;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("appsettings.json", false, true);

            builder.Logging.ClearProviders();
            builder.Host.UseSerilog(new LoggerConfiguration().WriteTo.File($"{Directory.GetCurrentDirectory()}/logging/log.log", Serilog.Events.LogEventLevel.Warning).CreateLogger());

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite($"Data Source={builder.Configuration["Sqlite3DbPath"]}"));
            builder.Services.AddControllers();
            builder.Services.AddMemoryCache();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IMovieRepository, MovieRepository>();
            builder.Services.AddScoped<IMovieService, MovieService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
