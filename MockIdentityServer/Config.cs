using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace MockIdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("mock_api"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // interactive client using code flow + pkce
                new Client
                {
                    ClientName = "eSOR Development",
                    ClientId = "sor-rod-dev",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("21B5F798-BE55-42BC-8AA8-0025B903DC3B".Sha256())
                    },

                    // Enabled = true,

                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    
                    RequirePkce = false, // for Hybrid
                    AllowAccessTokensViaBrowser = true,

                    RequireConsent = false,
                    AllowRememberConsent = false,

                    // https://stackoverflow.com/questions/40575185/cannot-use-refresh-token-to-obtain-new-access-token-and-refresh-token-in-identit
                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 3600,

                    RefreshTokenUsage = TokenUsage.ReUse,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = 3600,
                    
                    AllowOfflineAccess = true,

                    RedirectUris =
                    {
                        "http://localhost:5000/api/auth/callback",
                    },

                    PostLogoutRedirectUris =
                    {
                        "http://localhost:4200/en",
                    },

                    // AllowAccessToAllScopes = true,

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        // removed name
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "mock_api"
                    }
                },
            };
    }
}