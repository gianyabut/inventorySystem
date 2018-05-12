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
    public class ProductController : Controller
    {
        private readonly IGenericRepository<Product> _productRepository;

        public ProductController(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: Product
        /// <summary>
        /// Gets all the products and pass it on the view
        /// </summary>
        /// <returns>Returns a view containing all the products</returns>
        public ActionResult Index()
        {
            return View(GetListProductsModel());
        }

        #region Private Methods

        /// <summary>
        /// This gets all the unit sizes and assign maps it to ListUnitSizeViewModel
        /// </summary>
        /// <returns>Returns a ListUnitSizeViewModel object</returns>
        private ListProductsViewModel GetListProductsModel()
        {
            var products = _productRepository.GetAll();
            var model = new ListProductsViewModel();

            var productsList = Mapper.Map<List<Product>, List<ProductViewModel>>(products.ToList());
            model.Products = productsList;
            model.Messages = new List<ViewMessage>();

            return model;
        }

        #endregion Private Methods
    }
}