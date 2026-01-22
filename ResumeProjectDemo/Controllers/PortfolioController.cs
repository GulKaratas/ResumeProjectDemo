using Microsoft.AspNetCore.Mvc;
using ResumeProjectDemo.Context;
using ResumeProjectDemo.Entities;

namespace ResumeProjectDemo.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly ResumeContext _context;
        public PortfolioController(ResumeContext context)
        {
            _context = context;
        }
        public IActionResult PortfolioList()
        {
            var values = _context.Portfolios.ToList();
            return View(values);
        }
        public IActionResult CreatePortfolio()
        {
            return View();

        }
        [HttpPost]
        public IActionResult CreatePortfolio(Portfolio portfolio)
        {
            portfolio.ImageUrl = "https://via.placeholder.com/150";
            _context.Portfolios.Add(portfolio);
            _context.SaveChanges();
            return RedirectToAction("PortfolioList");
        }
    }
}
