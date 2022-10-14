using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Xml;
using Zygieldesk.Application.Exceptions;
using Formatting = Newtonsoft.Json.Formatting;
using ValidationException = FluentValidation.ValidationException;

namespace Zygieldesk.Application.Middlewares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (BadRequestException badRequestException)
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject((badRequestException.Message)));
            }
            catch (ValidationException validationException)
            {
                var errorTextList = new List<string>();
                foreach (var error in validationException.Errors)
                {
                    errorTextList.Add(error.ErrorMessage);
                }
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorTextList));
            }
            catch (ForbiddenException forbiddenException)
            {
                context.Response.StatusCode = 403;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(forbiddenException.Message));
            }
            catch (NotFoundException notFoundException)
            {
                context.Response.StatusCode = 404;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(notFoundException.Message));
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject("Something went wrong"));
            }
        }
    }
}