using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopper.DAL.Entities
{
    public class SubCategory
    {
        public Guid SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Boolean IsActive { get; set; }
    }
}
