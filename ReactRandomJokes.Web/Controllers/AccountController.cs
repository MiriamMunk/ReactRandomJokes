using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReactBookmarkManager.Web.Modals;
using ReactRandomJokes.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReactRandomJokes.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly string _ConnString;
        public AccountController(IConfiguration con)
        {
            _ConnString = con.GetConnectionString("ConStr");
        }

        [Route("signUp")]
        [HttpPost]
        public void SignUp(SignUpViewModel u)
        {
            UserRepository repo = new(_ConnString);
            repo.SignUp(u, u.Password);
        }

        [Route("logIn")]
        [HttpPost]
        public User LogIn(LogInViewModel l)
        {
            UserRepository repo = new(_ConnString);
            var user = repo.LogIn(l.Email, l.Password);
            if (user == null)
            {
                return null;
            }

            var claims = new List<Claim>()
            {
                new Claim("user", l.Email)
            };

            HttpContext.SignInAsync(new ClaimsPrincipal(
                new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();
            return user;
        }

        [Route("getcurrentuser")]
        [HttpGet]
        public User GetCurrentUser()
        {
            UserRepository repo = new(_ConnString);
            if (!User.Identity.IsAuthenticated)
            {
                return null;
            }
            return repo.GetByEmail(User.Identity.Name);
        }

        [Route("logout")]
        [HttpPost]
        public void LogOut()
        {
            HttpContext.SignOutAsync().Wait();
        }
    }
}
