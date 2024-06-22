using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Translator
{
    public interface ITranslator
    {
        string Translate(string key);
    }
}
