using Datastore;
using Entities;
using Localize;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Services.Datastore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Services
{
    public record ServiceParameters(
        ILogger<RootService> Logger,
        DBSettings DBSettings,
        IStringLocalizer<Resource> Localizer);
    public abstract class BaseService
    {
        protected readonly ServiceParameters di;
        protected JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public BaseService(
            ServiceParameters di)
        {
            this.di = di;
        }

        public async Task<Response> InvalidAccess()
        {
            return await Task.FromResult(new Response(
                    ResponseStatus.FAIL.ToString(),
                    di.Localizer["Invalid access"]));
        }

        public async Task<Response> Fail(string s)
        {
            return await Task.FromResult(new Response(
                    ResponseStatus.FAIL.ToString(),
                    di.Localizer[s]));
        }
        public static async Task<Response> OK(string s)
        {
            return await Task.FromResult(new Response(
                    ResponseStatus.OK.ToString(),
                    s));

        }

    }
}
