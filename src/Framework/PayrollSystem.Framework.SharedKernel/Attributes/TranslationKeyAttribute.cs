using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Framework.SharedKernel.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class TranslationKeyAttribute : Attribute
    {
        public string Key { get; }

        public TranslationKeyAttribute(string key)
        {
            Key = key;
        }
    }
}
