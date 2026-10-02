

using Microsoft.EntityFrameworkCore;
using SharedModelsLib;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Add this service for Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options =>
    {
        options.DetailedErrors = true;
    });

//ajouter un service DBContext injectable
builder.Services.AddDbContext<ColvalteacherSportsdbContext>(
    options =>
        options.UseMySQL(builder.Configuration.GetConnectionString("OnlineMySqlDB"))
                .UseLazyLoadingProxies()

    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

//Add this middleware for Blazor
app.MapRazorComponents<MyBlazorLib.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();
