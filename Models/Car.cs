using System.ComponentModel.DataAnnotations;

namespace POSIndexer.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Type { get; set; }
        [MaxLength(20)] 
        public string Color { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? SoldDate { get; set; }
        public List<Part> Parts { get; set; } = new();
    }
}