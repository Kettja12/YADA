using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Datastore;

namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            DBSettings dbSettings = new DBSettings();
            Configuration.GetSection("DBSettings").Bind(dbSettings);
            services.AddSingleton<DBSettings>(dbSettings);
            services.AddSingleton<RootService>();
            services.AddLocalization();
            services.AddCors(o => o.AddPolicy("policy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("policy");
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapPost("/services", async context =>
                {
                    try
                    {
                        var s = await context.Request.ReadFromJsonAsync<ParametersIn>();
                        var services = endpoints
                            .ServiceProvider
                            .GetService<RootService>();
                        var r = await services.Post(s);
                        await context.Response.WriteAsJsonAsync(r);
                    }
                    catch
                    {
                        context.Response.StatusCode = 401;
                    }

                });

                endpoints.MapGet("/services", async context =>
                {
                    var services = endpoints
                        .ServiceProvider
                        .GetService<RootService>();
                    await context.Response
                        .WriteAsJsonAsync(await services.Get());
                });
            });
        }
    }
}
