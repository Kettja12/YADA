using Xunit;
using Microsoft.Extensions.Logging;
using Services;
using Services.Datastore;
using Microsoft.Extensions.Localization;
using Localize;
using System.Globalization;
using System.Threading;
using Entities;

namespace TestServices
{
    public class TestServices
    {
        public readonly ILogger<RootService> logger;
        private readonly DBSettings dbSettings;
        private readonly IStringLocalizer<Resource> localizer;

        public TestServices(ILogger<RootService> logger,
            DBSettings dbSettings,
            IStringLocalizer<Resource> localizer)
        {
            this.logger = logger;
            this.dbSettings = dbSettings;
            this.localizer = localizer;
        }
        [Fact]
        public void TestRootService()
        {
            var services = new RootService(logger,dbSettings,localizer);
            Assert.NotNull(services);
            Assert.Equal(1, 1);
        }
        [Fact]
        public async System.Threading.Tasks.Task TestServiceGetAsync()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            var services = new RootService(logger,dbSettings,localizer);
            var response = await services.Get();
            Assert.Equal(ResponseStatus.OK.ToString(), response.Status);
            Assert.Equal(localizer["ServicesInitResponse"], response.Data);
    
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fi-FI");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("fi-FI");

            response = await services.Get();
            Assert.Equal(ResponseStatus.OK.ToString(), response.Status);
            Assert.Equal(localizer["ServicesInitResponse"], response.Data);

        }
        [Fact]
        public async System.Threading.Tasks.Task TestServiceLiveAsync()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            var services = new RootService(logger, dbSettings, localizer);
            var request = new ParametersIn("", "Live", "");
            var response = await services.Post(request);
            Assert.Equal(ResponseStatus.OK.ToString(), response.Status);
            Assert.Equal(localizer["ServicesLiveResponse"], response.Data);

            Thread.CurrentThread.CurrentCulture = new CultureInfo("fi-FI");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("fi-FI");

            response = await services.Post(request);
            Assert.Equal(ResponseStatus.OK.ToString(), response.Status);
            Assert.Equal(localizer["ServicesLiveResponse"], response.Data);

        }

    }
}
