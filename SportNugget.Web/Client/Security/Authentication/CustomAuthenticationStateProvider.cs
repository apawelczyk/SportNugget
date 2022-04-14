using Microsoft.AspNetCore.Components.Authorization;
using SportNugget.Shared.State.Auth.Interfaces;
using System.Security.Claims;

namespace SportNugget.Web.Client.Security.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthState _authState;

        public CustomAuthenticationStateProvider(IAuthState authState)
        {
            _authState = authState;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var bearerToken = _authState.WebAPIBearerToken;
            if (string.IsNullOrWhiteSpace(bearerToken))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "mrfibuli"),
            }, "Fake authentication type");

            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }
    }
}
