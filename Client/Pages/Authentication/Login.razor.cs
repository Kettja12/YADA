using Client.Services;
using Client.Shared;
using Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.Pages.Authentication
{
    public partial class Login
    {
        [Inject] ILogger<Login> Logger { get; set; }
        [Inject] IStringLocalizer<Login> L { get; set; }
        [Inject] StateContainer StateContainer { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] ApiService ApiService { get; set; }
        
        [CascadingParameter]
        public MainLayout Mainlayout { get; set; }

        private readonly User User = new();
        protected override void OnInitialized()
        {
            StateContainer.Token = string.Empty;
            StateContainer.IsAuthenticated = false;
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                try
                {
                    var response = await ApiService.TestConnectionAsync();
                    Logger.LogInformation(response.Status + " " + response.Data);

                    var parameters = new ParametersIn("", "Live", "");
                    response = await ApiService.CallServiceAsync(parameters);
                    Logger.LogInformation(response.Status + " " + response.Data);

                }
                catch (Exception e)
                {
                    Logger.LogError(e.Message);
                    Mainlayout.InfoMessage.Show("alert-primary", "Palvelinyhteys virhe");
                }
            }
        }
        private async Task HandleLoginAsync()
        {
            try
            {
                var token = await GetLoginAsync();
                if (token == string.Empty) return;
                var user = await GetUserAsync(token);
                if (user == null) return;
                var claims = await GetClaims(token);
                if (claims == null) return;

                StateContainer.Token = token;
                StateContainer.User = user;
                StateContainer.Claims = claims;
                StateContainer.IsAuthenticated = true;
                foreach (var item in claims)
                {
                    if (item.ClaimType == "Admin" && item.ClaimValue == "True")
                    {
                        StateContainer.IsAdmin = true;
                    }
                }
                NavigationManager.NavigateTo(StateContainer.PopPage());
            }
            catch (Exception e)
            {
                Mainlayout.InfoMessage.Show("alert-primary", e.Message);
            }
        }
        public async Task<string> GetLoginAsync()
        {
            try
            {
                var c = CultureInfo.CurrentCulture;
                var data = JsonSerializer.Serialize(User);
                var paramsin = new ParametersIn("", "Authentication.Login", data);
                var response = await ApiService.CallServiceAsync(paramsin);
                if (response.Status == "FAIL")
                {
                    Mainlayout.InfoMessage.Show("alert-primary", response.Data);
                    return string.Empty;
                }
                Guid newGuid = Guid.Parse(response.Data);
                return response.Data;
            }
            catch (Exception e)
            {
                Mainlayout.InfoMessage.Show("alert-primary", e.Message);
            }
            return string.Empty;
        }
        public async Task<User> GetUserAsync(string token)
        {
            var paramsin = new ParametersIn(token, "Authentication.GetUser", "");
            var response = await ApiService.CallServiceAsync(paramsin);
            if (response.Status == "FAIL")
            {
                Mainlayout.InfoMessage.Show("alert-primary", response.Data);
                return null;
            }

            if (response.Status == "FAIL")
            {
                Mainlayout.InfoMessage.Show("alert-primary", response.Data);
                return null;
            }
            return JsonSerializer.Deserialize<User>(response.Data);

        }
        public async Task<List<Claim>> GetClaims(string token)
        {
            var paramsin = new ParametersIn(token, "Authentication.GetClaims", "");
            var response = await ApiService.CallServiceAsync(paramsin);
            if (response.Status == "FAIL")
            {
                Mainlayout.InfoMessage.Show("alert-primary", response.Data);
                return null;
            }
            return JsonSerializer.Deserialize<List<Claim>>(response.Data);
        }
    }
}
