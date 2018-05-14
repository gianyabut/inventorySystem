using AutoMapper;
using Inventory.LunarMed.Data.Entities;
using Inventory.LunarMed.Web.Business.Interfaces;
using Inventory.LunarMed.Web.Models;
using Inventory.LunarMed.Web.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.LunarMed.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGenericRepository<Product> _productRepository;

        public HomeController(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public ActionResult Index()
        {
            var model = new HomePageViewModel
            {
                Products = GetListProductsModel()
            };

            return View(model);
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

        #region Private Methods

        /// <summary>
        /// This gets all the unit sizes and assign maps it to ListUnitSizeViewModel
        /// </summary>
        /// <returns>Returns a ListUnitSizeViewModel object</returns>
        private List<ProductViewModel> GetListProductsModel()
        {
            var products = _productRepository.GetAll();
            var productsList = Mapper.Map<List<Product>, List<ProductViewModel>>(products.ToList());

            return productsList;
        }

        #endregion Private Methods
    }
}