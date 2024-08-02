namespace SaarSystem.Models
{
    public class WorkOrder
    {
        public int WorkOrderId { get; set; }
        public int EquipmentId { get; set; }
        public string? Description { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string? Status { get; set; }

        public Equipment Equipment { get; set; }
    }
}