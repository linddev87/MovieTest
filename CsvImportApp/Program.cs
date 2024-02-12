using Application.Models;
using Application.Services;
using Domain.Entities;

namespace CsvImportApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var file = CsvFileHandler<MovieImportDto>.LoadFromPath(args[0]);
        }
    }
}
