using SportNugget.Shared.State.Auth.Interfaces;

namespace SportNugget.Shared.State.Auth
{
    public class AuthState : IAuthState
    {
        #region Private fields
        private int _userId = 0;
        private bool _isAuthenticated = false;
        private string _webAPIBearerToken = null;

        public event Action OnChange;
        #endregion

        public AuthState()
        {

        }

        #region Properties
        public int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                NotifyStateChanged();
            }
        }
        public bool IsAuthenticated { get => _isAuthenticated; set { _isAuthenticated = value; } }

        public string WebAPIBearerToken
        {
            get => _webAPIBearerToken;
            set
            {
                _webAPIBearerToken = value;
            }
        }
        #endregion

        #region Functions
        public void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }
        #endregion

        #region Helper Functions
        #endregion
    }
}
