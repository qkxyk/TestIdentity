using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MvcMovie.Command;
using MvcMovie.Data;
using MvcMovie.Models;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MvcMovieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MvcMovieContext")));

builder.Services.AddIdentity<UserModel, RoleModel>().AddEntityFrameworkStores<MvcMovieContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 3;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;

    // ��������
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // �û�����
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Logging.AddLog4Net();
#region ����jwt��cookie��ע�����cookie��identity��cookie��ͬ��identity���Բ���Ҫ���²���
var config = new JwtConfigOptions();
config = builder.Configuration.GetSection(JwtConfigOptions.Position).Get<JwtConfigOptions>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            //ValidateLifetime = true,
            ValidateLifetime = false,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config.Issuer,
            ValidAudience = config.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.IssuerSigningKey)),
            ClockSkew = TimeSpan.Zero
        };
    });
#endregion
#region identity��Ҫ���ã��������identity���Բ���Ҫ��������
builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "Cookies";
    config.LoginPath = "/account/login";
});
#endregion

#region ��Ȩ
//builder.Services.AddAuthorization(config =>
//{
//    var defaultAuthBuilder = new AuthorizationPolicyBuilder();
//    var defaultPolicy = defaultAuthBuilder.RequireAuthenticatedUser().RequireClaim(ClaimTypes.Country).Build();
//    config.DefaultPolicy = defaultPolicy;
//    config.AddPolicy("Claim.Count", policyBuilder =>
//    {
//        policyBuilder.AuthenticationSchemes.Add(CookieAuthenticationDefaults.AuthenticationScheme);
//        policyBuilder.RequireClaim(ClaimTypes.Actor);
//    });
//    config.AddPolicy("Over18", policy =>
//    {
//        //policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
//        policy.RequireAuthenticatedUser();
//        //policy.Requirements.Add(new MinimumAgeRequirement());
//    });
//});
#endregion
var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var service = scope.ServiceProvider;
//    SeedData.Initialize(service);
//}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
