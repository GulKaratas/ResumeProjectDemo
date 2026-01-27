using Microsoft.AspNetCore.Mvc;
using ResumeProjectDemo.Context;
using ResumeProjectDemo.Entities;

namespace ResumeProjectDemo.Controllers
{
    public class SkillsController : Controller
    {
        private readonly ResumeContext _context;

        public SkillsController(ResumeContext context)
        {
            _context = context; 
        }
        public IActionResult SkillsList()
        {
            var values = _context.Skilss.ToList();
            return View(values);
        }

        [HttpPost]
        public IActionResult CreateSkills(Skils skils)
        {
            try
            {
                _context.Skilss.Add(skils);
                _context.SaveChanges();
                TempData["Success"] = "Yetenek başarıyla eklendi!";
            }
            catch
            {
                TempData["Error"] = "Yetenek eklenirken hata oluştu!";
            }
            return RedirectToAction("SkillsList");
        }

        [HttpPost]
        public IActionResult UpdateSkills(Skils skils)
        {
            try
            {
                var existing = _context.Skilss.Find(skils.SkilsId);
                if (existing != null)
                {
                    existing.Title = skils.Title;
                    existing.Description = skils.Description;
                    existing.Percentage = skils.Percentage;
                    existing.Category = skils.Category;
                    _context.SaveChanges();
                    TempData["Success"] = "Yetenek başarıyla güncellendi!";
                }
            }
            catch
            {
                TempData["Error"] = "Yetenek güncellenirken hata oluştu!";
            }
            return RedirectToAction("SkillsList");
        }

        [HttpPost]
        public IActionResult DeleteSkills(int skilsId)
        {
            try
            {
                var skill = _context.Skilss.Find(skilsId);
                if (skill != null)
                {
                    _context.Skilss.Remove(skill);
                    _context.SaveChanges();
                    TempData["Success"] = "Yetenek başarıyla silindi!";
                }
            }
            catch
            {
                TempData["Error"] = "Yetenek silinirken hata oluştu!";
            }
            return RedirectToAction("SkillsList");
        }
    }
}
