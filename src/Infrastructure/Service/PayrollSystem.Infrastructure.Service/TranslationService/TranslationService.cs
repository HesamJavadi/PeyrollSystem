using PayrollSystem.Domain.Contracts.Translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Infrastructure.Service.TranslationService
{
    public class TranslationService : ITranslator
    {
        private readonly Dictionary<string, string> _translations = new Dictionary<string, string>
        {
            { "RESOURCE_NOT_FOUND", "The requested resource was not found." },
            { "SOME_KIND_OF_ERROR_OCCURRED_IN_THE_API", "Some kind of error occurred in the API." }
        };

        public string Translate(string key)
        {
            if (_translations.TryGetValue(key, out var translation))
            {
                return translation;
            }
            return key; 
        }
    }
}
