using System.Text;
using Advertisement.Infrastructure;
using Advertisement.Infrastructure.DataAccess;
using Advertisement.PublicApi.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Advertisement.PublicApi
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
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            
            services
                .AddApplicationModule()
                .AddHttpContextAccessor()
                .AddInfrastructureModule(configuration => configuration.IdentityFromHttpContext())
                
                
                .AddDataAccessModule(configuration =>
                        
                    //configuration.InMemory()
                    //configuration.InSqlServer(Configuration.GetConnectionString("SqlServerDb"))
                    configuration.InPostgress(Configuration.GetConnectionString("PostgresDb"))
                );

         
            services.AddHttpContextAccessor();

            services.AddSwaggerModule();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateActor = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"]))
                    };
                });

            services.AddApplicationException(config => { config.DefaultErrorStatusCode = 500; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Init migrations
             using var scope = app.ApplicationServices.CreateScope();
             var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
             db.Database.Migrate();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PublicApi v1"));
            app.UseApplicationException();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}