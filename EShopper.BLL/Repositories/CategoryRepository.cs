using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShopper.DAL.Entities;
using EShopper.DAL.Context;
using System.Data.Entity;

namespace EShopper.BLL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public string AddCategory(Category category)
        {
            string status = string.Empty;
            try
            {
                using (Context db = new Context())
                {
                    Category obj = new Category();
                    if (category.CategoryId != Guid.Empty)
                    {
                        obj.CategoryId = category.CategoryId;
                        obj.CategoryName = category.CategoryName;
                        category.Created = obj.Created;
                        category.Updated = DateTime.Now;
                        db.Entry(category).State = EntityState.Modified;
                    }
                    else
                    {
                        obj.CategoryId = Guid.NewGuid();
                        obj.CategoryName = category.CategoryName;
                        obj.Created = DateTime.Now;
                        obj.Updated = DateTime.Now;
                        db.Categories.Add(obj);
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
        public List<Category> GetAllCategories()
        {
            try
            {
                using (Context db = new Context())
                {
                    return db.Categories.ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //public string EditCategory(Category category)
        //{
        //    try
        //    {
        //        using (Context db = new Context())
        //        {
        //            Category obj = GetCategoryById(category.CategoryId);
        //            category.Created = obj.Created;
        //            category.Updated = DateTime.Now;
        //            db.Entry(category).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return "Success";
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        return "Failure";
        //    }
        //}
        public Category GetCategoryById(Guid? categoryid)
        {
            try
            {
                using (Context db = new Context())
                {
                    return db.Categories.Find(categoryid);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string DeleteCategory(Guid categoryid)
        {
            try
            {
                using (Context db = new Context())
                {

                    Category category = db.Categories.Find(categoryid);
                    db.Categories.Remove(category);
                    db.SaveChanges();
                    return "Success";
                }
            }
            catch (Exception ex)
            {
                return "Failure";
            }
        }      
    }
}

