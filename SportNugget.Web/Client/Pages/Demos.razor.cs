namespace SportNugget.Web.Client.Pages
{
    public partial class Demos
    {
        public string Test { get; set; } = "Test";
        public List<string> TestData { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Test = "Test Demo";
            TestData = new List<string> { "Test Web 1", "Test Web 2" };
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
        }
    }
}
