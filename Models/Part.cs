using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSIndexer.Models
{
    public class Part
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Car Car { get; set; }
        public Guid CarId { get; set; }
    }
}
