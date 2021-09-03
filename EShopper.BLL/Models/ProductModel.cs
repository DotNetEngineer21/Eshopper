using EShopper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShopper.Models
{
    public class ProductModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }        
        public string Color { get; set; }
        public string ProductImage { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Guid SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
        public List<Category> CategoryList { get; set; }
        public List<SubCategoryModel> SubCategoryList { get; set; }
        public int? currentPage { get; set; }
        public int? startPage { get; set; }
        public string SizeXS { get; set; }
        public string SizeS { get; set; }
        public string SizeM { get; set; }
        public string SizeL { get; set; }
        public string SizeXL { get; set; }
        public List<Sizes> SizeList { get; set; }
        public List<Colors> ColorList { get; set; }
        public string Quantity { get; set; }
    }
    public class SubCategoryModel
    {
        public Guid SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Boolean IsActive { get; set; }
        public List<Category> CategoryList { get; set; }
    }
}