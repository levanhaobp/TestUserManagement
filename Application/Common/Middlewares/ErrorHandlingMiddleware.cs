using Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Application.Common.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.ToString();
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = 500;
            var message = "Đã xảy ra lỗi trong quá trình xử lý";
            switch (ex)
            {
                case UnauthorizedAccessException _:
                    code = (int)HttpStatusCode.Unauthorized;
                    message = ex.Message;
                    break;
                case HttpStatusException exception:
                    code = exception.StatusCode;
                    message = exception.Message;
                    break;
                case FluentValidation.ValidationException _:
                    code = (int)HttpStatusCode.BadRequest;
                    message = ex.Message;
                    break;
            }

            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = code;
            return context.Response.WriteAsync(message);
        }
    }
}
