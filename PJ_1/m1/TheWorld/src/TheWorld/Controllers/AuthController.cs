using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Models;

namespace TheWorld.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<WorldUser> _sm;

        public AuthController(SignInManager<Models.WorldUser> sm)
        {
            _sm = sm;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToActionPermanent("trips", "app");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(ViewModels.LoginViewModel login, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                var sm = await _sm.PasswordSignInAsync(login.Username, login.Password, true, false);

                if (sm.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                        return RedirectToAction("Trips", "App");
                    else
                        return Redirect(returnUrl);
                }
                else
                    ModelState.AddModelError("", "User/ Password incorrect! ");
            }

            return View();
        }

        public async Task<ActionResult> Logout()
        {
            if(User.Identity.IsAuthenticated)
            {
                await _sm.SignOutAsync();
            }

            return RedirectToAction("Index", "App");
        }

    }
}
