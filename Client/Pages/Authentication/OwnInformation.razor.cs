using Client.Models;
using Client.Pages;
using Client.Services;
using Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace Client.Pages.Authentication
{
    public partial class OwnInformation
    {
        [Inject] ILogger<Empty> Logger { get; set; }
        [Inject] IStringLocalizer<Index> IL { get; set; }
        [Inject] IStringLocalizer<OwnInformation> L { get; set; }
        [Inject] StateContainer StateContainer { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] HttpClient HttpClient { get; set; }
        [Inject] AppSettings AppSettings { get; set; }
        [Inject] ApiService ApiService { get; set; }

        [CascadingParameter]
        public MainLayout Mainlayout { get; set; }
        protected override void OnInitialized()
        {
            if (StateContainer.IsAuthenticated == false)
            {
                StateContainer.PushPage("/Authentication/OwnInformation");
                NavigationManager.NavigateTo(StateContainer.PushPage("/Authentication/Login"));
            }
        }
    }
}