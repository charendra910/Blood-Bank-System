using Blood_Bank_System.Data;
using Blood_Bank_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//registering the connection string

builder.Services.AddDbContext<ContactContext>
    (s => s.UseSqlServer(builder.Configuration.GetConnectionString("con")));

//register for the appointment connection string
builder.Services.AddDbContext<IndexContext>
    (appoint => appoint.UseSqlServer(builder.Configuration.GetConnectionString("con")));

//register for the loginsignup
builder.Services.AddDbContext<AppDbContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("con")));

builder.Services.AddIdentity<UsersModel, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();





//registering dbcontext for the blood donors
builder.Services.AddDbContext<AddDonorContext>
    (add => add.UseSqlServer(builder.Configuration.GetConnectionString("con")));

builder.Services.AddDbContext<ReceiveBloodContext>
    (s1 => s1.UseSqlServer(builder.Configuration.GetConnectionString("con")));

// Register the ApplicationDbContext with the SQL Server connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("con")));
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

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Admin")),
    RequestPath = "/Admin"
});

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=IndexAppoint}/{action=AddAppoint}/{id?}");
//pattern: "{controller=Admin}/{action=Dashboard}/{id?}");


app.Run();
