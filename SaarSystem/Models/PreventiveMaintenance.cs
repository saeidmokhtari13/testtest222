namespace SaarSystem.Models
{
    public class PreventiveMaintenance
    {
        public int PreventiveMaintenanceId { get; set; }
        public int EquipmentId { get; set; }
        public DateTime MaintenanceDate { get; set; } = DateTime.Now;
        public string? Description { get; set; }

        public Equipment Equipment { get; set; }
    }
}