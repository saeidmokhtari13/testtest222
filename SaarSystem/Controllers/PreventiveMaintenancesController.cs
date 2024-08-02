using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaarSystem.Data;
using SaarSystem.Models;
using System.Threading.Tasks;

namespace SaarSystem.Controllers
{
    public class PreventiveMaintenancesController : Controller
    {
        private readonly CMMSDbContext _context;

        public PreventiveMaintenancesController(CMMSDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var preventiveMaintenances = _context.PreventiveMaintenances.Include(p => p.Equipment);
            return View(await preventiveMaintenances.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["Equipments"] = new SelectList(_context.Equipments, "EquipmentId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EquipmentId,MaintenanceDate,Description")] PreventiveMaintenance preventiveMaintenance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(preventiveMaintenance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Equipments"] = new SelectList(_context.Equipments, "EquipmentId", "Name", preventiveMaintenance.EquipmentId);
            return View(preventiveMaintenance);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preventiveMaintenance = await _context.PreventiveMaintenances.FindAsync(id);
            if (preventiveMaintenance == null)
            {
                return NotFound();
            }
            ViewData["Equipments"] = new SelectList(_context.Equipments, "EquipmentId", "Name", preventiveMaintenance.EquipmentId);
            return View(preventiveMaintenance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PreventiveMaintenanceId,EquipmentId,MaintenanceDate,Description")] PreventiveMaintenance preventiveMaintenance)
        {
            if (id != preventiveMaintenance.PreventiveMaintenanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(preventiveMaintenance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreventiveMaintenanceExists(preventiveMaintenance.PreventiveMaintenanceId))
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
            ViewData["Equipments"] = new SelectList(_context.Equipments, "EquipmentId", "Name", preventiveMaintenance.EquipmentId);
            return View(preventiveMaintenance);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preventiveMaintenance = await _context.PreventiveMaintenances
                .Include(p => p.Equipment)
                .FirstOrDefaultAsync(m => m.PreventiveMaintenanceId == id);
            if (preventiveMaintenance == null)
            {
                return NotFound();
            }

            return View(preventiveMaintenance);
        }

        private bool PreventiveMaintenanceExists(int id)
        {
            return _context.PreventiveMaintenances.Any(e => e.PreventiveMaintenanceId == id);
        }
    }
}