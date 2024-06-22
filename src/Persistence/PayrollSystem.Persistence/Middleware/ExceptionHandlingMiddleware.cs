using Newtonsoft.Json;
using PayrollSystem.Domain.Contracts.Translator;
using PayrollSystem.Framework.SharedKernel.Attributes;
using System.Net;
using System.Text.Json;

namespace PayrollSystem.Persistence.Middleware
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionMiddleware> _logger;
        private readonly ITranslator _translator;

        public ApiExceptionMiddleware( RequestDelegate next,
            ILogger<ApiExceptionMiddleware> logger,ITranslator translator)
        {
            _next = next;
            _logger = logger;
            _translator = translator;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var error = new ApiError
            {
                Id = Guid.NewGuid().ToString(),
                Status = (short)GetStatusCode(exception),
                Title = GetTranslatedErrorMessage(exception)
            };

            //_options.AddResponseDetails?.Invoke(context, exception, error);

            var innerExMessage = GetInnermostExceptionMessage(exception);

            //var level = _options.DetermineLogLevel?.Invoke(exception) ?? LogLevel.Error;
            _logger.Log(LogLevel.Error, exception, "BADNESS!!! " + innerExMessage + " -- {ErrorId}.", error.Id);


            var result = System.Text.Json.JsonSerializer.Serialize(error);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = error.Status;
            return context.Response.WriteAsync(result);
        }

        private int GetStatusCode(Exception exception)
        {
            return (int)HttpStatusCode.InternalServerError;
        }

        private string GetTranslatedErrorMessage(Exception exception)
        {
            var translationKeyAttribute = exception.GetType()
                .GetCustomAttributes(typeof(TranslationKeyAttribute), false)
                .FirstOrDefault() as TranslationKeyAttribute;

            if (translationKeyAttribute != null)
            {
                return _translator.Translate(translationKeyAttribute?.Key);
            }

            return "_";

        }

        private string GetInnermostExceptionMessage(Exception exception)
        {
            if (exception.InnerException != null)
                return GetInnermostExceptionMessage(exception.InnerException);

            return exception.Message;
        }
    }
}
