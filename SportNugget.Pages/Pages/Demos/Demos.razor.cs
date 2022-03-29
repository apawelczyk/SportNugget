using Microsoft.AspNetCore.Components;
using SportNugget.Shared.Services.Interfaces;
using SportNugget.Shared.ViewModelBuilders.Interfaces;
using SportNugget.ViewModels.Demos;

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
        public List<TestViewModel> TestData { get; set; } = new List<TestViewModel>();
        #endregion

        #region Lifecycles
        protected override async Task OnInitializedAsync()
        {
            Test = "Test Demo";

            var testData = await TestService.GetTests();
            var builtViewModels = TestViewModelBuilder.BuildMany(testData);
            TestData = builtViewModels;
        }
        #endregion
    }
}
