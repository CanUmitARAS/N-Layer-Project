using BussinesLayer.Concrete;
using BussinesLayer.FluentValidation;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Demo_Product.Controllers
{
    public class ProductController : Controller
    {
        ProductManager productManager = new ProductManager(new EfProductDal());
		CategoryManager categoryManager= new CategoryManager(new EfCategoryDal());
		public IActionResult Index()
        {
            var values = productManager.GetProductWithCategory();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
			List<SelectListItem> values = (from x in categoryManager.TGetList()

										   select new SelectListItem
										   {
											   Text = x.CategoryName,
											   Value = x.CategoryId.ToString()
										   }).ToList();
			ViewBag.v = values;
			return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product p)
        {
            ProductValidator validationRules = new ProductValidator();
            ValidationResult results = validationRules.Validate(p);
            if(results.IsValid)
            {
				productManager.TInsert(p);
				return RedirectToAction("Index");
			}
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.ErrorCode, item.ErrorMessage);
                }
            }

            return View();
           
        }

        public IActionResult DeleteProduct(int id)
        {
            var value = productManager.TGetById(id);
            productManager.TDelete(value);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            var value = productManager.TGetById(id);
            
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product p)
        {
           
            productManager.TUpdate(p);
            return RedirectToAction("Index");
        }

    }
}
