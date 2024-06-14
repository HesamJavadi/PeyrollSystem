using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Utilities
{
    // ResponseGeneric class definition
    public class ResponseGeneric<T>
    {
        public bool success { get; private set; }
        public T data { get; private set; }
        public int? total { get; private set; }
        public string message { get; private set; }
        public List<string> errors { get; private set; }

        private ResponseGeneric(bool _success, T _data, string _message, List<string> _errors, int? _total)
        {
            success = _success;
            data = _data;
            message = _message;
            errors = _errors;
            total = _total;
        }

        public static ResponseGeneric<T> Success(T data, string message = "", int? total = null)
        {
            return new ResponseGeneric<T>(true, data, message, null, total);
        }

        public static ResponseGeneric<T> Success(string message = "")
        {
            return new ResponseGeneric<T>(true, default, message, null , null);
        }

        public static ResponseGeneric<T> Fail(List<string> errors)
        {
            return new ResponseGeneric<T>(false, default, "", errors, null);
        }

        public static ResponseGeneric<T> Fail(string error)
        {
            return new ResponseGeneric<T>(false, default, "", new List<string> { error }, null);
        }
    }


}
