using FILMEX.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FILMEX.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using FILMEX.Models.Entities;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// weryfikacja maila
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IEmailSender, EmailSender>();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//ogarnac czemu nie dziala seedowanie

/*using(var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string email = "Admin@admin123.com";
    string pwd = "Admin@admin123.com";

    if(await userManager.FindByEmailAsync(email) == null)
    {
        var adminUser = new User();
        adminUser.UserName = "Admin";
        adminUser.FirstName = "Admin";
        adminUser.LastName = "Admin";
        adminUser.Email = email;
        adminUser.EmailConfirmed = true;
        
        

        await userManager.CreateAsync(adminUser,pwd);

        await userManager.AddToRoleAsync(adminUser, "Admin");
    }

}*/


app.Run();