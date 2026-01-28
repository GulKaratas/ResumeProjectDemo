using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResumeProjectDemo.Context;

namespace ResumeProjectDemo.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ResumeContext _context;
        public DashboardController(ResumeContext context)
        {
            _context = context;
        }
        public IActionResult DashboardList()
        {
            // İstatistikler
            ViewBag.TotalProjects = _context.Portfolios.Count();
            ViewBag.UnreadMessages = _context.Messages.Count(m => !m.IsRead);
            ViewBag.TotalMessages = _context.Messages.Count();
            ViewBag.TotalSkills = _context.Skilss.Count();
            ViewBag.TotalExperiences = _context.Experiences.Count();
            ViewBag.TotalEducations = _context.Educations.Count();
            
            // Son 4 Mesaj
            ViewBag.RecentMessages = _context.Messages
                .OrderByDescending(m => m.SendDate)
                .Take(4)
                .ToList();
            
            // Son 4 Proje (Category ile birlikte)
            ViewBag.RecentProjects = _context.Portfolios
                .Include(p => p.Category)
                .OrderByDescending(p => p.PortfolioId)
                .Take(4)
                .ToList();
            
            // En İyi 5 Beceri (Percentage'e göre)
            ViewBag.TopSkills = _context.Skilss
                .OrderByDescending(s => s.Percentage)
                .Take(5)
                .ToList();
            
            return View();
        }
    }
}
