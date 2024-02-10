using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class InputValidation
    {
        private List<string> Errors { get; set; } = new();
        public bool Failed() => Errors.Any();
        public void AddError(string message) => Errors.Add(message);
        public string GetErrorMessage()
        {
            return string.Format("Input validation failed: {0}", string.Join(", ", Errors)); 
        }
    }
}
