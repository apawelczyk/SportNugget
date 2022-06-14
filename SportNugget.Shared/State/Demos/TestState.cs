using Blazored.LocalStorage;
using SportNugget.Logging.Interfaces;
using SportNugget.Shared.Services.Interfaces;
using SportNugget.Shared.State.Base;
using SportNugget.Shared.State.Demos.Interfaces;

namespace SportNugget.Shared.State.Demos
{
    public class TestState : BaseState, ITestState
    {
        private readonly ISyncLocalStorageService _localStorageService;
        private readonly ILogger _logger;
        private readonly ISessionStateService _sessionStateService;

        public TestState(ISyncLocalStorageService localStorageService, ILogger logger, ISessionStateService sessionStateService)
        {
            _localStorageService = localStorageService;
            _logger = logger;
            _sessionStateService = sessionStateService;
        }

        private string? _protectedTestText;

        public string TestText
        {
            // TODO: Move to base class and get/set in storage based on Class/Property name (ex. TestState.TestText)
            get
            {
                var result = _localStorageService.GetItem<string>("TestState.TestText");
                return result;
                //return testText ?? string.Empty;
            }
            set
            {
                try
                {
                    //testText = value;
                    _localStorageService.SetItem("TestState.TestText", value);
                    NotifyStateChanged();
                }
                catch(Exception e)
                {
                    _logger.LogError(e, "Error setting TestTextState.");
                }
            }
        }

        public string ProtectedTestText
        {
            get
            {
                return _protectedTestText;
            }
            set
            {

            }
        }

        public event Action? OnChange;

        public void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }

        public async Task SetProtectedTestText(string text)
        {
            try
            {
                await _sessionStateService.SetSessionStateData<string>("TestState.ProtectedTestText", text);
                _protectedTestText = text;
                NotifyStateChanged();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error setting ProtectedTestText.");
            }
        }

        public async Task<string> GetProtectedTestText()
        {
            try
            {
                var result = await _sessionStateService.GetSessionStateData<string>("TestState.ProtectedTestText");
                _protectedTestText = result;
                NotifyStateChanged();
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error setting ProtectedTestText.");
                return string.Empty;
            }
        }
    }
}
