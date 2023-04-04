using Microsoft.AspNetCore.Mvc;

namespace Tyche.StarterApp.Identity.Static;

public class RegisterController : Controller
{
    [HttpGet("/register")]
    public IActionResult Index()
    {
        return View("~/Identity/Static/Register.cshtml");
    }
}