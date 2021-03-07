using Client.Models;
using Client.Services;
using Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Pages
{
    public partial class Index
    {
        [Inject] ILogger<Index> Logger { get; set; }
        [Inject] IStringLocalizer<Index> L { get; set; }
        [Inject] StateContainer StateContainer { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] HttpClient HttpClient { get; set; }
        [Inject] AppSettings AppSettings { get; set; }
        [Inject] ApiService ApiService { get; set; }

        [CascadingParameter]
        public MainLayout Mainlayout { get; set; }
        protected override void OnInitialized()
        {
            if (StateContainer.IsAuthenticated==false)
            {
                StateContainer.PushPage("/");
                NavigationManager.NavigateTo(StateContainer.PushPage("/Authentication/Login"));
            }
        }
    }
}
