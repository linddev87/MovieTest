﻿namespace CsvImportApp
{
    /// <summary>
    /// Simple console app responsible for executing 
    /// </summary>
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            using var host = BuildHost();

            var context = host.Services.GetRequiredService<ApplicationDbContext>();

            context.Database.EnsureCreated();
            context.Database.Migrate();

            Console.WriteLine();
            Console.WriteLine($"Running movie import.");
            Console.WriteLine();

            var csvImportService = host.Services.GetRequiredService<IGenericImportService<MovieImportDto, Movie>>();
            var inserted = await csvImportService.RunImport();

            Console.WriteLine();
            Console.WriteLine($"Import complete. Inserted {inserted} new movies. Press any key to exit.");
            Console.ReadLine();
            //await host.RunAsync();
        }

        private static IHost BuildHost()
        {
            var builder = Host.CreateApplicationBuilder();
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.Logging.AddSerilog(new LoggerConfiguration().WriteTo.File($"{Directory.GetCurrentDirectory()}/logging/log.log", Serilog.Events.LogEventLevel.Warning).CreateLogger());

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite($"Data Source={builder.Configuration["Sqlite3DbPath"]}"));
            builder.Services.AddScoped<IGenericRepository<Movie>, GenericRepository<Movie>>();
            builder.Services.AddScoped<IGenericImportService<MovieImportDto, Movie>, CsvImportService<MovieImportDto, Movie>>();
            builder.Services.AddScoped<IGenericFileHandler, FileHandler>();

            return builder.Build();
        }
    }
}