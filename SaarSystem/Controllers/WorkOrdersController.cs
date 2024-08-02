using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaarSystem.Data;
using SaarSystem.Models;
using System.Threading.Tasks;

namespace SaarSystem.Controllers
{
    public class WorkOrdersController : Controller
    {
        private readonly CMMSDbContext _context;

        public WorkOrdersController(CMMSDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var workOrders = _context.WorkOrders.Include(w => w.Equipment);
            return View(await workOrders.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["Equipments"] = new SelectList(_context.Equipments, "EquipmentId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EquipmentId,Description,DateCreated,Status")] WorkOrder workOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Equipments"] = new SelectList(_context.Equipments, "EquipmentId", "Name", workOrder.EquipmentId);
            return View(workOrder);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _context.WorkOrders.FindAsync(id);
            if (workOrder == null)
            {
                return NotFound();
            }
            ViewData["Equipments"] = new SelectList(_context.Equipments, "EquipmentId", "Name", workOrder.EquipmentId);
            return View(workOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkOrderId,EquipmentId,Description,DateCreated,Status")] WorkOrder workOrder)
        {
            if (id != workOrder.WorkOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkOrderExists(workOrder.WorkOrderId))
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
            ViewData["Equipments"] = new SelectList(_context.Equipments, "EquipmentId", "Name", workOrder.EquipmentId);
            return View(workOrder);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _context.WorkOrders
                .Include(w => w.Equipment)
                .FirstOrDefaultAsync(m => m.WorkOrderId == id);
            if (workOrder == null)
            {
                return NotFound();
            }

            return View(workOrder);
        }

        private bool WorkOrderExists(int id)
        {
            return _context.WorkOrders.Any(e => e.WorkOrderId == id);
        }
    }
}