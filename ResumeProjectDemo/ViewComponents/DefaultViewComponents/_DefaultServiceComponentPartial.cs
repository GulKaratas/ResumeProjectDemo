using Microsoft.AspNetCore.Mvc;
using ResumeProjectDemo.Context;

namespace ResumeProjectDemo.ViewComponents.DefaultViewComponents
{
    public class _DefaultServiceComponentPartial :ViewComponent
    {
        private readonly ResumeContext _context;
        public _DefaultServiceComponentPartial(ResumeContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _context.Services.ToList();
            return View(values);
        }
    }
}
//ti-timer
//ti-brush-alt
//ti-book
// ti-layers
//ti-settings
//ti-headphone-alt