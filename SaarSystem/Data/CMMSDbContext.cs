using Microsoft.EntityFrameworkCore;
using SaarSystem.Models;

namespace SaarSystem.Data
{
    public class CMMSDbContext : DbContext
    {
        public CMMSDbContext(DbContextOptions<CMMSDbContext> options) : base(options) { }

        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<PreventiveMaintenance> PreventiveMaintenances { get; set; }
    }
}