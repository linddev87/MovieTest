
namespace Application.Services
{
    public class CsvFileHandler<TOut> : ICsvFile<TOut> where TOut : class, IDto
    {
        private readonly List<string> _headers;
        private readonly IEnumerable<string> _lines;

        private CsvFileHandler(IEnumerable<string> headers, IEnumerable<string> lines)
        {
            _headers = headers.ToList();
            _lines = lines;
        }

        /// <summary>
        /// Instantiate a new generic CsvFileHandler from a file path. 
        /// <typeparamref name="TOut">The specific implementation of IDto that you expect the CSV file to contain.</typeparamref>
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="InvalidDataException"></exception>
        public static async Task<ICsvFile<TOut>> LoadFromPath(string path)
        {
            var allLines = await File.ReadAllLinesAsync(path);

            if (!allLines.Any())
            {
                throw new InvalidOperationException("Cannot import empty CSV file.");
            }

            var headers = allLines.First().ToLower().Split(',').ToList();
            var propNames = typeof(TOut).GetProperties().Select(p => p.Name);
            var validation = ValidateHeaders<TOut>(headers, propNames);

            if (validation.Failed())
            {
                throw new InvalidDataException(validation.GetErrorMessage());
            }

            return new CsvFileHandler<TOut>(headers, allLines.Skip(1));
        }

        private static InputValidation ValidateHeaders<T>(IEnumerable<string> headers, IEnumerable<string> propNames)
        {
            var missingHeaders = propNames.Except(headers).ToList();
            var inputValidation = new InputValidation();

            if (missingHeaders.Any())
            {
                inputValidation.AddError(string.Format("The CSV file does not contain headers matching the following properties '{0}' required by the type '{1}'.", string.Join(", ", missingHeaders), typeof(T)));
            }

            return inputValidation;
        }

        /// <summary>
        /// Get all records of type TOut contained in the CSV file.
        /// </summary>
        /// <returns>An IEnumerable of the expected return type.</returns>
        public IEnumerable<TOut> GetDtos()
        {
            var dtos = new List<TOut>();
            
            foreach(var line in _lines)
            {
                var dto = BuildDto(line);

                if(dto is null)
                {
                    continue;
                }

                dtos.Add(dto);
            }

            return dtos;
        }

        private TOut? BuildDto(string line)
        {
            var values = line.Split(',');
            var valueDict = new Dictionary<string, object>();

            for(var i = 0; i <  values.Length; i++)
            {
                valueDict.Add(_headers[i], values[i]);
            }

            var createMethod = typeof(TOut).GetMethod("Create")?.MakeGenericMethod(typeof(TOut));
            var dto = createMethod?.Invoke(null, new object[] { valueDict }) as TOut;

            return dto;
        }  
    }
}
