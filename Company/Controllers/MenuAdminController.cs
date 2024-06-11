using Microsoft.AspNetCore.Mvc;

namespace Company.Controllers
{
    public class MenuAdminController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string page)
        {
            if (!string.IsNullOrEmpty(page))
            {
                return Redirect(page);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}