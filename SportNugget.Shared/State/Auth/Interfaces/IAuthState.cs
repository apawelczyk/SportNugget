namespace SportNugget.Shared.State.Auth.Interfaces
{
    public interface IAuthState
    {
        int UserId { get; set; }
        bool IsAuthenticated { get; set; }
        string WebAPIBearerToken { get; set; }
    }
}
