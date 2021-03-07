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
    public class ServiceFactory
    {
        private readonly ServiceParameters di;

        public ServiceFactory(
            ServiceParameters di)
        {
            this.di = di;
        }

        public  async Task<Response> Run(ParametersIn request)
        {
            try
            {
                var serviceParts = request.Service.Split('.');
                var serviceclass = Type.GetType("Services."+serviceParts[0]);
                if (serviceclass != null)
                {
                    MethodInfo mInfo = serviceclass.GetMethod(serviceParts[1]);
                    if (mInfo != null)
                    {
                        var sf = Activator.CreateInstance(serviceclass, di); 
                        var result = (Task<Response>)mInfo.Invoke(sf, new object[] { request });
                        return await result;
                    }
                }
                if (request.Service == "Live")
                    return await Task.FromResult(
                    new Response(ResponseStatus.OK.ToString(), di.Localizer["I'm alive"]));
                return await Task.FromResult(new Response(ResponseStatus.FAIL.ToString(), di.Localizer["Invalid service call"]));
            }
            catch (Exception e)
            {
                di.Logger.LogError(e.Message);
                return await Task.FromResult(new Response(ResponseStatus.FAIL.ToString(), di.Localizer["ServicesInvalidCall"]));

            }
        }
    }

}
