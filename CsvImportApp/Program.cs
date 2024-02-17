﻿
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace CsvImportApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var host = BuildHost();
            
            var context = host.Services.GetRequiredService<ApplicationDbContext>();

            context.Database.EnsureCreated();
            context.Database.Migrate();

            var csvImportService = host.Services.GetRequiredService<IGenericImportService<MovieImportDto, Movie>>();
            await csvImportService.RunImport();

            await host.RunAsync();
        }

        private static IHost BuildHost()
        {
            var builder = Host.CreateApplicationBuilder();
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var logger = new LoggerConfiguration()
            .WriteTo.File($"{Directory.GetCurrentDirectory()}/logging/log.log", Serilog.Events.LogEventLevel.Warning)
            .CreateLogger();
            builder.Logging.AddSerilog(logger);

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite($"Data Source={builder.Configuration["Sqlite3DbPath"]}"));
            builder.Services.AddScoped<IGenericRepository<Movie>, GenericRepository<Movie>>();
            builder.Services.AddScoped<IGenericImportService<MovieImportDto, Movie>, CsvImportService<MovieImportDto, Movie>>();

            return builder.Build();
        }
    }
}
