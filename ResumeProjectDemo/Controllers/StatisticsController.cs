using Microsoft.AspNetCore.Mvc;
using ResumeProjectDemo.Context;

namespace ResumeProjectDemo.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly ResumeContext _context;

        public StatisticsController(ResumeContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
             ViewBag.v1 = _context.Messages.Count();
             ViewBag.v2 = _context.Messages.Where(x => x.IsRead == false).Count();
             ViewBag.v3 = _context.Messages.Where(x => x.IsRead == true).Count();
            ViewBag.v4 = _context.Messages.Where(x => x.MessageId == 1).Select(x => x.NameSurname).FirstOrDefault();
            return View();
        }
    }
}
