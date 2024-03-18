
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(opts => {
    opts.UseMySql(
        builder.Configuration["ConnectionStrings:MyDBConnection"]!,
        new MySqlServerVersion(new Version(8, 0, 35)));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}");

app.MapGet("/", context =>
{
    // Redirect to Products/Index when the root path ("/") is accessed
    var response = context.Response;
    response.Redirect("/Products/Index");
    return Task.CompletedTask;
});


app.Run();
