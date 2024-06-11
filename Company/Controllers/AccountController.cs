using Microsoft.AspNetCore.Mvc;
using Company.DataContext;
using Company.Models;
using Company.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Company.Controllers
{
    public class AccountController : Controller
    {
        private readonly CompanyContext _context;

        public AccountController(CompanyContext context)
        {
            _context = context;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Аккаунты
                    .Include(u => u.Роли)
                    .FirstOrDefaultAsync(u => u.Логин == model.Login && u.Пароль == model.Password);

                if (user != null)
                {
                    if (user.Роли != null)
                    {
                        var roleCode = user.Роли.Код_роли;

                        if (roleCode == 1)
                        {
                            return RedirectToAction("Index", "Menu");
                        }
                        else if (roleCode == 2)
                        {
                            return RedirectToAction("Index", "MenuAdmin");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Роль пользователя не определена");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Неверный логин или пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _context.Аккаунты.AnyAsync(u => u.Логин == model.Login))
                {
                    ModelState.AddModelError("Login", "Пользователь с таким логином уже существует");
                    return View(model);
                }

                if (await _context.Роли.AllAsync(r => r.Код_роли != model.RoleCode))
                {
                    ModelState.AddModelError("RoleCode", "Роль с указанным кодом не существует");
                    return View(model);
                }

                var account = new Аккаунты
                {
                    Логин = model.Login,
                    Пароль = model.Password,
                    Код_аккаунта = model.AccountCode
                };

                account.Роли = await _context.Роли.FindAsync(model.RoleCode);

                _context.Add(account);
                await _context.SaveChangesAsync();

                if (model.RoleCode == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (model.RoleCode == 2)
                {
                    return RedirectToAction("Index", "Menu");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Account/Login.cshtml");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Views/Account/Register.cshtml");
        }
        public IActionResult RegistrationSuccess()
        {
            return View("~/Views/Home/Index.cshtml");
        }
    }
}
