﻿using Microsoft.AspNetCore.Components;
using SportNugget.Logging.Interfaces;
using SportNugget.Pages.Shared;
using SportNugget.Shared.Services.Interfaces;
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
        #endregion

        #region Parameters
        #endregion

        #region Local Variables
        public string Test { get; set; } = "Test";
        public List<TestViewModel> TestData { get; set; } = new List<TestViewModel>();
        public bool IsLoading { get; set; } = true;
        #endregion

        #region Lifecycles
        protected override async Task OnInitializedAsync()
        {
            try
            {
                Logger.LogInfo("Demos.razor.cs OnInitializedAsync!");
                Test = "Test Demo";

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
    }
}
