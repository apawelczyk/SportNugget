using Microsoft.AspNetCore.Components;
using SportNugget.Shared.Services.Interfaces;
using SportNugget.Shared.ViewModelBuilders.Interfaces;

namespace SportNugget.Pages.Pages.Demos
{
    public partial class Demos
    {
        #region Injected Services
        [Inject]
        private ITestService TestService { get; set; }
        [Inject]
        private ITestViewModelBuilder TestViewModelBuilder { get; set; }
        #endregion

        #region Local Variables
        public string Test { get; set; } = "Test";
        public List<string> TestData { get; set; } = new List<string>();
        #endregion

        #region Lifecycles
        protected override async Task OnInitializedAsync()
        {
            Test = "Test Demo";
            TestData = new List<string> { "Test Web 1", "Test Web 2" };

            var testData = await TestService.GetTests();
            var builtViewModels = TestViewModelBuilder.BuildMany(testData);
            TestData = builtViewModels?.Select(x => x.Name)?.ToList() ?? new List<string>();
        }
        #endregion
    }
}
