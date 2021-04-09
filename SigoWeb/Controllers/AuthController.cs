using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SigoWeb.Infrastructure.Requests;
using SigoWeb.Models;
using System.Threading.Tasks;

namespace SigoWeb.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRefitAuth _refitAuth;

        public AuthController(IHttpContextAccessor httpContextAccessor, IRefitAuth refitAuth)
        {
            _httpContextAccessor = httpContextAccessor;
            _refitAuth = refitAuth;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var authResult = await _refitAuth.ObterTokenAsync(model);

            if (authResult.IsSuccessStatusCode)
            {
                var authResponse = await authResult.Content.ReadAsStringAsync();
                var tokenModel = JsonConvert.DeserializeObject<TokenModel>(authResponse);

                if (!JwtTokenValidator.ValidaToken(tokenModel.Token))
                {
                    ModelState.AddModelError("", "Login inválido!");
                    return View(model);
                }

                Response.Cookies.Append("token", tokenModel.Token);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Login inválido!");
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            Response.Cookies.Append("token", string.Empty);
            return RedirectToAction("Login", "Auth");
        }

        public ActionResult SemPermissao()
        {
            return View();
        }
    }
}
