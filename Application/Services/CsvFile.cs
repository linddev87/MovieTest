
using Domain.Models;
namespace Application.Services
{
    public class CsvFile<TOut> : ICsvFile<TOut> where TOut : class, IDto
    {
        private readonly IEnumerable<string> _headers;
        private readonly IEnumerable<string> _lines;

        private CsvFile(IEnumerable<string> headers, IEnumerable<string> lines)
        {
            _headers = headers;
            _lines = lines;
        }

        public static async Task<ICsvFile<TOut>> LoadFile(string path)
        {
            var allLines = await File.ReadAllLinesAsync(path);

            if (!allLines.Any())
            {
                throw new InvalidOperationException("Cannot import empty CSV file.");
            }

            var headers = allLines.First().ToLower().Split(',').ToList();
            var propNames = typeof(TOut).GetProperties().Select(p => p.Name).ToList();
            var validation = ValidateHeaders<TOut>(headers, propNames);

            if (validation.Failed())
            {
                throw new InvalidDataException(validation.GetErrorMessage());
            }

            return new CsvFile<TOut>(headers, allLines.Skip(1));
        }


        private static InputValidation ValidateHeaders<T>(List<string> headers, List<string> propNames)
        {
            var missingHeaders = propNames.Except(headers).ToList();
            var inputValidation = new InputValidation();

            if (missingHeaders.Any())
            {
                inputValidation.AddError(string.Format("The CSV file does not contain headers matching the following properties '{0}' required by the type '{1}'.", string.Join(", ", missingHeaders), typeof(T)));
            }

            return inputValidation;
        }

        public Task<IEnumerable<TOut>> GetDtos()
        {
            throw new NotImplementedException();
        }

        public async Task<List<MovieImportDto>> GetEntities(string path)
        {
            var lines = await File.ReadAllLinesAsync(path);
            var headers = lines.FirstOrDefault()?.ToLower().Split(',').ToList();

            if(headers is null || headers.Count != 2 || !headers.Contains("title") || !headers.Contains(""))
            {
                throw new InvalidDataException("");
            }

            var titleIndex = headers?.IndexOf("title");
            var 


            lines = lines.Skip(1).ToArray();

            return lines.Select(l => new List<Movie>(l.Split()));
        }
    }
}
