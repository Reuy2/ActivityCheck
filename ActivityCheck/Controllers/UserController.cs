using ActivityCheck.Domain.Entity;
using ActivityCheck.Domain.Response;
using ActivityCheck.Domain.ViewEntity.User;
using ActivityCheck.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ActivityCheck.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        //регистрация и авторизация
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string? returnUrl)
        {
            var form = HttpContext.Request.Form;
            string login = form["login"];
            string password = form["password"];

            var response = await _userService.GetUser(login, password);

            if(response.StatusCode == Domain.Enum.StatusCode.NotFound)
            {
                ViewBag.isIncorrectLogin = true;
                return View();
            }

            if(response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                await AuthorizeUser(response.Data);
                return Redirect(returnUrl ?? "/");
            }

            return new StatusCodeResult(int.Parse(Domain.Enum.StatusCode.InternalServerError.ToString()));
        }

        private async Task AuthorizeUser(User user)
        {
            var claims = new List<Claim>() { new Claim(ClaimTypes.Name, user.Name),new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
            var claimIdentity = new ClaimsIdentity(claims, "Cookies");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] UserViewModel userViewModel)
        {
            //рефакторнуть чтобы нормально выглядло
            var response = await _userService.FindUser(userViewModel.Name, userViewModel.Email);

            if(response.StatusCode == Domain.Enum.StatusCode.NotFound)
            {
                response =  await _userService.CreateUser(userViewModel);
                if(response.StatusCode == Domain.Enum.StatusCode.Ok)
                {
                    AuthorizeUser(response.Data);
                    return Redirect("/");
                }
                else
                {
                    return Redirect("Error");
                }
            }
            else if(response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                ViewBag.isIncorrectRegister = true;
                return View();
            }
            else
            {
                return Redirect("Error");
            }

        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
