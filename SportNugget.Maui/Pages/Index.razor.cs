using Microsoft.AspNetCore.Components;

namespace SportNugget.Maui.Pages
{
    public partial class Index
    {
        #region Injected Services
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        #endregion

        #region Lifecycles
        protected override async Task OnInitializedAsync()
        {
            NavigationManager.NavigateTo("/Home");
        }
        #endregion
    }
}
