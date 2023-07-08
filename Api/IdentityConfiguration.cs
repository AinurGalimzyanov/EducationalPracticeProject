using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Api;

public class IdentityConfiguration
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("HrelloWebAPI", "Web API")
        };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            new ApiResource("HrelloWebAPI", "Web API", new []
                { JwtClaimTypes.Name})
            {
                Scopes = {"HrelloWebAPI"}
            }
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "hrello-web-api",
                ClientName = "Hrello Web",
                AllowedGrantTypes = new[] { OidcConstants.GrantTypes.JwtBearer, OidcConstants.GrantTypes.Password, OidcConstants.GrantTypes.RefreshToken },
                RequireClientSecret = false,
                RequirePkce = true,
                RedirectUris =
                {
                    "http://.../signin-oidc"
                },
                AllowedCorsOrigins =
                {
                    "http://..."
                },
                PostLogoutRedirectUris =
                {
                    "http:/.../signout-oidc"
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "NotesWebAPI"
                },
                AllowAccessTokensViaBrowser = true,
                AllowOfflineAccess = true
            }
        };
}