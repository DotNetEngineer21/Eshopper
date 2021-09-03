using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EShopper.DAL;
using EShopper.DAL.Entities;
using EShopper.BLL.Repositories;
using EShopper.Models;
using EShopper.BLL.Models;
using System.Web.Security;

namespace EShopper.Controllers
{
    public class HomeController : Controller
    {
        public static List<ProductModel> _checkoutitemlist = new List<ProductModel>();
        public List<ProductModel> CheckOutItemsList
        {
            get
            {
                return _checkoutitemlist;
            }
            set
            {
                _checkoutitemlist = value;
            }
        }
        public static List<ProductModel> _filteredrecordlist = new List<ProductModel>();
        public static List<ProductModel> productList;
        public List<ProductModel> FilteredRecordList
        {
            get
            {
                return _filteredrecordlist;
            }
            set { _filteredrecordlist = value; }
        }
        public static List<ProductModel> _wishlist = new List<ProductModel>();
        public List<ProductModel> Wishlist
        {
            get
            {
                return _wishlist;
            }
            set
            {
                _wishlist = value;
            }
        }
        public ActionResult Index()
        {
            List<ProductModel> productslist = new List<ProductModel>();
            IProductRepository obj = new ProductRepository();
            ViewBag.CategoryList = obj.Getproducts();
            if (FilteredRecordList.Count != 0)
            {
                productslist = ViewBag.FilteredProducts;
            }
            else
            {
                productslist = obj.GetAllProducts();
            }
            return View(productslist);

        }
        public ActionResult PartialIndex()
        {
            List<ProductModel> productslist = new List<ProductModel>();
            IProductRepository obj = new ProductRepository();
            ViewBag.CategoryList = obj.Getproducts();
            if (FilteredRecordList.Count != 0)
            {
                productslist = ViewBag.FilteredProducts;
            }
            else
            {
                productslist = obj.GetAllProducts();
            }
            return PartialView(productslist);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User objUser)
        {
            try
            {
                IUserRepository obj = new UserRepository();
                UserModel user = obj.checkUser(objUser.UserName, objUser.Password);
                System.Web.Security.FormsAuthentication.SetAuthCookie(user.Email, false);
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, user.Email, DateTime.Now, DateTime.Now.AddMinutes(20), false, user.RoleName.ToString().Trim('"'));
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);
                if (user.RoleName == "User")
                {
                    if (Request.UrlReferrer.Query != "")
                    {
                        return RedirectToAction("CheckOut");
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("GetProducts", "Admin");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login_Register");
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User objUser)
        {
            IUserRepository obj = new UserRepository();
            obj.AddUser(objUser);
            return RedirectToAction("Login_Register");
        }
        [HttpGet]
        public ActionResult Login_Register(string value)
        {
            //string result = " Name= " Name " Department= " Department;
            return View();
            //return Content("Value="+id);
            //return View("Login_Register", new { value = id });
        }
        [HttpGet]
        public ActionResult ProductDetails1(Guid id)
        {
            IProductRepository obj = new ProductRepository();
            ProductModel objmodel = new ProductModel();
            objmodel = obj.GetProductById(id);
            return View(objmodel);
        }
        [HttpPost]
        public ActionResult ProductDetails1(ProductModel product)
        {
            IProductRepository obj = new ProductRepository();
            //ProductModel objmodel = new ProductModel();
            if (CheckOutItemsList.Count == 0)
            {
                CheckOutItemsList = new List<ProductModel>();
                CheckOutItemsList.Add(obj.GetProductById(product.ProductId));
            }
            else
            {
                CheckOutItemsList.Add(obj.GetProductById(product.ProductId));
            }
            return RedirectToAction("CheckOut");
        }
        [HttpGet]
        public ActionResult CheckOut1()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<ProductModel> products = new List<ProductModel>();
                products = CheckOutItemsList;
                return View(products);
            }
            else
                return RedirectToAction("Login_Register", "Home", new { value = "abc" });
        }
        [HttpGet]
        public JsonResult GetProductsWithFilter(string sortby, string gender, string product, string startprice, string endprice, string color, string size, int pageNo, int paging)
        {
            try
            {
                IProductRepository objprouct = new ProductRepository();
                // List<ProductModel> obj = new List<ProductModel>();
                productList = objprouct.GetFilteredProducts(sortby, gender, product, startprice, endprice, color, size);
                productList.ForEach(z => z.currentPage = pageNo);
                return Json(productList, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index", "Home", new
                //{
                //    model = model
                //});
                //return RedirectToAction("Index");
                //return RedirectToAction("Index", "Home", new { model = obj });
                //return View("../Home/Index", model);
                //return View("Index", model);
                //return PartialView("../Home/PartialIndex", model);
            }
            catch (Exception exception)
            {
                //return Json(exception);
                return null;
            }
        }
        [HttpGet]
        public ActionResult ProductItemLists(int pageNo, int paging)
        {
            List<ProductModel> data = productList.Skip((pageNo - 1) * 10).Take(10).ToList();
            productList.ForEach(z => z.currentPage = pageNo);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Logout()
        {
            CheckOutItemsList = new List<ProductModel>();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login_Register", "Home");
        }
        [HttpGet]
        public ActionResult Shop()
        {
            List<ProductModel> productslist = new List<ProductModel>();
            IProductRepository obj = new ProductRepository();
            ViewBag.productListofMen = obj.GetproductsforMen();
            ViewBag.productListofWomen = obj.GetproductsforWomen();
            ViewBag.HighestPrice = obj.GetHighestPrice();
            //////
            ViewBag.AllColor = obj.GetAllColor();
            ViewBag.AllSize = obj.GetAllSizes();
            if (FilteredRecordList.Count != 0)
            {
                productslist = ViewBag.FilteredProducts;
            }
            else
            {
                productslist = obj.GetAllProducts();
            }
            return View(productslist);
        }
        [HttpGet]
        public ActionResult AddToCart(Guid id)
        {
            List<ProductModel> products = new List<ProductModel>();
            IProductRepository obj = new ProductRepository();
            if (CheckOutItemsList.Count == 0)
            {
                CheckOutItemsList = new List<ProductModel>();
                CheckOutItemsList.Add(obj.GetProductById(id));
            }
            else
            {
                //CheckOutItemsList.Exists(Predicate<obj.GetProductById(id)>);

                foreach (var item in _checkoutitemlist)
                {
                    if (item.ProductId != id)
                    {
                        CheckOutItemsList.Add(obj.GetProductById(id));
                    }
                }
            }
            products = CheckOutItemsList;
            return View(products);
        }
        [HttpGet]
        public ActionResult AddToCartList()
        {
            return Json(CheckOutItemsList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RemoveProductFromAddToCartList(Guid id)
        {
            IProductRepository obj = new ProductRepository();
            ProductModel objmodel = new ProductModel();
            objmodel = obj.GetProductById(id);
            CheckOutItemsList.Remove(objmodel);
            return Json(CheckOutItemsList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CheckOut(List<ProductModel> modelList)
        {
            return View(modelList);
        }
        public ActionResult ProductDetails(Guid id)
        {
            IProductRepository obj = new ProductRepository();
            ProductModel objmodel = new ProductModel();
            objmodel = obj.GetProductById(id);
            return View(objmodel);
        }
        public ActionResult AddToWishList(Guid id)
        {
            IProductRepository obj = new ProductRepository();
            List<ProductModel> objmodel = new List<ProductModel>();
            if (Wishlist.Count == 0)
            {
                Wishlist = new List<ProductModel>();
                Wishlist.Add(obj.GetProductById(id));
            }
            else
            {
                foreach (var item in _wishlist)
                {
                    if (item.ProductId != id)
                    {
                        Wishlist.Add(obj.GetProductById(id));
                    }
                }
            }
            objmodel = Wishlist;
            return View(objmodel);
        }
    }
}