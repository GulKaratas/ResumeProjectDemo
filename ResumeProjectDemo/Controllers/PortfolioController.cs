using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResumeProjectDemo.Context;
using ResumeProjectDemo.Entities;

namespace ResumeProjectDemo.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly ResumeContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PortfolioController(ResumeContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult PortfolioList()
        {
            var values = _context.Portfolios.ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreatePortfolio()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult CreatePortfolio(Portfolio portfolio, IFormFile? imageFile)
        {
            try
            {
                // Eğer dosya yüklendiyse
                if (imageFile != null && imageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(fileStream);
                    }

                    portfolio.ImageUrl = "/images/" + uniqueFileName;
                }
                // Eğer ImageUrl boşsa varsayılan resim koy
                else if (string.IsNullOrEmpty(portfolio.ImageUrl))
                {
                    portfolio.ImageUrl = "https://via.placeholder.com/1200x800?text=No+Image";
                }

                _context.Portfolios.Add(portfolio);
                _context.SaveChanges();
                TempData["Success"] = "Proje başarıyla eklendi!";
            }
            catch
            {
                TempData["Error"] = "Proje eklenirken hata oluştu!";
            }
            return RedirectToAction("PortfolioList");
        }
        [HttpPost]
        public IActionResult DeletePortfolio(int id)
        {
            var value = _context.Portfolios.Find(id);
            if (value != null)
            {
                _context.Portfolios.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Proje başarıyla silindi!";
            }
            return RedirectToAction("PortfolioList");   
        }

        [HttpGet]
        public IActionResult UpdatePortfolio(int id)
        {
            var value = _context.Portfolios
                .Include(x => x.Category)
                .FirstOrDefault(x => x.PortfolioId == id);

            if (value == null)
            {
                return NotFound();
            }

            ViewBag.Categories = _context.Categories.ToList();
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdatePortfolio(Portfolio portfolio, IFormFile? imageFile)
        {
            try
            {
                var value = _context.Portfolios.Find(portfolio.PortfolioId);
                if (value != null)
                {
                    // Eğer yeni dosya yüklendiyse
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        // Eski dosyayı sil (eğer local path ise)
                        if (!string.IsNullOrEmpty(value.ImageUrl) && value.ImageUrl.StartsWith("/images/"))
                        {
                            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, value.ImageUrl.TrimStart('/'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        // Yeni dosyayı kaydet
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                        
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            imageFile.CopyTo(fileStream);
                        }

                        value.ImageUrl = "/images/" + uniqueFileName;
                    }
                    // Dosya yüklenmediyse ImageUrl'i güncelle (manuel URL girilmişse)
                    else if (!string.IsNullOrEmpty(portfolio.ImageUrl))
                    {
                        value.ImageUrl = portfolio.ImageUrl;
                    }

                    value.Title = portfolio.Title;
                    value.CategoryId = portfolio.CategoryId;
                    
                    _context.SaveChanges();
                    TempData["Success"] = "Proje başarıyla güncellendi!";
                }
            }
            catch
            {
                TempData["Error"] = "Proje güncellenirken hata oluştu!";
            }
            return RedirectToAction("PortfolioList");
        }
    }
}
