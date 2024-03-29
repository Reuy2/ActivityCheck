using ActivityCheck.DAL;
using ActivityCheck.DAL.Interfaces;
using ActivityCheck.DAL.Repositories;
using ActivityCheck.Domain.Entity;
using ActivityCheck.Service;
using ActivityCheck.Service.Implementations;
using ActivityCheck.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddAuthentication("Cookies").AddCookie(options => options.LoginPath = "/user/login");
builder.Services.AddAuthorization();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddScoped<IBaseRepository<Activity>, ActivityRepository>();
builder.Services.AddScoped<IBaseRepository<User>, UserRepository>();

builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<IUserService, UserService>();



var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

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
