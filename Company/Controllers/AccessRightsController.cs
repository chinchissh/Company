using Microsoft.AspNetCore.Mvc;
using Company.DataContext;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Company.Controllers
{
    public class AccessRightsController : Controller
    {
        private readonly CompanyContext _context;

        public AccessRightsController(CompanyContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var accessRights = await _context.Права_доступа.Include(a => a.Роли).ToListAsync();
            return View(accessRights);
        }
    }
}
