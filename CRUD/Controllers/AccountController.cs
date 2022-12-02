using IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO;

namespace CRUD.Controllers
{
    [Route("[controller]/[action]")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            //If errors then no registration and throw errors on view
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return View(registerDto);
            }

            //If model is valid then create application user
            var user = new ApplicationUser()
            {
                PersonName = registerDto.PersonName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                PhoneNumber = registerDto.Phone,
            };

            //Create user using UserManager object and store result
            IdentityResult identityResult = await _userManager.CreateAsync(user, registerDto.Password);

            //If registration succeeded then redirect to Home page else throw errors on view
            if (identityResult.Succeeded)
            {
                //Make user login
                //Add checkbox of remember me in view and set isPersistent to true if checked
                await _signInManager.SignInAsync(user, isPersistent: false);

                //Here we have redirected him to home page and now app.UseAuthentication() will read cookie
                //and identity the user and show him home page is his certs are valid
                return RedirectToAction(nameof(PersonsController.Index), "Persons");
            }
            else
            {
                foreach (IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError("Register", error.Description);
                }
            }

            return View(registerDto);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return View(loginDto);
            }

            var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(PersonsController.Index), "Persons");
            }

            ModelState.AddModelError("Login", "Invalid Email or Password");

            return View(loginDto);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(PersonsController.Index), "Persons");
        }
    }
}
