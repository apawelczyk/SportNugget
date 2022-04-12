using Microsoft.AspNetCore.Components;

namespace SportNugget.Components.Shared
{
    public partial class Loader
    {
        #region Injected Services
        #endregion

        #region Parameters
        [Parameter]
        public bool IsLoading { get; set; } = true;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public RenderFragment? LoaderTemplate { get; set; }
        #endregion

        #region Local Variables
        public bool isLoading { get; set; } = true;
        #endregion

        #region Lifecycles
        protected override Task OnParametersSetAsync()
        {
            try
            {
                isLoading = IsLoading;
            }
            catch (Exception e)
            {

            }
            return base.OnParametersSetAsync();
        }
        #endregion
    }
}
