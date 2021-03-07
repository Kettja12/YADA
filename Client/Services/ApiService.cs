using Client.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.Services
{
    public class ApiService
    {
        private AppSettings appsettings { get; set; }
        public ApiService(AppSettings appsettings)
        {
            this.appsettings = appsettings;
        }

        public async Task<Response> TestConnectionAsync()
        {
            return await new HttpClient().GetFromJsonAsync<Response>(appsettings.ApiUrl);
        }
        public async Task<Response> CallServiceAsync(ParametersIn parametersIn)
        {
            var response= await new HttpClient().PostAsJsonAsync(appsettings.ApiUrl, parametersIn);
            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var s=response.Content.ReadAsStringAsync().Result;
                var r = JsonSerializer.Deserialize<Response>(s,options);
                return r;
            }
            return new Response("FAIL", "Invalid call");
        }
    }
}
