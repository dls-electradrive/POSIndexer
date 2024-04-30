namespace POSIndexer.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string Battery { get; set; }
        public bool Hitch { get; set; }
        public bool IsAvailable { get; set; }
        public List<Part> Parts { get; set; } = new();
    }
}