using EShopper.DAL.Entities;
using EShopper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopper.BLL.Repositories
{
    public interface ISubCategoryRepository
    {
        string AddSubCategory(SubCategory subcategory);
        List<SubCategoryModel> GetAllSubCategories();
        SubCategoryModel GetSubCategoryById(Guid? subcategoryid);
        string DeleteSubCategory(Guid subcategoryid);
        List<SubCategory> GetSubCategoryByCategoryId(Guid? categoryid);
    }
}
