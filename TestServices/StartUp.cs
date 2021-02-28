using Localize;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Services;
using Services.Datastore;

namespace TestServices
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            DBSettings dbSettings = new DBSettings()
            {
                ConnectionString="Data Source=.\\SQLEXPRESS;database=dataapi;trusted_connection=true;"
            };

            services.AddLocalization();
            services.AddSingleton(dbSettings);
        }
    }
}
