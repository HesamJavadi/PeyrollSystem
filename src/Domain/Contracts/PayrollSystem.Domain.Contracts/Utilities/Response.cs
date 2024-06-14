using System.Collections.Generic;

namespace PayrollSystem.Domain.Contracts.Utilities
{
    public class Response
    {
        public bool success { get; private set; }
        public string? message { get; private set; }
        public List<string>? errors { get; private set; }

        private Response(bool _success, string? _message, List<string>? _errors)
        {
            success = _success;
            message = _message;
            errors = _errors;
        }

        public static Response Success(string message = "")
        {
            return new Response(true, message, null);
        }

        public static Response Fail(List<string> errors)
        {
            return new Response(false, "", errors);
        }

        public static Response Fail(string error)
        {
            return new Response(false, "", new List<string> { error });
        }
    }
}
