namespace POSIndexer.Models
{
    public class CustomCar
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Quantity { get; set; }
        public string Type { get; set; }
        public string Color { get; set; } = "";
        public string Battery { get; set; } = "";
        public bool Hitch;
        public List<PartType> Parts { get; set; } = new List<PartType>();
    }
}