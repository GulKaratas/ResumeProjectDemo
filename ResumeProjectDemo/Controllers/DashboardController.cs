using Microsoft.AspNetCore.Mvc;

namespace ResumeProjectDemo.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult DashboardList()
        {
            return View();
        }
    }
}
