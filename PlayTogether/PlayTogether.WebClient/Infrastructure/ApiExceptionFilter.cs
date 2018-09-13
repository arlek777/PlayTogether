using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PlayTogether.WebClient.Infrastructure
{
    public class ApiExceptionFilter: ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment _env;

        public ApiExceptionFilter(IHostingEnvironment env)
        {
            _env = env;
        }

        public override void OnException(ExceptionContext context)
        {
            object apiError;
            var exception = context.Exception;
            if (_env.IsDevelopment())
            {
                apiError = new
                {
                    message = exception.Message,
                    innerException = exception.InnerException?.Message,
                    stackTrace = exception.StackTrace
                };
            }
            else
            {
                apiError = new { message = "Server Error" };
            }

            context.HttpContext.Response.StatusCode = 500;
            context.Result = new JsonResult(apiError);

            base.OnException(context);
        }
    }
}
