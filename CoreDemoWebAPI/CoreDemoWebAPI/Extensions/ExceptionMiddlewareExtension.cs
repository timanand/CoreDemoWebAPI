//using Contracts;
//using Entities.ErrorModel;
//Global Error Handling - BEGIN

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

using CoreDemoWebAPI.Domain.ErrorModel;
using CoreDemoWebAPI.Data;

namespace AdvancedWebAPIProject.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        //public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    // We are setting things up here
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    // This one line gets the exception
                    var contextException = context.Features.Get<IExceptionHandlerFeature>();

                    // We report on the exception, log it and send API response
                    if (contextException != null)
                    {
                        // This writes to log file
                        logger.LogError($"Something went wrong: {contextException.Error}");

                        // This is related to API response
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error"
                        }.ToString());
                    }
                });
            });
        }
    }
}

//Global Error Handling - END
