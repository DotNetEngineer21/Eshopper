using EShopper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopper.BLL.Repositories
{
    public interface ICategoryRepository
    {
        string AddCategory(Category category);
        List<Category> GetAllCategories();
        Category GetCategoryById(Guid? categoryid);
        string DeleteCategory(Guid categoryid);
        //string EditCategory(Category category);
    }
}
