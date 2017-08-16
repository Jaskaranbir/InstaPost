using Api.DIServiceRegister;
using Api.Models.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api {
    public class Startup {
        public Startup(IHostingEnvironment env) {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            // Add framework services.
            services.AddMvc().AddJsonOptions(options => 
                options.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              .AllowCredentials());
            });

            IConfigurationSection serverData = Configuration.GetSection("ServerData");
            services.Configure<Auth0Config>(serverData.GetSection("Auth0"));
            services.Configure<CloudinaryConfig>(serverData.GetSection("Cloudinary"));

            SqlServerDb.Register(services, Configuration);
            MongoDb.Register(services, Configuration);
            
            MongoDbSets.Register(services);
            ModelRepositories.Register(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddConsole().AddDebug();

            // Add the cookie middleware
            var options = new JwtBearerOptions {
                Audience = Configuration["ServerData:Auth0:ApiIdentifier"],
                Authority = $"https://{Configuration["ServerData:Auth0:Domain"]}/",
                Events = new JwtBearerEvents {
                    OnTokenValidated = context => {
                        // Grab the raw value of the token, and store it as a claim so we can retrieve it again later in the request pipeline
                        // Have a look at the ValuesController.UserInformation() method to see how to retrieve it and use it to retrieve the
                        // user's information from the /userinfo endpoint
                        if (context.SecurityToken is JwtSecurityToken token) {
                            if (context.Ticket.Principal.Identity is ClaimsIdentity identity) {
                                identity.AddClaim(new Claim("access_token", token.RawData));
                                //identity.
                            }
                        }

                        return Task.FromResult(0);
                    }
                }
            };
            app.UseJwtBearerAuthentication(options);

            app.UseCors("CorsPolicy");
            app.UseMvc();
        }
    }
}
