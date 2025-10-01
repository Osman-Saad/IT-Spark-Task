using ITSpark.BLL.IRepository;
using ITSpark.BLL.Repository;
using ITSpark.DAL.Data;
using ITSparkTask.PL.Profiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ITSparkDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opt =>
{
    opt.Password.RequireDigit = true;
    opt.Password.RequireUppercase = true;
    opt.Password.RequireLowercase = true;
}).AddEntityFrameworkStores<ITSparkDbContext>()
.AddDefaultTokenProviders();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.LoginPath = "Account/Login"; 
                    options.AccessDeniedPath = "Home/Error";
                });
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(AppProfile));

var app = builder.Build();
var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;
var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
try
{
    var dbContext = serviceProvider.GetRequiredService<ITSparkDbContext>();
    await dbContext.Database.MigrateAsync();
}
catch (Exception ex)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, ex.Message);
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Invoice}/{action=Index}/{id?}");

app.Run();
