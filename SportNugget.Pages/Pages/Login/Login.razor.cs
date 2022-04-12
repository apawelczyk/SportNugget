using Microsoft.AspNetCore.Components;
using SportNugget.Logging.Interfaces;
using SportNugget.Pages.Shared;

namespace SportNugget.Pages.Pages.Login
{
    public partial class Login : PageBase
    {
        #region Injected Services
        [Inject]
        public ILogger Logger { get; set; }
        #endregion

        #region Parameters
        #endregion

        #region Local Variables
        #endregion

        #region Lifecycles
        protected override async Task OnInitializedAsync()
        {
            try
            {
                Logger.LogInfo("Login.razor.cs OnInitializedAsync!");
            }
            catch (Exception e)
            {
                Error?.ProcessError(e);
            }
        }
        #endregion

        #region Tasks
        #endregion
    }
}
