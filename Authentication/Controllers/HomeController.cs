using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public async Task<IActionResult> Authenticate()
        {
            var grandmaClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Yavuz"),
                new Claim(ClaimTypes.Email, "yavuz@fmail.com"),
                new Claim("Yavuz.Tells", "Veri nice coder.")
            };

            var licenceClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Yasmali"),
                new Claim("Driving Licence", "A+")
            };

            var grandmaIdentity = new ClaimsIdentity(grandmaClaims, "Yavuz Identity");
            var licenceIdentity = new ClaimsIdentity(licenceClaims, "Government");

            var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity, licenceIdentity });

            await HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index");
        }
    }
}