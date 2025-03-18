using DynamicMenu.Core.Interfaces;
using DynamicMenu.Infrastructure.Data;
using DynamicMenu.Infrastructure.Repositories;
using DynamicMenu.Web.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation(); // Hot reload için gerekli

// DbContext
builder.Services.AddDbContext<DynamicMenuDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("DynamicMenu.Infrastructure")
    ), ServiceLifetime.Scoped); // Scoped lifetime eklendi

builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<IMenuGroupRepository, MenuGroupRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();

builder.Services.AddHttpClient<RemoteServiceDynamicMenuAPI>()
    .SetHandlerLifetime(TimeSpan.FromSeconds(5))
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { ServerCertificateCustomValidationCallback = delegate { return true; } });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();


app.Run();