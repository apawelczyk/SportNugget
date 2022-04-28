using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace SportNugget.Identity.Configuration
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource()
                {
                    Name = "SportNugget.Web.Client",
                    DisplayName = "Sport Nugget Web Client Resource",
                    Description = "Description",
                    Scopes = new[]{ "web-api-scope" },
                    Enabled = true,
                },
                new ApiResource()
                {
                    Name = "SportNugget.Web.API",
                    DisplayName = "Sport Nugget Web API Resource",
                    Description = "Description",
                    Scopes = new[]{ "web-api-scope" },
                    Enabled = true,
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                // SportNugget Web Client
                new Client
                {
                    ClientName = "SportNugget Web Assembly App",
                    ClientId = "579a56d4-cb3c-489c-af75-5f0cfeedb660",
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets =
                    {
                        new Secret("secret-key-11111".Sha256())
                    },
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedCorsOrigins = { "https://localhost:5001" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "web-api-scope"
                    },
                    RedirectUris = { "https://localhost:5001/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:5001/authentication/logout-callback" },
                    Enabled = true
                },
                // SportNugget Web API Client
                new Client
                {
                    ClientName = "SportNugget Web API App",
                    ClientId = "dc32f4d3-7911-4233-8a1b-bf143617ebeb",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret-key-22222".Sha256())
                    },
                    AllowedScopes = 
                    { 
                        IdentityServerConstants.StandardScopes.OpenId, 
                        "web-api-scope"
                    },
                    Enabled = true
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("web-api-scope", "Web API Scope")
            };
        }

        public static IEnumerable<TestUser> GetUsers()
        {
            return new List<TestUser>()
            {
                new TestUser()
                {
                    SubjectId = "1",
                    Username = "user1",
                    Password = "password1",
                    Claims = new[]
                    {
                        new Claim("roleType", "user-type")
                    }
                },
                new TestUser()
                {
                    SubjectId = "2",
                    Username = "admin1",
                    Password = "password1",
                    Claims = new[]
                    {
                        new Claim("roleType", "admin-type")
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
    }
}
