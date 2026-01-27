using Microsoft.AspNetCore.Mvc;
using ResumeProjectDemo.Context;
using System.Linq;

namespace ResumeProjectDemo.Controllers
{
    public class MessageController : Controller
    {
        private readonly ResumeContext _context;
        public MessageController(ResumeContext context)
        {
            _context = context;
        }

        public IActionResult MessageList()
        {
            var values = _context.Messages.OrderByDescending(m => m.SendDate).ToList();
            return View(values);
        }

        [HttpPost]
        public IActionResult MarkAsRead(int id)
        {
            try
            {
                var message = _context.Messages.Find(id);
                if (message != null)
                {
                    message.IsRead = true;
                    _context.SaveChanges();
                    TempData["Success"] = "Mesaj okundu olarak işaretlendi!";
                }
            }
            catch
            {
                TempData["Error"] = "İşlem sırasında hata oluştu!";
            }
            return RedirectToAction("MessageList");
        }

        [HttpPost]
        public IActionResult MarkAsUnread(int id)
        {
            try
            {
                var message = _context.Messages.Find(id);
                if (message != null)
                {
                    message.IsRead = false;
                    _context.SaveChanges();
                    TempData["Success"] = "Mesaj okunmadı olarak işaretlendi!";
                }
            }
            catch
            {
                TempData["Error"] = "İşlem sırasında hata oluştu!";
            }
            return RedirectToAction("MessageList");
        }

        [HttpPost]
        public IActionResult DeleteMessage(int id)
        {
            try
            {
                var message = _context.Messages.Find(id);
                if (message != null)
                {
                    _context.Messages.Remove(message);
                    _context.SaveChanges();
                    TempData["Success"] = "Mesaj başarıyla silindi!";
                }
            }
            catch
            {
                TempData["Error"] = "Mesaj silinirken hata oluştu!";
            }
            return RedirectToAction("MessageList");
        }
    }
}
