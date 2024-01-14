using ActivityCheck.DAL;
using ActivityCheck.DAL.Interfaces;
using ActivityCheck.DAL.Repositories;
using ActivityCheck.Domain.Entity;
using ActivityCheck.Service;
using ActivityCheck.Service.Implementations;
using ActivityCheck.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddScoped<IBaseRepository<Activity>, ActivityRepository>();
builder.Services.AddScoped<IActivityService, ActivityService>();


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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
