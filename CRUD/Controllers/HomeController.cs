using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        [Route("Error")]
        public IActionResult Error()
        {

            IExceptionHandlerPathFeature? exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature != null && exceptionHandlerPathFeature.Error != null)
            {
                ViewBag.ErrorMessage = exceptionHandlerPathFeature.Error.Message;
            }

            //Views/Shared/Error.cshtml
            return View();
        }
    }
}
