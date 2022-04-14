using Microsoft.AspNetCore.Components;
using SportNugget.Shared.State.Demos.Interfaces;

namespace SportNugget.Pages.Shared
{
    public class PageBase : ComponentBase, IDisposable
    {
        [Inject]
        public ITestState TestState { get; set; }

        [CascadingParameter]
        public Error? Error { get; set; }

        protected override Task OnInitializedAsync()
        {
            TestState.OnChange += StateHasChanged;
            return base.OnInitializedAsync();
        }

        public void Dispose()
        {
            TestState.OnChange -= StateHasChanged;
        }
    }
}
