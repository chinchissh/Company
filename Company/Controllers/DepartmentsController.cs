﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Company.DataContext;
using Company.Models;

namespace Company.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly CompanyContext _context;

        public DepartmentsController(CompanyContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var companyContext = _context.Отделы.Include(о => о.Организации);
            return View(await companyContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var отделы = await _context.Отделы
                .Include(о => о.Организации)
                .FirstOrDefaultAsync(m => m.Код_отдела == id);
            if (отделы == null)
            {
                return NotFound();
            }

            return View(отделы);
        }

        public IActionResult Create()
        {
            ViewData["Код_организации"] = new SelectList(_context.Организации, "Код_организации", "Код_организации");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Код_отдела,Название_отдела,Код_организации")] Отделы отделы)
        {
            if (ModelState.IsValid)
            {
                _context.Add(отделы);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Код_организации"] = new SelectList(_context.Организации, "Код_организации", "Код_организации", отделы.Код_организации);
            return View(отделы);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var отделы = await _context.Отделы.FindAsync(id);
            if (отделы == null)
            {
                return NotFound();
            }
            ViewData["Код_организации"] = new SelectList(_context.Организации, "Код_организации", "Код_организации", отделы.Код_организации);
            return View(отделы);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Код_отдела,Название_отдела,Код_организации")] Отделы отделы)
        {
            if (id != отделы.Код_отдела)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(отделы);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ОтделыExists(отделы.Код_отдела))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Код_организации"] = new SelectList(_context.Организации, "Код_организации", "Код_организации", отделы.Код_организации);
            return View(отделы);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var отделы = await _context.Отделы
                .Include(о => о.Организации)
                .FirstOrDefaultAsync(m => m.Код_отдела == id);
            if (отделы == null)
            {
                return NotFound();
            }

            return View(отделы);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var отделы = await _context.Отделы.FindAsync(id);
            if (отделы != null)
            {
                _context.Отделы.Remove(отделы);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ОтделыExists(int id)
        {
            return _context.Отделы.Any(e => e.Код_отдела == id);
        }
    }
}
