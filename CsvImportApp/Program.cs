using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CsvImportApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var host = BuildHost();

            var csvImportService = host.Services.GetRequiredService<CsvImportService<MovieImportDto, Movie>>();
            await csvImportService.RunImport(args);

            await host.RunAsync();
        }

        private static IHost BuildHost()
        {
            var builder = Host.CreateApplicationBuilder();
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("database"));
            builder.Services.AddScoped<IGenericRepository<Movie>, GenericRepository<Movie>>();
            builder.Services.AddScoped<CsvImportService<MovieImportDto, Movie>>();

            return builder.Build();
        }
    }
}
