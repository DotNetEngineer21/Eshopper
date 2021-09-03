using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EShopper.DAL.Entities;
using EShopper.BLL.Repositories;
using EShopper.Models;
using System.Web.Helpers;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace EShopper.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddCategory(Guid? id)
        {
            if (id == Guid.Empty)
            {
                return View();
            }
            else
            {
                ICategoryRepository obj = new CategoryRepository();
                Category category = obj.GetCategoryById(id);
                if (category != null)
                {
                    return View(category);
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            ICategoryRepository obj = new CategoryRepository();
            string status = obj.AddCategory(category);
            return RedirectToAction("AddCategory", "Admin");
        }
        [HttpGet]
        public ActionResult GetCategories()
        {
            ICategoryRepository obj = new CategoryRepository();
            List<Category> categorylist = obj.GetAllCategories();
            return View(categorylist);
        }
        [HttpGet]
        public ActionResult DeleteCategory(Guid id)
        {
            ICategoryRepository obj = new CategoryRepository();
            string result = obj.DeleteCategory(id);
            return RedirectToAction("Getcategories");
        }
        //[HttpPost]
        //public ActionResult EditCategory(Category category)
        //{
        //    ICategoryRepository obj = new CategoryRepository();
        //    string status = obj.EditCategory(category);
        //    return RedirectToAction("Getcategories");
        //}
        [HttpGet]
        public ActionResult AddProduct(Guid? id)
        {
            if (id == null)
            {
                ICategoryRepository obj = new CategoryRepository();
                ISubCategoryRepository objsub = new SubCategoryRepository();
                ProductModel products = new ProductModel();
                products.CategoryList = obj.GetAllCategories();
                products.SubCategoryList = objsub.GetAllSubCategories();
                return View(products);
            }
            else
            {
                ICategoryRepository obj = new CategoryRepository();
                ISubCategoryRepository objsub = new SubCategoryRepository();
                ProductModel products = new ProductModel();
                IProductRepository objproduct = new ProductRepository();
                products = objproduct.GetProductById(id);
                products.CategoryList = obj.GetAllCategories();
                products.SubCategoryList = objsub.GetAllSubCategories();
                return View(products);
            }
        }
        
        public class Size
        {
            public Size(int width, int height)
            {
                Width = width;
                Height = height;
            }
            public int Width { get; set; }
            public int Height { get; set; }
        }

        public static bool ResizeImage(string orgFile, string resizedFile, ImageFormat format, int width, int height)
        {
            try
            {
                using (Image img = Image.FromFile(orgFile))
                {
                    Image thumbNail = new Bitmap(width, height, img.PixelFormat);
                    Graphics g = Graphics.FromImage(thumbNail);
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Rectangle rect = new Rectangle(0, 0, width, height);
                    g.DrawImage(img, rect);
                    thumbNail.Save(resizedFile, format);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        [HttpPost]
        public ActionResult AddProduct(ProductModel product)
        {
            ICategoryRepository obj = new CategoryRepository();
            //ViewBag.Categories = obj.GetAllCategories();
            IProductRepository objproduct = new ProductRepository();

            Guid result = objproduct.AddProduct(product);
            if (result != Guid.Empty)
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {

                        var content = new byte[file.ContentLength];
                        file.InputStream.Read(content, 0, file.ContentLength);


                        WebImage image = new WebImage(file.InputStream);
                        //WebImage img = new WebImage(file.InputStream);
                        //if (img.Width > 1000)
                        //    img.Resize(1000, 1000);
                        //img.Save("path");
                        //return View();
                        if (image.Height >= 0)
                        {
                            if (image.Width >= 0)
                            {
                                //image.Resize(242, 357, false, true);
                                //image.Resize(width: 242, height: 357, preserveAspectRatio: true,preventEnlarge: true);
                                image.FileName = result.ToString();
                                //image.Resize(293, 349, true, true);
                                image.Resize(200, 200);
                                string fileName = Path.GetFileName(image.FileName);
                                string path = System.IO.Path.Combine(Server.MapPath("~/assets/images/ProductImages"), fileName+".jpeg");
                                //image.Save(path);
                                ResizeImage(file.FileName,path, ImageFormat.Jpeg, 293, 349);
                            }
                        }
                    }
                }
            }
            return RedirectToAction("GetProducts");
        }
        [HttpGet]
        public ActionResult GetProducts()
        {
            IProductRepository obj = new ProductRepository();
            List<ProductModel> productlist = obj.GetAllProducts();
            return View(productlist);
        }
        //[HttpPost]
        //public ActionResult EditProduct(Product product)
        //{
        //    IProductRepository obj = new ProductRepository();
        //    string status = obj.EditProduct(product);
        //    return RedirectToAction("GetProducts");
        //}
        [HttpGet]
        public ActionResult DeleteProduct(Guid id)
        {
            IProductRepository obj = new ProductRepository();
            string result = obj.DeleteProduct(id);
            return RedirectToAction("GetProducts");
        }
        [HttpGet]
        public ActionResult AddSubCategory(Guid? id)
        {
            if (id == null)
            {
                ICategoryRepository obj = new CategoryRepository();
                SubCategoryModel subcategories = new SubCategoryModel();
                subcategories.CategoryList = obj.GetAllCategories();
                return View(subcategories);
            }
            else
            {
                ICategoryRepository obj = new CategoryRepository();
                SubCategoryModel subcategories = new SubCategoryModel();
                ISubCategoryRepository objsubcategory = new SubCategoryRepository();
                subcategories = objsubcategory.GetSubCategoryById(id);
                subcategories.CategoryList = obj.GetAllCategories();
                return View(subcategories);
            }
        }
        [HttpPost]
        public ActionResult AddSubCategory(SubCategory subcategory)
        {
            ICategoryRepository obj = new CategoryRepository();
            //ViewBag.Categories = obj.GetAllCategories();
            ISubCategoryRepository objsubcategory = new SubCategoryRepository();

            string result = objsubcategory.AddSubCategory(subcategory);          
            return RedirectToAction("GetSubCategories");
        }
        [HttpGet]
        public ActionResult GetSubCategories()
        {
            ISubCategoryRepository obj = new SubCategoryRepository();
            List<SubCategoryModel> subcategorylist = obj.GetAllSubCategories();
            return View(subcategorylist);
        }
        [HttpGet]
        public ActionResult DeleteSubCategory(Guid id)
        {
            ISubCategoryRepository obj = new SubCategoryRepository();
            string result = obj.DeleteSubCategory(id);
            return RedirectToAction("GetSubCategories");
        }
        public JsonResult GetCategorySubCategories(Guid? CategoryId)
        {
            ISubCategoryRepository objsub = new SubCategoryRepository();
            List<SelectListItem> defaultDdl = new List<SelectListItem>();
            defaultDdl.Add(new SelectListItem { Text = "Select SubCategory", Value = "-1", Selected = true });
            List<SubCategory> data = objsub.GetSubCategoryByCategoryId(CategoryId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}