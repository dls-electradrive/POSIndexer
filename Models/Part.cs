using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSIndexer.Models
{
    public class Part
    {
        public Guid PartId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid CarId { get; set; }
    }
}
