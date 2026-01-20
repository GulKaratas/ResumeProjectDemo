using Microsoft.AspNetCore.Mvc;

namespace ResumeProjectDemo.Controllers
{
    public class AdminLayoutController : Controller
    {
       public IActionResult Index()
        {
            return View();
        }
    }
}
