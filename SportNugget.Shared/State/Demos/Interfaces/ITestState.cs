namespace SportNugget.Shared.State.Demos.Interfaces
{
    public interface ITestState
    {
        public string TestText { get; set; }
        public string ProtectedTestText { get; set; }
        public event Action? OnChange;
        public void NotifyStateChanged();
    }
}
