namespace SportNugget.Shared.State.Demos.Interfaces
{
    public interface ITestState
    {
        public string TestText { get; set; }
        public string ProtectedTestText { get; set; }
        public Task SetProtectedTestText(string text);
        public Task<string> GetProtectedTestText();
        public event Action? OnChange;
        public void NotifyStateChanged();
    }
}
