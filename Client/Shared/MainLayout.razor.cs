
using Client.Services;
using Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Security;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.Shared
{
    public partial class MainLayout
    {
        [Inject] ILogger<MainLayout> logger { get; set; }
        //[Inject] YADA.YADAClient YADAClient { get; set; }
        [Inject] StateContainer StateContainer { get; set; }
        [Inject] IJSRuntime JSRuntime { get; set; }
        RenderFragment ChildContent { get; set; }
        public InfoMessage InfoMessage { get; set; }

        protected string name;
        protected override void OnInitialized()
            {
            }
        /*        protected override async Task OnAfterRenderAsync(bool firstRender)
            {
                if (firstRender)
                {
                    try
                    {
                        var r = await TestConnectionAsync();
                        if (r == false) return;
                        await TestOldAuthentication();
                        logger.LogError(StateContainer.IsAuthenticated.ToString());

                    }
                    catch (Exception e)
                    {
                        logger.LogError(e.Message);
                        var c = CultureInfo.CurrentCulture;
                        StateContainer.Culture = c.Name;
                    }
                    await LoadControlAsync();
                }
            }
            private async Task TestOldAuthentication()
            {
                var token = await JSRuntime.InvokeAsync<string>("Token.get");
                if (string.IsNullOrEmpty(token) == false)
                {
                    try
                    {
                        var userresponse = await YADAClient.GetAsync(
                            new ParametersIn()
                            {
                                Service = (int)ServiceCode.GetUser,
                                Token = token,
                            }
                        );
                        if (userresponse.Status == "OK")
                        {
                            var user = JsonSerializer.Deserialize<Entities.User>(userresponse.Data);
                            StateContainer.IsAuthenticated = true;
                            StateContainer.Username = user.Username;
                            StateContainer.Token = token;
                            StateContainer.Culture = user.Culture;
                            StateContainer.Displayname = user.FirstName + " " + user.LastName;
                            var claimsresponse = await YADAClient.GetAsync(
                                new ParametersIn()
                                {
                                    Service = (int)ServiceCode.GetClaims,
                                    Token = token,

                                }
                            );
                            var claims = JsonSerializer.Deserialize<List<Claim>>(claimsresponse.Data);
                            foreach (var item in claims)
                            {
                                if (item.ClaimType == "Admin" && item.ClaimValue == "True")
                                {
                                    StateContainer.IsAdmin = true;
                                }
                            }
                        }
                        else
                        {
                            await JSRuntime.InvokeAsync<string>("Token.set", "");
                        }
                    }
                    catch (Exception e)
                    {
                        logger.LogError(e.Message);
                        InfoMessage.Show(InfoMessageTypes.Warning, "Login expired");
                        await JSRuntime.InvokeAsync<string>("Token.set", "");
                    }
                }
            }

            private async Task<bool> TestConnectionAsync()
            {
                try
                {
                    var response = await YADAClient.GetAsync(
                      new ParametersIn()
                      {
                          Service = (int)ServiceCode.Live

                      }
                  );
                    logger.LogInformation(response.Status + " " + response.Data);
                    return true;
                }
                catch (Exception e)
                {
                    logger.LogError(e.Message);
                    InfoMessage.Show(InfoMessageTypes.Warning, "Could not establish connection to data server");
                    return false;
                }
            }
            public async Task BackControlAsync()
            {
                StateContainer.ControlStack.Pop();
                await LoadControlAsync();
            }

            public async Task LoadControlAsync(ActiveControl c)
            {
                if (c == ActiveControl.login)
                {
                    StateContainer.IsAuthenticated = false;
                }
                else
                {
                    StateContainer.ControlStack.Push(c);
                }
                await LoadControlAsync();
            }

            public async Task LoadControlAsync()
            {

                if (StateContainer.IsAuthenticated == false)
                {
                    if (StateContainer.ControlStack.Peek() != ActiveControl.login)
                        StateContainer.ControlStack.Push(ActiveControl.login);
                    await JSRuntime.InvokeAsync<string>("Token.set", "");

                }
                ChildContent = (builder) =>
                {
                    switch (StateContainer.ControlStack.Peek())
                    {
                        case ActiveControl.login: builder.OpenComponent<Login>(0);break;
                        case ActiveControl.Index: builder.OpenComponent<Index>(0); break;
                        case ActiveControl.Customers: builder.OpenComponent<Customers>(0); break;
                        case ActiveControl.Eyeglasses: builder.OpenComponent<Components.Customers.Eyeglasses>(0); break;
                        case ActiveControl.ContactLences: builder.OpenComponent<Contactlences>(0); break;
                        case ActiveControl.Eyedoctor: builder.OpenComponent<Eyedoctor>(0); break;
                        case ActiveControl.User: builder.OpenComponent<Components.Authentication.User>(0); break;
                    };
                     builder.CloseComponent();
                };
                StateHasChanged();
            }
        */
    }
}
