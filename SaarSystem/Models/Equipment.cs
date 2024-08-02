namespace SaarSystem.Models
{
    public class Equipment
    {
        public int EquipmentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public DateTime InstallationDate { get; set; } = DateTime.Now;
        public string Location { get; set; } = string.Empty;
        public string Status { get; set; } = "Active";
    }
}