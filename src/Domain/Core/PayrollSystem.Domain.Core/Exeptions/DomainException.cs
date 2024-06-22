using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Core.Exeptions
{
    public class DomainException : Exception
    {
        public string[] Parameters { get; set; }

        protected DomainException(string message, params string[] parameters) : base(message)
        {
            Parameters = parameters;
        }

        public override string ToString()
        {
            if (Parameters == null || Parameters.Length == 0)
            {
                return Message;
            }

            string result = Message;
            for (int i = 0; i < Parameters.Length; i++)
            {
                string placeholder = $"{{{i}}}";
                result = result.Replace(placeholder, Parameters[i]);
            }

            return result;
        }
    }
}
