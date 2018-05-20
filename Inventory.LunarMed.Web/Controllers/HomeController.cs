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
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<Collection> _collectionRepository;

        public HomeController(IGenericRepository<Product> productRepository, IGenericRepository<Order> orderRepository, IGenericRepository<Collection> collectionRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _collectionRepository = collectionRepository;
        }

        public ActionResult Index()
        {
            var model = new HomePageViewModel
            {
                Products = GetListProductsModel(),
                Orders = GetListOrdersModel(),
                Collections = GetListCollectionsModel()
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
        /// This gets all the products and assign maps it to ProductViewModel
        /// </summary>
        /// <returns>Returns a list of ProductViewModel object</returns>
        private List<ProductViewModel> GetListProductsModel()
        {
            var products = _productRepository.GetAll();
            var productsList = Mapper.Map<List<Product>, List<ProductViewModel>>(products.ToList());

            return productsList;
        }

        /// <summary>
        /// This gets all the orders and assign maps it to List<OrderViewModel>
        /// </summary>
        /// <returns>Returns a list of OrderViewModel object</returns>
        private List<OrderViewModel> GetListOrdersModel()
        {
            var orders = _orderRepository.GetAll();
            var ordersList = Mapper.Map<List<Order>, List<OrderViewModel>>(orders.ToList());

            return ordersList;
        }

        /// <summary>
        /// This gets all the collections and assign maps it to List<CollectionViewModel>
        /// </summary>
        /// <returns>Returns a list of CollectionViewModel object</returns>
        private List<CollectionViewModel> GetListCollectionsModel()
        {
            var collections = _collectionRepository.GetAll();
            var collectionsList = Mapper.Map<List<Collection>, List<CollectionViewModel>>(collections.ToList());

            return collectionsList;
        }

        #endregion Private Methods
    }
}