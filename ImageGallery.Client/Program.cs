using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
	.AddJsonOptions(configure =>
		configure.JsonSerializerOptions.PropertyNamingPolicy = null);

//Prevously mapped for backwards compativility by MS?
JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

// create an HttpClient used for accessing the API
builder.Services.AddHttpClient("APIClient", client => {
	client.BaseAddress = new Uri(builder.Configuration["ImageGalleryAPIRoot"]);
	client.DefaultRequestHeaders.Clear();
	client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
});

builder.Services.AddAuthentication(options => {
	options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
 .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options => {
	options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	options.Authority = "https://localhost:5001";
	options.ClientId = "imagegalleryclient";
	options.ClientSecret = "secret";
	options.ResponseType = "code";
	//defaults
	//options.Scope.Add("openid");
	//options.Scope.Add("profile");
	//options.CallbackPath = new PathString("signin-oidc");
	 
	options.SaveTokens = true;
	options.GetClaimsFromUserInfoEndpoint = true;

	//Stop middleware from removing a claim weird
	options.ClaimActions.Remove("aud");
	 
	options.ClaimActions.DeleteClaim("sid");
	options.Scope.Add("roles");
	options.ClaimActions.MapJsonKey("role", "role");
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
	app.UseExceptionHandler();
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Gallery}/{action=Index}/{id?}");

app.Run();
