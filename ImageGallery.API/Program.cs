using ImageGallery.API.DbContexts;
using ImageGallery.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(configure => configure.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddDbContext<GalleryContext>(options => {
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:ImageGalleryDBConnectionString"]);
});

// register the repository
builder.Services.AddScoped<IGalleryRepository, GalleryRepository>();

// register AutoMapper-related services
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
    AddJwtBearer(options => {
        options.Authority = "https://localhost:5001";
        options.Audience = "imagegalleryapi";
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters {
            ValidTypes = new[] { "at+jwt" },
            NameClaimType = "given_name",
            RoleClaimType = "role"
            };



    });

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
