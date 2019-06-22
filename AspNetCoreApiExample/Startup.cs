using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreApiExample.Models;
using AspNetCoreApiExample.Business;
using AspNetCoreApiExample.Business.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AspNetCoreApiExample.Repository;
using AspNetCoreApiExample.Repository.Implementations;
using AspNetCoreApiExample.Repository.Generic;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Tapioca.HATEOAS;
using AspNetCoreApiExample.HyperMedia;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Rewrite;
using AspNetCoreApiExample.Security.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreApiExample
{
    public class Startup
    {
        private readonly ILogger _logger;
        public IHostingEnvironment hostingEnvironment { get; }
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration, IHostingEnvironment enviroment, ILogger<Startup> logger)
        {
            Configuration = configuration;
            hostingEnvironment = enviroment;
            _logger = logger;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(
              options =>
              {
                  options.RespectBrowserAcceptHeader = true;
              }
            ).AddXmlSerializerFormatters()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var connection = Configuration.GetConnectionString("MySqlConnectionString");
            services.AddDbContext<MySqlContext>(options => options.UseMySql(connection));

            services.AddApiVersioning();

            AddAuthetication(services);

            //Dependency injection
            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ObjectContentResponseEnricherList.Add(new PersonEnricher());
            filterOptions.ObjectContentResponseEnricherList.Add(new BookEnricher());
            services.AddSingleton(filterOptions);

            services.AddScoped<IPersonBusiness, PersonBusiness>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IBookBusiness, BookBusiness>();
            services.AddScoped<IUserBusiness, UserBusiness>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFileBusiness, FileBusiness>();


            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));


            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new Info { Title = "RESTful API With ASP.NET Core 2.0", Version = "v1"});
            });

            //if (hostingEnvironment.IsDevelopment())
            //{
            //    try
            //    {
            //        var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connection);

            //        var evolve = new Evolve.Evolve(evolveConnection, msg => _logger.LogInformation(msg))
            //        {
            //            Locations = new List<string> { "DB/migrations" },
            //            IsEraseDisabled = false
            //        };
            //        evolve.Migrate();
            //    }
            //    catch (Exception e)
            //    {
            //        _logger.LogCritical("Database migrations failed", e);
            //        throw;
            //    }
            //}
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c=> {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","My Api v1");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void AddAuthetication(IServiceCollection services)
        {
            var signingConfigurations = new SignInConfiguration();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                Configuration.GetSection("TokenConfigurations")
            )
            .Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);


            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Validates the signing of a received token
                paramsValidation.ValidateIssuerSigningKey = true;

                // Checks if a received token is still valid
                paramsValidation.ValidateLifetime = true;

                // Tolerance time for the expiration of a token (used in case
                // of time synchronization problems between different
                // computers involved in the communication process)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Enables the use of the token as a means of
            // authorizing access to this project's resources
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });
        }
    }
}
