using System.Collections.Generic;

namespace PayrollSystem.Domain.Contracts.Utilities
{
    public class ServiceResponse
    {
        public bool success { get; private set; }
        public string? message { get; private set; }
        public List<string>? errors { get; private set; }

        private ServiceResponse(bool _success, string? _message, List<string>? _errors)
        {
            success = _success;
            message = _message;
            errors = _errors;
        }

        public static ServiceResponse Success(string message = "")
        {
            return new ServiceResponse(true, message , null);
        }

        public static ServiceResponse Fail(List<string> errors)
        {
            return new ServiceResponse(false, null , errors);
        }

        public static ServiceResponse Fail(string error)
        {
            return new ServiceResponse(false, null, new List<string> { error });
        }
    }
}
