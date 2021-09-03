using EShopper.DAL.Context;
using EShopper.DAL.Entities;
using EShopper.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopper.BLL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public Guid AddProduct(ProductModel product)
        {
            string status = string.Empty;
            try
            {
                using (Context db = new Context())
                {
                    Product obj = new Product();
                    if (product.ProductId != Guid.Empty)
                    {
                        obj.ProductId = product.ProductId;
                        obj.ProductName = product.ProductName;
                        obj.SubCategoryId = product.SubCategoryId;
                        obj.Price = product.Price;
                        obj.IsActive = true;
                        obj.Created = product.Created;
                        obj.Updated = DateTime.Now;
                        obj.Description = product.Description;
                        obj.Color = product.Color;
                        //obj.Size = product.Size;
                        db.Entry(obj).State = EntityState.Modified;

                    }
                    else
                    {
                        obj.ProductId = Guid.NewGuid();
                        obj.ProductName = product.ProductName;
                        obj.SubCategoryId = product.SubCategoryId;
                        obj.Price = product.Price;
                        obj.IsActive = true;
                        obj.Created = DateTime.Now;
                        obj.Updated = DateTime.Now;
                        obj.Description = product.Description;
                        //obj.Color = product.Color;
                        //obj.Size = product.Size;
                        db.Products.Add(obj);

                        string[] colors= product.Color.Split(',');
                        foreach (var item in colors) {
                            Colors color = new Colors();
                            color.ColorId = Guid.NewGuid();
                            color.Color =item;
                            color.ProductId = obj.ProductId;
                            db.Colors.Add(color);
                        }

                        if (product.SizeXS == "on") {
                            Sizes size = new Sizes();
                            size.SizeId = Guid.NewGuid();
                            size.Size = "XS";
                            size.ProductId = obj.ProductId;
                            db.Sizes.Add(size);
                        }
                        if (product.SizeS == "on")
                        {
                            Sizes size = new Sizes();
                            size.SizeId = Guid.NewGuid();
                            size.Size = "S";
                            size.ProductId = obj.ProductId;
                            db.Sizes.Add(size);
                        }
                        if (product.SizeM == "on")
                        {
                            Sizes size = new Sizes();
                            size.SizeId = Guid.NewGuid();
                            size.Size = "M";
                            size.ProductId = obj.ProductId;
                            db.Sizes.Add(size);
                        }
                        if (product.SizeL == "on")
                        {
                            Sizes size = new Sizes();
                            size.SizeId = Guid.NewGuid();
                            size.Size = "L";
                            size.ProductId = obj.ProductId;
                            db.Sizes.Add(size);
                        }
                        if (product.SizeXL == "on")
                        {
                            Sizes size = new Sizes();
                            size.SizeId = Guid.NewGuid();
                            size.Size = "XL";
                            size.ProductId = obj.ProductId;
                            db.Sizes.Add(size);
                        }
                    }
                    db.SaveChanges();
                    return obj.ProductId;
                }
            }
            catch (Exception ex)
            {
                return Guid.Empty;
            }
        }
        public List<ProductModel> GetAllProducts()
        {
            try
            {
                using (Context db = new Context())
                {
                    var products = db.Products.Where(p => p.IsActive == true).ToList();
                    List<ProductModel> productlist = new List<ProductModel>();
                    foreach (var item in products)
                    {
                        var query = db.SubCategories.Where(s => s.SubCategoryId == item.SubCategoryId).FirstOrDefault();

                        ProductModel product = new ProductModel();
                        product.ProductId = item.ProductId;
                        product.ProductName = item.ProductName;
                        product.SubCategoryId = item.SubCategoryId;
                        product.SubCategoryName = query.SubCategoryName;                //commented for later use
                        product.Price = item.Price;
                        product.IsActive = item.IsActive;
                        product.Description = item.Description;
                       // product.Color = item.Color;
                       // product.Size = item.Size;       //commented for later use
                        productlist.Add(product);
                    }
                    return productlist;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //public string EditProduct(Product product)
        //{
        //    try
        //    {
        //        using (Context db = new Context())
        //        {
        //            Product obj = GetProductById(product.ProductId);
        //            obj.ProductName = product.ProductName;
        //            obj.CategoryId = product.CategoryId;
        //            obj.Price = product.Price;
        //            obj.IsActive = true;
        //            obj.Created = obj.Created;
        //            obj.Updated = DateTime.Now;
        //            db.Entry(obj).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return "Success";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Failure";
        //    }
        //}
        public ProductModel GetProductById(Guid? productid)
        {
            try
            {
                using (Context db = new Context())
                {
                    var product = db.Products.Find(productid);
                    ProductModel objmodel = new ProductModel();
                    objmodel.ProductId = product.ProductId;
                    objmodel.ProductName = product.ProductName;
                    objmodel.Price = product.Price;
                    objmodel.IsActive = product.IsActive;
                    objmodel.SubCategoryId = product.SubCategoryId;
                    objmodel.Created = product.Created;
                    objmodel.Description = product.Description;
                    objmodel.Color = product.Color;
                    // objmodel.Size = product.Size;         //for later use
                    objmodel.SizeList = db.Sizes.Where(s => s.ProductId == product.ProductId).ToList();
                    objmodel.ColorList = db.Colors.Where(c => c.ProductId == product.ProductId).ToList();
                    return objmodel;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string DeleteProduct(Guid productid)
        {
            try
            {
                using (Context db = new Context())
                {

                    Product product = db.Products.Find(productid);
                    db.Products.Remove(product);
                    db.SaveChanges();
                    return "Success";
                }
            }
            catch (Exception ex)
            {
                return "Failure";
            }
        }
        public List<Product> Getproducts()
        {
            try
            {
                using (Context db = new Context())
                {
                    //List<Product> productlist = db.Products.GroupBy(p => p.ProductName).Select(o => new Product { ProductId = new Guid(o.Key)}).ToList();
                    //List<Product> productlist = db.Products.GroupBy(u => u.ProductName).Select(grp => grp).ToList();
                    List<Product> productlist = db.Products.Select(o => new { objectType = o.ProductName, object_id = o.ProductId })
    .GroupBy(fl => fl.objectType).ToList()
    .Select(fl => new Product { ProductName = fl.Select(x => x.objectType).FirstOrDefault() })
    .ToList();
                    return productlist;
                }
            }
            catch (Exception ex) { return null; }
        }
        public List<SubCategory> GetproductsforMen()
        {
            try
            {
                using (Context db = new Context())
                {
                    //Guid categoryid = Guid.Parse("E426EC3F-7E0E-469B-9BD5-549171EA7C79");
                    Category category=db.Categories.Where(c => c.CategoryName == "Men").FirstOrDefault();
                    //List<Product> productlist = db.Products.Where(x => x.SubCategoryId == categoryid).Select(o => new { objectType = o.ProductName, object_id = o.ProductId })
                    //                            .GroupBy(fl => fl.objectType).ToList()
                    //                            .Select(fl => new Product
                    //                            {
                    //                                ProductName = fl.Select(x => x.objectType).FirstOrDefault(),
                    //                                ProductId = fl.Select(x => x.object_id).FirstOrDefault()
                    //                            })
                    //                            .ToList();
                    List<SubCategory> subcategorylist = db.SubCategories.Where(x => x.CategoryId == category.CategoryId).Select(o => new { objectType = o.SubCategoryName, object_id = o.SubCategoryId })
                                            .GroupBy(fl => fl.objectType).ToList()
                                            .Select(fl => new SubCategory
                                            {
                                                SubCategoryName = fl.Select(x => x.objectType).FirstOrDefault(),
                                                SubCategoryId = fl.Select(x => x.object_id).FirstOrDefault()
                                            })
                                            .ToList();
                    return subcategorylist;
                }
            }
            catch (Exception ex) { return null; }
        }
        public List<SubCategory> GetproductsforWomen()
        {
            try
            {
                using (Context db = new Context())
                {
                    Category category = db.Categories.Where(c => c.CategoryName == "Women").FirstOrDefault();
                    List<SubCategory> subcategorylist = db.SubCategories.Join(db.Products,p=>p.SubCategoryId,s=>s.SubCategoryId
               , (s, p) => new {
                   s.SubCategoryId,
                   s.SubCategoryName,
                   p.ProductId,
                   s.CategoryId
               })
                        .Where(x => x.CategoryId == category.CategoryId).Select(o => new { objectType = o.SubCategoryName, object_id = o.SubCategoryId })
                                           .GroupBy(fl => fl.objectType).ToList()
                                           .Select(fl => new SubCategory
                                           {
                                               SubCategoryName = fl.Select(x => x.objectType).FirstOrDefault(),
                                               SubCategoryId = fl.Select(x => x.object_id).FirstOrDefault()
                                           })
                                           .ToList();
                    return subcategorylist;
                }
            }
            catch (Exception ex) { return null; }
        }
        public int GetHighestPrice()
        {
            try
            {
                using (Context db = new Context())
                {
                    //Guid categoryid = Guid.Parse("C70CA757-DF5E-47B2-A69D-A964E5A593B4");
                    var price = db.Products.ToList().Max(x => x.Price);
                    return Convert.ToInt32(price);
                }
            }
            catch (Exception ex) { return 0; }
        }
        public List<Colors> GetAllColor()
        {
            try
            {
                using (Context db = new Context())
                {
                    List<Colors> colorList = db.Colors.Join(db.Products, c => c.ProductId, p => p.ProductId
               , (c, p) => new {
                   c.ColorId,
                   c.Color,
                   p.ProductId
               })
                        .Select(o => new { objectType = o.Color, object_id = o.ColorId })
                                           .GroupBy(fl => fl.objectType).ToList()
                                           .Select(fl => new Colors
                                           {
                                               Color = fl.Select(x => x.objectType).FirstOrDefault(),
                                               ColorId = fl.Select(x => x.object_id).FirstOrDefault()
                                           })
                                           .ToList();
                    return colorList;
                }
            }
            catch (Exception ex) { return null; }
        }
        public List<Sizes> GetAllSizes()
        {
            try
            {
                using (Context db = new Context())
                {
                    List<Sizes> sizeList = db.Sizes.Select(o => new { objectType = o.Size, object_id = o.SizeId })
                                           .GroupBy(fl => fl.objectType).ToList()
                                           .Select(fl => new Sizes
                                           {
                                               Size = fl.Select(x => x.objectType).FirstOrDefault(),
                                               SizeId = fl.Select(x => x.object_id).FirstOrDefault()
                                           })
                                           .ToList();
                    return sizeList;
                }
            }
            catch (Exception ex) { return null; }
        }
        public List<ProductModel> GetFilteredProducts(string sortby, string category, string product, string startprice, string endprice,string color,string size)
        {
            try
            {
                using (Context db = new Context())
                {
                    List<ProductModel> productlist = new List<ProductModel>();
                    decimal sprice = decimal.Parse(startprice);
                    decimal eprice = decimal.Parse(endprice);
                    var query = (dynamic)null;
                    switch (sortby)
                    {
                        case "Latest":
                            if ((category == "All") && (product == "All"))
                            {

                                //change query according to subcategory
                                query = (from p in db.Products
                                         join c in db.Categories on p.SubCategoryId equals c.CategoryId into cl
                                         from clt in cl.DefaultIfEmpty()
                                         where p.IsActive == true
                                         orderby p.Created
                                         select new { p, clt }).ToList();
                            }
                            else
                            {
                                //change query acc to sub categoryid 
                                query = (from p in db.Products
                                         join c in db.Categories on p.SubCategoryId equals c.CategoryId into cl
                                         from clt in cl.DefaultIfEmpty()
                                         where ((category.ToLower() == "all" ? "" : clt.CategoryName.ToLower()) == (category.ToLower() == "all" ? "" : category.ToLower())) && ((product.ToLower() == "all" ? "" : p.ProductName.ToLower()) == (product.ToLower() == "all" ? "" : product.ToLower())) && p.IsActive == true
                                         orderby p.Created
                                         select new { p, clt }).ToList();
                            }
                            break;
                        case "High to Low Price":
                            if ((category == "All") && (product == "All"))
                            {
                                // change query acc to subcategoryid
                                query = (from p in db.Products
                                         join c in db.Categories on p.SubCategoryId equals c.CategoryId into cl
                                         from clt in cl.DefaultIfEmpty()
                                         where p.IsActive == true
                                         orderby p.Price descending
                                         select new { p, clt }).ToList();
                            }
                            else
                            {
                                //change to subcategoryid
                                query = (from p in db.Products
                                         join c in db.Categories on p.SubCategoryId equals c.CategoryId into cl
                                         from clt in cl.DefaultIfEmpty()
                                         where ((category.ToLower() == "all" ? "" : clt.CategoryName.ToLower()) == (category.ToLower() == "all" ? "" : category.ToLower())) && ((product.ToLower() == "all" ? "" : p.ProductName.ToLower()) == (product.ToLower() == "all" ? "" : product.ToLower())) && p.IsActive == true
                                         orderby p.Price descending
                                         select new { p, clt }).ToList();
                            }
                            break;
                        case "Low to High Price":
                            if ((category == "All") && (product == "All"))
                            {
                                //change to subcategoryid
                                query = (from p in db.Products
                                         join c in db.Categories on p.SubCategoryId equals c.CategoryId into cl
                                         from clt in cl.DefaultIfEmpty()
                                         where p.IsActive == true
                                         orderby p.Price ascending
                                         select new { p, clt }).ToList();
                            }
                            else
                            {
                                //change to subcategory id 
                                query = (from p in db.Products
                                         join c in db.Categories on p.SubCategoryId equals c.CategoryId into cl
                                         from clt in cl.DefaultIfEmpty()
                                         where ((category.ToLower() == "all" ? "" : clt.CategoryName.ToLower()) == (category.ToLower() == "all" ? "" : category.ToLower())) && ((product.ToLower() == "all" ? "" : p.ProductName.ToLower()) == (product.ToLower() == "all" ? "" : product.ToLower())) && p.IsActive == true
                                         orderby p.Price ascending
                                         select new { p, clt }).ToList();
                            }
                            break;
                        case "Default Sorting":
                            if ((category == "All") && (product == "All") && (sprice == 0) && (eprice == 0) && (color=="")&&(size==""))
                            {
                                //change to subcategory id 
                                query = (from p in db.Products
                                         join s in db.SubCategories on p.SubCategoryId equals s.SubCategoryId into sl
                                         from slt in sl.DefaultIfEmpty()
                                         join ct in db.Categories on slt.CategoryId equals ct.CategoryId into ctl
                                         from ctt in ctl.DefaultIfEmpty()
                                         where p.IsActive == true
                                         select new { p, slt ,ctt}).ToList();
                            }
                            else if (size == "")
                            {
                                query = (from p in db.Products
                                         join s in db.SubCategories on p.SubCategoryId equals s.SubCategoryId into sl
                                         from slt in sl.DefaultIfEmpty()
                                         join c in db.Colors on p.ProductId equals c.ProductId into cl
                                         from clt in cl.DefaultIfEmpty()
                                         where ((product.ToLower() == "all" ? "" : slt.SubCategoryName.ToLower()) == (product.ToLower() == "all" ? "" : product.ToLower())) && ((sprice == 0 ? 0 : p.Price) >= (sprice == 0 ? 0 : sprice)) && ((eprice == 0 ? 0 : p.Price) <= (eprice == 0 ? 0 : eprice)) && ((color == "" ? "" : clt.Color) == (color == "" ? "" : color)) && p.IsActive == true
                                         select new { p, slt }).Distinct().ToList();
                            }
                            else if (color == "")
                            {
                                query = (from p in db.Products
                                         join s in db.SubCategories on p.SubCategoryId equals s.SubCategoryId into sl
                                         from slt in sl.DefaultIfEmpty()
                                         join sz in db.Sizes on p.ProductId equals sz.ProductId into szl
                                         from szlt in szl.DefaultIfEmpty()
                                         where ((product.ToLower() == "all" ? "" : slt.SubCategoryName.ToLower()) == (product.ToLower() == "all" ? "" : product.ToLower())) && ((sprice == 0 ? 0 : p.Price) >= (sprice == 0 ? 0 : sprice)) && ((eprice == 0 ? 0 : p.Price) <= (eprice == 0 ? 0 : eprice)) && ((size.ToLower() == "" ? "" : szlt.Size.ToLower()) == (size.ToLower() == "" ? "" : size.ToLower())) && p.IsActive == true
                                         select new { p, slt }).Distinct().ToList();
                            }
                            else
                            {
                                query = (from p in db.Products
                                         join s in db.SubCategories on p.SubCategoryId equals s.SubCategoryId into sl
                                         from slt in sl.DefaultIfEmpty()
                                         join c in db.Colors on p.ProductId equals c.ProductId into cl
                                         from clt in cl.DefaultIfEmpty()
                                         join sz in db.Sizes on p.ProductId equals sz.ProductId into szl
                                         from szlt in szl.DefaultIfEmpty()
                                         where ((product.ToLower() == "all" ? "" : slt.SubCategoryName.ToLower()) == (product.ToLower() == "all" ? "" : product.ToLower())) &&  ((sprice == 0 ? 0 : p.Price) >= (sprice == 0 ? 0 : sprice)) && ((eprice == 0 ? 0 : p.Price) <= (eprice == 0 ? 0 : eprice)) && ((color == "" ? "" : clt.Color) == (color == "" ? "" : color))&&((size.ToLower() == "" ? "" : szlt.Size.ToLower()) == (size.ToLower() == "" ? "" : size.ToLower())) && p.IsActive == true
                                         select new { p, slt }).ToList();
                            }
                                break;
                    }

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ProductModel objproduct = new ProductModel();
                            objproduct.ProductId = item.p.ProductId;
                            objproduct.ProductName = item.p.ProductName;
                            objproduct.SubCategoryId = item.p.SubCategoryId;
                            objproduct.SubCategoryName = item.slt.SubCategoryName;
                            objproduct.Price = item.p.Price;
                            objproduct.Created = item.p.Created;
                            objproduct.IsActive = item.p.IsActive;
                            objproduct.Description = item.p.Description;
                            objproduct.Color = item.p.Color;
                          //  objproduct.Size = item.p.Size;                        //commented for later use
                            productlist.Add(objproduct);
                        }
                    }
                    return productlist;
                }
            }
            catch (Exception ex) { return null; }
        }
    }
}
