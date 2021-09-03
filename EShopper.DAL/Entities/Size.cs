using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopper.DAL.Entities
{
    public class Sizes
    {
        [Key]
        public Guid SizeId { get; set; }
        public string Size { get; set; }
        public Guid ProductId { get; set; }        

    }
}
