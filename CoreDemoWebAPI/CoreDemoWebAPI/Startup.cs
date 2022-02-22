using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CoreDemoWebAPI.Data;

// AANA - BEGIN
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using CoreDemoWebAPI.Extensions;
// AANA - END


namespace CoreDemoWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.ConfigureCors();
            services.AddControllersWithViews();
            services.AddControllers().AddNewtonsoftJson(options =>
                {
                    options.UseMemberCasing();
                } // note : default is camel casing 
            ); // with this we can use WebAPI (http get, post, put calls)


            // 21/02/2022 - Swagger BEGIN
            //services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "dotnetClaimAuthorization", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                       new OpenApiSecurityScheme{
                        Reference=new OpenApiReference{
                            Type = ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                       },
                       new string[]{}
                   }
               });
            });


            // 21/02/2022 - Swagger END


            // AANA - BEGIN
            var key = "This is my test key"; // In Real Life application, this will be more complex !

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });






            // 09/02/2022 - BEGIN

            var builder = new ConfigurationBuilder().SetBasePath(System.IO.Directory.GetCurrentDirectory()).AddUserSecrets("155b0bd7-6992-4ed6-8b80-edafb814d098");
            var config = builder.Build();

            string connectionString = config["ConnectionStrings:StaffConnex"];

            //services.AddTransient<IStaffRepository>(p => new StaffRepository(connectionString));
            services.AddTransient<IProvider>(p => new Provider(connectionString));


            // 09/02/2022 - END



            //20/10/2021 Ioc mapping for unit of work
            services.AddScoped<IUow, Uow>();
            //services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(Configuration, key));
            services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(Configuration, connectionString, key));
            // AANA - END



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // 18/02/2022 - Swagger BEGIN
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoreDemoWebAPI V1");
            });

            // 18/02/2022 - Swagger END


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseRouting();

            app.UseAuthentication(); // Added by AANA
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
