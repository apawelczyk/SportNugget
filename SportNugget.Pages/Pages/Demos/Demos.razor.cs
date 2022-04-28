using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using SportNugget.Logging.Interfaces;
using SportNugget.Pages.Shared;
using SportNugget.Shared.Services.Interfaces;
using SportNugget.Shared.State.Demos.Interfaces;
using SportNugget.Shared.ViewModelBuilders.Interfaces;
using SportNugget.ViewModels.Demos;

namespace SportNugget.Pages.Pages.Demos
{
    public partial class Demos : PageBase
    {
        #region Injected Services
        [Inject]
        private ITestService TestService { get; set; }
        [Inject]
        private ITestViewModelBuilder TestViewModelBuilder { get; set; }
        [Inject]
        public ILogger Logger { get; set; }
        [Inject]
        public ITestState TestState { get; set; }
        [Inject]
        public IAccessTokenProvider TokenProvider { get; set; }
        #endregion

        #region Parameters
        #endregion

        #region Local Variables
        public string TestText { get; set; }
        public List<TestViewModel> TestData { get; set; } = new List<TestViewModel>();
        public bool IsLoading { get; set; } = true;
        #endregion

        #region Lifecycles
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var accessTokenResult = await TokenProvider.RequestAccessToken();
                var bearerToken = string.Empty;

                if (accessTokenResult.TryGetToken(out var token))
                {
                    bearerToken = token.Value;
                }

                Logger.LogInfo("Demos.razor.cs OnInitializedAsync!" + $" Token: {bearerToken}");
                TestText = TestState.TestText;

                var testDataTask = LoadTestData();

                await Task.WhenAll(testDataTask);

                IsLoading = false;
                StateHasChanged();
                throw new Exception();
            }
            catch(Exception e)
            {
                Error?.ProcessError(e);
            }
        }
        #endregion

        #region Tasks
        private async Task LoadTestData()
        {
            var testData = await TestService.GetTests();
            var builtViewModels = TestViewModelBuilder.BuildMany(testData);
            TestData = builtViewModels;
        }
        #endregion

        #region Events
        public void OnTestStateChangeButtonClick()
        {
            TestState.TestText = TestText;
            StateHasChanged();
        }
        #endregion
    }
}
