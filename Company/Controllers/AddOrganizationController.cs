using Microsoft.AspNetCore.Mvc;
using Company.Models;
using System.Threading.Tasks;
using Company.DataContext;
using Company.ViewModels;

namespace Company.Controllers
{
    public class AddOrganizationController : Controller
    {
        private readonly CompanyContext _context;

        public AddOrganizationController(CompanyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new AddOrganizationViewModel(); 
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrganization(AddOrganizationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var организации = new Организации
                {
                    Название_организации = viewModel.Название_организации,
                    ИНН = viewModel.ИНН,
                    КПП = viewModel.КПП,
                    ОГРН = viewModel.ОГРН,
                    Код = viewModel.Код,
                    Тип_организации = viewModel.Тип_организации,
                    Дата_закрытия = viewModel.Дата_закрытия,
                    Код_родительской_организации = viewModel.Код_родительской_организации,
                    Код_организации = viewModel.Код_организации,
                    Код_статуса = viewModel.Код_статуса
                };

                _context.Add(организации);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Index", viewModel);
        }
    }
}
