using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace SportNugget.Pages.Shared
{
    public partial class TopMenu
    {
        [Inject]
        public SignOutSessionStateManager SignOutSessionStateManager { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private async Task OnLogOut(MouseEventArgs args)
        {
            await SignOutSessionStateManager.SetSignOutState();
            NavigationManager.NavigateTo("/Logout");
        }
    }
}
