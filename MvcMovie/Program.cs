using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MvcMovie.Command;
using MvcMovie.Data;
using MvcMovie.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
//{
//    //config.Cookie.Name = "Cookie.Name";
//    config.LoginPath = "/account/login";
//});

builder.Services.AddDbContext<MvcMovieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MvcMovieContext")));

builder.Services.AddIdentity<UserModel, RoleModel>().AddEntityFrameworkStores<MvcMovieContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 3;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;

});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Logging.AddLog4Net();

//添加认证
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
//    {
//        builder.Configuration.Bind("JwtSetting", options);
//    })
//    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
//    {
//        //builder.Configuration.Bind("CookieSettings", options);
//        options.ExpireTimeSpan = new TimeSpan(72, 0, 0);//cookie的过期时间会72小时
//        options.LoginPath = "/account/login";//配置登录url
//        options.LogoutPath = "/account/logout";//配置登出url
//        options.AccessDeniedPath = "/account/login/accessDenied";//配置无权访问时跳转的url
//    });
var config = new JwtConfigOptions();
config = builder.Configuration.GetSection(JwtConfigOptions.Position).Get<JwtConfigOptions>();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme; //默认身份验证方案
    option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultForbidScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        builder.Configuration.Bind("CookieSettings", options);
        //options.ExpireTimeSpan = new TimeSpan(72, 0, 0);//cookie的过期时间会72小时
        //options.LoginPath = "/account/login";//配置登录url
        //options.LogoutPath = "/account/logout";//配置登出url
        //options.AccessDeniedPath = "/account/login/accessDenied";//配置无权访问时跳转的url

    }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
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
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    SeedData.Initialize(service);
}


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
