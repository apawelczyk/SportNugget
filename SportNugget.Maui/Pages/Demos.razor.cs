namespace SportNugget.Maui.Pages
{
    public partial class Demos
    {
        public string Test { get; set; } = "Test";

        protected override async Task OnInitializedAsync()
        {
            Test = "Test Maui";
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
        }
    }
}
