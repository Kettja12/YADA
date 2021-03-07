using Entities;
using Localize;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Services.Datastore;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Services
{
    public class RootService
    {
        private readonly ILogger<RootService> logger;
        private readonly DBSettings dbSettings;
        private readonly IStringLocalizer<Resource> localizer;

        public RootService(
               ILogger<RootService> logger,
               DBSettings dbSettings,
               IStringLocalizer<Resource> localizer)
        {
            this.logger = logger;
            this.dbSettings = dbSettings;
            this.localizer = localizer;
        }
        public Task<Response> Get() => Task.FromResult(new Response(ResponseStatus.OK.ToString(), localizer["Services here. How I can help You"]));

        public async Task<Response> Post(ParametersIn request)
        {
            try
            {
                var di = new ServiceParameters(logger, dbSettings, localizer);
                var sf = new ServiceFactory(di);
                var result = sf.Run(request);
                return await result;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return await Task.FromResult(new Response(ResponseStatus.FAIL.ToString(), localizer["Invalid service call"]));
            }
        }

    }
}
