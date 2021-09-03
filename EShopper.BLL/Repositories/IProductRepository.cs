using EShopper.DAL.Entities;
using EShopper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopper.BLL.Repositories
{
    public interface IProductRepository
    {
        Guid AddProduct(ProductModel product);
        List<ProductModel> GetAllProducts();
        ProductModel GetProductById(Guid? productid);
       // string EditProduct(Product product);
        string DeleteProduct(Guid productid);
        List<Product> Getproducts();
        List<SubCategory> GetproductsforMen();
        List<SubCategory> GetproductsforWomen();
        int GetHighestPrice();
        List<Colors> GetAllColor();
        List<Sizes> GetAllSizes();
        List<ProductModel> GetFilteredProducts(string sortby, string category, string product,string startprice,string endprice,string color,string size);
    }
}
