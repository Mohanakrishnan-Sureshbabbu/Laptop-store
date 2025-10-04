using Microsoft.EntityFrameworkCore;
using LaptopStore.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
/*var connectionString = builder.Configuration.GetConnectionString("AppDataContextConnection");

builder.Services.AddDbContext<AppDataContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDataContext>();*/

IConfiguration configuration = new ConfigurationBuilder()
   .AddJsonFile("appsettings.json", true, true)
   .Build();



builder.Services.AddDbContext<AppDataContext>(options => options.UseSqlServer
(configuration.GetConnectionString("Default")));

builder.Services.AddIdentity<AppUser, IdentityRole>()
.AddDefaultTokenProviders()
.AddDefaultUI()
.AddEntityFrameworkStores<AppDataContext>();


// Add services to the container.
builder.Services.AddRazorPages().AddRazorPagesOptions(options => {
    options.Conventions.AuthorizePage("/cart");
    options.Conventions.AuthorizePage("/Checkout");
    options.Conventions.AuthorizePage("/Confirmation");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
