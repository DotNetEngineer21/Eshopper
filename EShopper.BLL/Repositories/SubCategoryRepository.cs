using EShopper.DAL.Context;
using EShopper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EShopper.Models;
using System.Text;
using System.Threading.Tasks;

namespace EShopper.BLL.Repositories
{
    public class SubCategoryRepository:ISubCategoryRepository
    {
        public string AddSubCategory(SubCategory subcategory)
        {
            string status = string.Empty;
            try
            {
                using (Context db = new Context())
                {
                    SubCategory obj = new SubCategory();
                    if (subcategory.SubCategoryId != Guid.Empty)
                    {
                        obj.SubCategoryId = subcategory.SubCategoryId;
                        obj.SubCategoryName = subcategory.SubCategoryName;
                        obj.CategoryId = subcategory.CategoryId;
                        obj.Created = subcategory.Created;
                        obj.Updated = DateTime.Now;
                        obj.IsActive = true;
                        db.Entry(obj).State = EntityState.Modified;
                    }
                    else
                    {
                        obj.SubCategoryId = Guid.NewGuid();
                        obj.SubCategoryName = subcategory.SubCategoryName;
                        obj.CategoryId = subcategory.CategoryId;
                        obj.Created = DateTime.Now;
                        obj.Updated = DateTime.Now;
                        obj.IsActive = true;
                        db.SubCategories.Add(obj);
                    }
                    db.SaveChanges();
                    return status = "Success";
                }
            }
            catch (Exception ex)
            {
                return status = "Failure";
            }
        }
        public List<SubCategoryModel> GetAllSubCategories()
        {
            try
            {
                using (Context db = new Context())
                {
                    var subcategories = db.SubCategories.Where(p => p.IsActive == true).ToList();
                    List<SubCategoryModel> productlist = new List<SubCategoryModel>();
                    foreach (var item in subcategories)
                    {
                        var query = db.Categories.Where(c => c.CategoryId == item.CategoryId).FirstOrDefault();
                        SubCategoryModel subcategory = new SubCategoryModel();
                        subcategory.SubCategoryId = item.SubCategoryId;
                        subcategory.SubCategoryName = item.SubCategoryName;
                        subcategory.CategoryId = item.CategoryId;
                        subcategory.CategoryName = query.CategoryName;
                        subcategory.Created = item.Created;
                        subcategory.Updated = item.Updated;
                        subcategory.IsActive = item.IsActive;
                        productlist.Add(subcategory);
                    }
                    return productlist;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public SubCategoryModel GetSubCategoryById(Guid? subcategoryid)
        {
            try
            {
                using (Context db = new Context())
                {
                    var subcategory = db.SubCategories.Find(subcategoryid);
                    SubCategoryModel objmodel = new SubCategoryModel();
                    objmodel.SubCategoryId = subcategory.SubCategoryId;
                    objmodel.SubCategoryName = subcategory.SubCategoryName;
                    objmodel.Created = subcategory.Created;
                    objmodel.IsActive = subcategory.IsActive;
                    objmodel.CategoryId = subcategory.CategoryId;
                    objmodel.Updated = DateTime.Now;
                    return objmodel;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string DeleteSubCategory(Guid subcategoryid)
        {
            try
            {
                using (Context db = new Context())
                {

                    SubCategory subcategory = db.SubCategories.Find(subcategoryid);
                    db.SubCategories.Remove(subcategory);
                    db.SaveChanges();
                    return "Success";
                }
            }
            catch (Exception ex)
            {
                return "Failure";
            }
        }
        public List<SubCategory> GetSubCategoryByCategoryId(Guid? categoryid)
        {
            try
            {
                using (Context db = new Context())
                {
                    List<SubCategory> subcategory = db.SubCategories.Where(p => p.IsActive == true&&p.CategoryId==categoryid).ToList();
                    return subcategory;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
