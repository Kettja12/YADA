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

namespace Services
{
    public partial class ServiceFactory
    {
        public readonly ILogger<RootService> logger;
        public readonly DBSettings dbSettings;
        private readonly IStringLocalizer<Resource> localizer;
        //public Task<Response> response;
        private JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public ServiceFactory(
            ILogger<RootService> logger,
            DBSettings dbSettings,
            IStringLocalizer<Resource> localizer)
        {
            this.logger = logger;
            this.dbSettings = dbSettings;
            this.localizer = localizer;
        }
    }

}
