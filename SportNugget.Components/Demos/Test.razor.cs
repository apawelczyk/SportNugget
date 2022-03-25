using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace SportNugget.Components.Demos
{
    public partial class Test
    {
        #region Injected Services
        [Inject]
        private IJSRuntime JS { get; set; }
        #endregion

        #region Parameters
        [Parameter]
        public List<string> TestData { get; set; }
        #endregion

        protected override async Task OnInitializedAsync()
        {

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var result = await JS.InvokeAsync<string>("testTest", "testing");
        }
    }
}
