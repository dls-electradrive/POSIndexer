using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSIndexer.Models
{
    public class Part
    {
        [Key]
        [Column("Part_Id")]
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Car Car { get; set; }
        [Column("Car_Id")]
        public Guid CarId { get; set; }
    }
}
