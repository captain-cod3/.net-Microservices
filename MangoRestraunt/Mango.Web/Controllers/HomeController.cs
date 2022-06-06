using Mango.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Mango.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() { 
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Login() {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout() {
            return SignOut("Cookies", "oidc");
        }
    }
}