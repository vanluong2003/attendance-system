using AttendanceSystem.Core.ConfigOptions;
using AttendanceSystem.Core.Domain.Identity;
using AttendanceSystem.Core.Events.LoginSuccessed;
using AttendanceSystem.Core.Models.Content;
using AttendanceSystem.Core.SeedWorks;
using AttendanceSystem.Data;
using AttendanceSystem.Data.Repositories;
using AttendanceSystem.Data.SeedWorks;
using AttendanceSystem.WebApp.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Custom setup
builder.Services.Configure<SystemConfig>(configuration.GetSection("SystemConfig"));

builder.Services.AddDbContext<AttendanceSystemContext>(options => options.UseSqlServer(connectionString));

#region Configure Identity
builder.Services.AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AttendanceSystemContext>()
                  .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = false;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddScoped<SignInManager<AppUser>, SignInManager<AppUser>>();
builder.Services.AddScoped<UserManager<AppUser>, UserManager<AppUser>>();
builder.Services.AddScoped<RoleManager<AppRole>, RoleManager<AppRole>>();

builder.Services.AddScoped<IUserClaimsPrincipalFactory<AppUser>,
   CustomClaimsPrincipalFactory>();
#endregion

builder.Services.AddAutoMapper(typeof(ClassInListDto));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LoginSuccessedEvent).Assembly));

#region Configure Services
// Add services to the container.
builder.Services.AddScoped(typeof(IRepository<,>), typeof(RepositoryBase<,>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// Business services and repositories
var services = typeof(ClassRepository).Assembly.GetTypes()
    .Where(x => x.GetInterfaces().Any(i => i.Name == typeof(IRepository<,>).Name)
    && !x.IsAbstract && x.IsClass && !x.IsGenericType);

foreach (var service in services)
{
    var allInterfaces = service.GetInterfaces();
    var directInterface = allInterfaces.Except(allInterfaces.SelectMany(t => t.GetInterfaces())).FirstOrDefault();
    if (directInterface != null)
    {
        builder.Services.Add(new ServiceDescriptor(directInterface, service, ServiceLifetime.Scoped));
    }
}

#endregion

//Start pipeline
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();