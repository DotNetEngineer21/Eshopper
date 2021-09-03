using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopper.DAL.Entities
{
    public class Colors
    {
        [Key]
        public Guid ColorId { get; set; }
        public string Color { get; set; }
        public Guid ProductId { get; set; }
    }
}
