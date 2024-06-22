using PayrollSystem.Framework.SharedKernel.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Core.Exeptions
{
    [TranslationKey("RESOURCE_NOT_FOUND")]
    public class NotFoundException : DomainException
    {
        public NotFoundException(string message, params string[] parameters) : base(message, parameters)
        {
        }
    }
}
