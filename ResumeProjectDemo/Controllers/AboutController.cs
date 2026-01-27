using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ResumeProjectDemo.Context;
using ResumeProjectDemo.Entities;

namespace ResumeProjectDemo.Controllers
{
    public class AboutController : Controller
    {
        private readonly ResumeContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        // Constructor'ı düzelttim - webHostEnvironment parametresini ekledim
        public AboutController(ResumeContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult AboutList()
        {
            var values = _context.Abouts.ToList();
            return View(values);
        }

        [HttpPost]
        // imageFile parametresini ekledim
        public IActionResult UpdateAbout(About about, IFormFile? imageFile)
        {
            try
            {
                var existingAbout = _context.Abouts.Find(about.AboutId);
                if (existingAbout == null)
                {
                    return NotFound();
                }

                // Eğer yeni fotoğraf yüklendiyse
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Eski fotoğrafı sil (opsiyonel)
                    if (!string.IsNullOrEmpty(existingAbout.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, existingAbout.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // Yeni fotoğrafı kaydet (direkt images klasörüne)
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                    // Klasör yoksa oluştur
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

                    existingAbout.ImageUrl = "/images/" + uniqueFileName;
                }

                existingAbout.NameSurname = about.NameSurname;
                existingAbout.Title = about.Title;
                existingAbout.Description = about.Description;

                _context.SaveChanges();
                TempData["Success"] = "Bilgiler başarıyla güncellendi!";
                return RedirectToAction("AboutList");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Bir hata oluştu: " + ex.Message;
                return RedirectToAction("AboutList");
            }
        }
    }
}