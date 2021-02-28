using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;
using Services.Datastore;
using System.Collections.Generic;
using System.Globalization;

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
            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("fi-FI"),
                        new CultureInfo("en-US"),
                        new CultureInfo("en-GB")
                    };

                    options.DefaultRequestCulture = new RequestCulture(culture: "fi-FI");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
