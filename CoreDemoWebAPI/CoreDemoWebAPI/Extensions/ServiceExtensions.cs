using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Builder;
using CoreDemoWebAPI.Data;
using LoggerService;


namespace CoreDemoWebAPI.Extensions
{
    public static class ServiceExtensions
    {

        public static void ConfigureCors(this IServiceCollection services)
        {
            // Create Cors Policy
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", cfg => cfg.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            
        }


        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddScoped<ILoggerManager, LoggerManager>();
        }

        // Custom CSV Formatter - BEGIN
        public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutputFormatter()));
        }


        // Custom CSV Formatter - END



    }
}
