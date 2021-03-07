
using Client.Models;
using Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Cache;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        public static WebAssemblyHostConfiguration Configuration { get; set; }

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Logging.SetMinimumLevel(LogLevel.Debug);
            Configuration = builder.Configuration;
            builder.RootComponents.Add<App>("#app");
            ConfigureServices(builder.Services);
            var host = builder.Build();
            //var jsInterop = host.Services.GetRequiredService<IJSRuntime>();
            //var result = await jsInterop.InvokeAsync<string>("Culture.get");
            //if (result != null)
            //{
            //    var culture = new CultureInfo(result);
            //    CultureInfo.DefaultThreadCurrentCulture = culture;
            //    CultureInfo.DefaultThreadCurrentUICulture = culture;
            //}
            await host.RunAsync();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();
            services.AddSingleton<AppSettings>(appSettings);
            var apiUrl = new Uri(appSettings.ApiUrl);
            services.AddTransient(c => new HttpClient{ BaseAddress = apiUrl});
            services.AddTransient(c => new ApiService(appSettings));
            services.AddLocalization();


            //services.Configure<RequestLocalizationOptions>(
            //   options =>
            //   {
            //       List<CultureInfo> supportedCultures =
            //           new List<CultureInfo>
            //       {
            //            new CultureInfo("fi-FI"),
            //            new CultureInfo("en-US"),
            //            new CultureInfo("en-GB")
            //       };
            //       options.DefaultRequestCulture = new RequestCulture("fi-FI");
            //       options.SupportedCultures = supportedCultures;
            //       options.SupportedUICultures = supportedCultures;

            //   });
            services.AddSingleton<StateContainer>();

        }


    }
}