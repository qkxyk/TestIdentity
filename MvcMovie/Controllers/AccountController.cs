using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MvcMovie.Command;
using MvcMovie.Models;
using MvcMovie.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MvcMovie.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly IConfiguration config;



        public AccountController(ILogger<AccountController> logger, SignInManager<UserModel> signInManager, IConfiguration config)
        {
            this._logger = logger;
            this._signInManager = signInManager;
            this.config = config;

        }

        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        private bool ValidateLogin(string userName, string password)
        {
            // For this sample, all logins are successful.

            return true;
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(UserLogin req, string? returnUrl)
        {


            if (ModelState.IsValid)
            {
                //var user = await _signInManager.PasswordSignInAsync(req.UserName, req.Password,req.Save,true);
                //if (user == null)
                //{
                //    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                //    return View();
                //}
                //        var claims = new List<Claim>
                //{
                //    new Claim(ClaimTypes.Name, user.Email),
                //    new Claim("FullName", user.FullName),
                //    new Claim(ClaimTypes.Role, "Administrator"),
                //};
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, req.UserName),
                    new Claim(ClaimTypes.Role, "Administrator")
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                bool bSave = false;
                if (req.Save == "on")
                {
                    bSave = true;
                }
                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    IsPersistent = bSave,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                    AllowRefresh = true
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                //_logger.LogInformation("User {Email} logged in at {Time}.",
                //    user.Email, DateTime.UtcNow);

                if (string.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("Index", "home");
                }
                else
                {
                    return Redirect(returnUrl);
                }
            }
            return View();


            //var SchoolClaims = new List<Claim>()
            //  {
            //    new Claim(ClaimTypes.Name,"Jack"),
            //    new Claim(ClaimTypes.Email,"Jack@fmail.com"),
            //    new Claim(ClaimTypes.Country,"china")
            //  };

            //var LicensClaims = new List<Claim>()
            //{
            //    new Claim(ClaimTypes.Name,"Jack.li"),
            //    new Claim(ClaimTypes.Email,"Jack@fmail.com"),
            //    new Claim("begin","2000.10.1")
            //};
            //var SchoolIdentity = new ClaimsIdentity(SchoolClaims, "Student Identity");
            //var CarManagerIdentity = new ClaimsIdentity(LicensClaims, "Licens Identity");
            //var userPrincipal = new ClaimsPrincipal(new[] { SchoolIdentity, CarManagerIdentity });

            //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);
            //if (string.IsNullOrEmpty(returnUrl))
            //{
            //    return RedirectToAction("Index", "home");
            //}
            //else
            //{
            //    //return RedirectToAction("index","Movies");
            //    //return RedirectToAction("index", "home");
            //    return Redirect(returnUrl);
            //}
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        [HttpPost]
        public IActionResult JwtLogin([FromBody] UserLogin req)
        {
            var _options = config.GetSection(JwtConfigOptions.Position).Get<JwtConfigOptions>();
            var claims = new Claim[]
            {
                new Claim ("Account", req.UserName),
                new Claim ("Role", "Administrator"),
                //new Claim("Code",u.Code),
                //new Claim("GroupId",u.GroupId),
                new Claim("IsAdmin","true")
                //new Claim("Id",u.Id.ToString()),
                //new Claim("Key",$"{u.Account}+{u.Id}")
            };
            //ClaimsIdentity id =  
            var now = DateTime.Now;
            var expires = now.Add(TimeSpan.FromMinutes(_options.AccessTokenExpiresMinutes));
            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                //notBefore: now,
                //expires: expires,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.IssuerSigningKey)), SecurityAlgorithms.HmacSha256));
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return Content(jwtToken);
        }

    }
}
