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
        public Task<Response> Get() => Task.FromResult(new Response(ResponseStatus.OK.ToString(), localizer["ServicesInitResponse"]));

        public async Task<Response> Post(ParametersIn request)
        {
            try
            {
                MethodInfo mInfo = typeof(ServiceFactory).GetMethod(request.Service);
                if (mInfo != null)
                {
                    var sf = new ServiceFactory(logger, dbSettings, localizer);
                    var result = (Task<Response>)mInfo.Invoke(sf, new object[] { request });
                    return await result;
                }
                else
                {
                    if (request.Service == "Live")
                        return await Task.FromResult(
                            new Response(ResponseStatus.OK.ToString(),localizer["ServicesLiveResponse"]));
                }
                return await Task.FromResult(new Response(ResponseStatus.FAIL.ToString(),localizer[ "ServicesInvalidCall"]));
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return await Task.FromResult(new Response(ResponseStatus.FAIL.ToString(), localizer["ServicesInvalidCall"]));
            }
        }

    }
}
