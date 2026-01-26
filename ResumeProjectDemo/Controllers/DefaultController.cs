using Microsoft.AspNetCore.Mvc;
using ResumeProjectDemo.Context;
using ResumeProjectDemo.Entities;


namespace ResumeProjectDemo.Controllers
{
    public class DefaultController : Controller
    {
        private readonly ResumeContext _context;

        public DefaultController(ResumeContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendMessage([FromBody] Message message)
        {
            try
            {
                message.SendDate = DateTime.Now;
                message.IsRead = false;

                if (string.IsNullOrEmpty(message.Subject))
                {
                    message.Subject = "Konu Yok";
                }
                _context.Messages.Add(message);
                _context.SaveChanges();

                return Json(new { success = true, message = "Mesajınız başarıyla gönderildi." });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Hata oluştu!" });
            }
        }
    }
}