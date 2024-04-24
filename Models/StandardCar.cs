using System;
using System.Collections.Generic;

namespace POSIndexer.Models
{
    public class StandardCar
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Quantity { get; set; }
        public string Type { get; set; }
        public string Color { get; set; } = "";
        public string Battery { get; set; } = "";
        public bool Hitch;
    }
}
