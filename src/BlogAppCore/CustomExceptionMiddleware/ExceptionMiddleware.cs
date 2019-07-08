using System;
using System.Net;
using System.Threading.Tasks;
using BlogAppCore.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BlogAppCore.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;

            if (ex is NotFoundException) code = HttpStatusCode.NotFound;
            else if (ex is ValidationException) code = HttpStatusCode.BadRequest;

            var result = JsonConvert.SerializeObject(new
            {
                code,
                error = ex.Message
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}