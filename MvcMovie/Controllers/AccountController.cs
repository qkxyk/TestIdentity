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
        private readonly UserManager<UserModel> _userManager;
        private readonly IConfiguration config;




        public AccountController(ILogger<AccountController> logger, SignInManager<UserModel> signInManager, UserManager<UserModel> userManager, IConfiguration config)
        {
            this._logger = logger;
            this._signInManager = signInManager;
            this._userManager = userManager;
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
            var user = await _userManager.FindByNameAsync(req.UserName);
            if (user != null)
            {
                // PasswordSignInAsync密码登录
                var Signresult = await _signInManager.PasswordSignInAsync(user, req.Password, false, false);
                if (Signresult.Succeeded)
                {
                    return Redirect(returnUrl);
                }
            }
            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return Redirect(returnUrl);
            }


        }

        public async Task<IActionResult> Logout()
        {
            //await HttpContext.SignOutAsync();
            await _signInManager.SignOutAsync();
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

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(UserRegister req, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                UserModel user = new UserModel { UserName = req.Name, Email = req.Email };

                var result = await _userManager.CreateAsync(user, req.Password);
                //await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Country, "china"));
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, true);//SignInAsync用于新注册的用户登录
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return View("login");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "模型验证失败");
                return View("login");
            }
        }

      
    }
}
