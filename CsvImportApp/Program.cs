using System.ComponentModel.DataAnnotations;
using Application.Models;
using Application.Services;
using Domain.Entities;

namespace CsvImportApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var file = new CsvImportService(args[0]);
            await file.ImportFromFile();
        }
    }
}
