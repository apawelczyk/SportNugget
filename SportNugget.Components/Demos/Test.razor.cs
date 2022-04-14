using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;

namespace SportNugget.Components.Demos
{
    public partial class Test<T>
    {
        #region Injected Services
        [Inject]
        private IJSRuntime JS { get; set; }
        #endregion

        #region Parameters
        [Parameter]
        public RenderFragment? TableHeader { get; set; }
        [Parameter]
        public RenderFragment<T>? RowTemplate { get; set; }
        [Parameter]
        public RenderFragment? FooterContent { get; set; }
        [Parameter, AllowNull]
        public IReadOnlyList<T> Items { get; set; }
        #endregion

        protected override async Task OnInitializedAsync()
        {
            var result = await JS.InvokeAsync<string>("testTest", "testing");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

        }
    }
}
