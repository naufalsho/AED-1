using Core;
using Core.Extensions;
using Core.Helpers;
using Core.Models;
using Domain.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AccountController : Controller
    {
        private readonly IAccountService _accSvc;
        private readonly AppSettings _appSettings;

        public AccountController(
            IAccountService accSvc,
            IOptions<AppSettings> appSettings)
        {
            _accSvc = accSvc;

            _appSettings = appSettings.Value;
        }

        [HttpGet("Login")]
        public async Task<IActionResult> Login()
        {
            ViewData["GroupMenuName"] = "Login";
            ViewData["MenuName"] = "Login";

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return View();
        }

        [HttpPost("Authenticate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Authenticate(AuthLoginDto data)
        {
            var ret = await _accSvc.Authenticate(data);

            if (ret.IsFailed)
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp.Message);
            }

            var userData = ret.Value;

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, userData.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, userData.UserId),
                    new Claim(ClaimTypes.Name, userData.Name),
                    new Claim(ClaimTypes.Role, userData.Role.Id.ToString()),
                    new Claim("RoleName", userData.Role.Name)
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = false
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            var userMenu = await _accSvc.GetUserMenu(userData.Role.Id);
            var nextMenu = userMenu.Value.FirstOrDefault();

            if (nextMenu != null)
            {
                if (data.Password == _appSettings.DefaultPassword)
                {
                    return Ok(Url.Action("ForceChangePassword").ToString());
                }

                return Ok(Url.Action("Index", nextMenu.Menus.FirstOrDefault().Controller).ToString());
            }

            return Ok(Url.Action("Logout").ToString());
        }

        [Authorize]
        [HttpGet("ChangePassword")]
        public IActionResult ChangePassword()
        {
            ViewData[ViewDataType.Controller] = "Account";
            ViewData[ViewDataType.Action] = "ChangePassword";

            ViewData[ViewDataType.ModalType] = ModalType.Edit;
            ViewData[ViewDataType.ModalTitle] = "Change Password";

            var ret = new ChangePasswordDto()
            {
                Id = User.GetUserClaims().Sid
            };

            return View(ret);
        }

        [Authorize]
        [HttpPost("ChangePassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto data)
        {
            var ret = await _accSvc.ChangePassword(data);

            if (ret.IsSuccess)
            {
                return Ok();
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp.Message);
            }
        }

        [Authorize]
        [HttpGet("ForceChangePassword")]
        public IActionResult ForceChangePassword()
        {
            ViewData["GroupMenuName"] = "ForceChangePassword";
            ViewData["MenuName"] = "ForceChangePassword";

            var ret = new ChangePasswordDto()
            {
                Id = User.GetUserClaims().Sid,
                OldPassword = _appSettings.DefaultPassword
            };

            return View(ret);
        }

        [Authorize]
        [HttpPost("ForceChangePassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForceChangePassword(ChangePasswordDto data)
        {
            var ret = await _accSvc.ChangePassword(data);

            if (ret.IsSuccess)
            {
                return Ok();
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp.Message);
            }
        }

        [Authorize]
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
    }
}
