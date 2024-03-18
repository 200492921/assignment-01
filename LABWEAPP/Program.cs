using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AspNet.Security.OAuth.GitHub;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(opts =>
{
    opts.UseMySql(
        builder.Configuration["ConnectionStrings:MyDBConnection"]!,
        new MySqlServerVersion(new Version(8, 0, 35))
    );
});

builder.Services
    .AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthentication().AddGitHub(o =>
{
    o.ClientId = builder.Configuration["Authentication:GitHub:ClientId"] ?? throw new InvalidOperationException();
    o.ClientSecret = builder.Configuration["Authentication:GitHub:ClientSecret"] ?? throw new InvalidOperationException();
    o.CallbackPath = "/signin-github";
    // Grants access to read a user's profile data.
    // https://docs.github.com/en/developers/apps/building-oauth-apps/scopes-for-oauth-apps
    o.Scope.Add("read:user");
    // Optional
    // if you need an access token to call GitHub Apis
    o.Events.OnCreatingTicket += context =>
    {
        if (context.AccessToken is not null)
        {
            context.Identity?.AddClaim(new Claim("access_token", context.AccessToken));
        }
        
        return Task.CompletedTask;
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/Home/Error");

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Products}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name : "areas",
    pattern : "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapGet("/", context =>
{
    // Redirect to Products/Index when the root path ("/") is accessed
    var response = context.Response;
    response.Redirect("/Products/Index");
    return Task.CompletedTask;
});
app.MapRazorPages(); // This is important for Identity

app.Run();