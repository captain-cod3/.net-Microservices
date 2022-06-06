using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServer
{
    public static class SD
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResource => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

        public static IEnumerable<ApiScope> apiScopes => new List<ApiScope>
        {
            new ApiScope("mango","mango server"),
            new ApiScope("read","read data"),
            new ApiScope("write", "write data"),
            new ApiScope("delete", "delete data")
        };

        public static IEnumerable<Client> clients => new List<Client>
        {
            new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret("secretKey123".Sha256())},
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "read", "write", "delete", "profile"},
            },
            new Client
            {
                ClientId = "mango",
                ClientSecrets = { new Secret("secretKey123".Sha256())},
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:7020/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:7020/signout-callback-oidc" },
                AllowedScopes = new List<string>{
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "mango"
                }
            }
        };
    }
}
