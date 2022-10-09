using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using Zygieldesk.Application.Exceptions;
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
            catch (ForbiddenException forbiddenException)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(forbiddenException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch(ValidationException validationException)
            {
                var errorTextList = new List<string>();
                foreach (var error in validationException.Errors)
                {
                    errorTextList.Add(error.ErrorMessage);
                }                
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(string.Join("\n", errorTextList));
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}