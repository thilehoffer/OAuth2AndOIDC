using Duende.IdentityServer;
using Duende.IdentityServer.Models;
namespace Company.IDP;

public static class Config {
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            //Adding support for users
            new IdentityResources.Profile(),

            new IdentityResource("roles", "Your role(s)", new [] {"role"} )
        };

    public static IEnumerable<ApiResource> ApiResources => [
        new ApiResource ("imagegalleryapi", "Image Gallery API",
            ["role"]
            ){
                Scopes = { "imagegallery.fullaccess" }
        }
    ];
    public static IEnumerable<ApiScope> ApiScopes =>
      [new ApiScope("imagegallery.fullaccess")];

    public static IEnumerable<Client> Clients =>
        [
                new Client {
                    ClientName = "Image Gallery",
                    ClientId = "imagegalleryclient",
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = {
                     "https://localhost:7184/signin-oidc"
                    },
                    PostLogoutRedirectUris = {
                        "https://localhost:7184/signout-callback-oidc"
                    },
                    AllowedScopes = {
                         IdentityServerConstants.StandardScopes.OpenId,
                         IdentityServerConstants.StandardScopes.Profile,
                         "roles",
                         "imagegallery.fullaccess"
                    },
                    ClientSecrets = {
                        new Secret("secret".Sha256())
                    },
                    //This paramter cause consent screen that shows and allows access 
                    RequireConsent = true

                }
            ];
    }