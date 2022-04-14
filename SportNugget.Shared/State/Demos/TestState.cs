using SportNugget.Shared.State.Demos.Interfaces;

namespace SportNugget.Shared.State.Demos
{
    public class TestState : ITestState
    {
        private string? testText;

        public string TestText
        {
            get
            {
                return testText ?? string.Empty;
            }
            set
            {
                testText = value;
                NotifyStateChanged();
            }
        }

        public event Action? OnChange;

        public void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }
    }
}
