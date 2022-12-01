using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO;

namespace CRUD.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterDto registerDto)
        {
            return RedirectToAction(nameof(PersonsController.Index), "Persons");
        }
    }
}
