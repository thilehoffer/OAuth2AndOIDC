using Duende.IdentityServer.Models;
using Duende.IdentityServer;
namespace Company.IDP;

public static class Config {
	public static IEnumerable<IdentityResource> IdentityResources =>
		new IdentityResource[]
		{
			new IdentityResources.OpenId(),
            //Adding support for users
            new IdentityResources.Profile()
		};

	public static IEnumerable<ApiScope> ApiScopes =>
		new ApiScope[]
			{ };

	public static IEnumerable<Client> Clients =>
		[
				new Client {
					ClientName = "Image Gallery",
					ClientId = "imagegalleryclient",
					AllowedGrantTypes = GrantTypes.Code,
					RedirectUris = {
					 "https://localhost:7184/signin-oidc"
					},
					AllowedScopes = {
						 IdentityServerConstants.StandardScopes.OpenId,
						 IdentityServerConstants.StandardScopes.Profile

					},
					ClientSecrets = {
						new Secret("secret".Sha256())
					}

				}
			];
}