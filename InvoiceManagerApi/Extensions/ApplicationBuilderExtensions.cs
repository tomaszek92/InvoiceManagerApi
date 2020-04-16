using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace InvoiceManagerApi.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseFluentValidationExceptionHandler(this IApplicationBuilder builder)
        {
            builder.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = errorFeature.Error;

                    if (!(exception is ValidationException validationException))
                    {
                        throw exception;
                    }

                    var errors = validationException.Errors.Select(error => new
                    {
                        error.PropertyName,
                        error.ErrorCode
                    });

                    var json = JsonSerializer.Serialize(errors);
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(json, Encoding.UTF8);
                });
            });
        }
    }
}
