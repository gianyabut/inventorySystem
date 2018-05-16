using AutoMapper;
using Inventory.LunarMed.Data.Entities;
using Inventory.LunarMed.Web.Business.Interfaces;
using Inventory.LunarMed.Web.Enum;
using Inventory.LunarMed.Web.Models;
using Inventory.LunarMed.Web.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.LunarMed.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<Client> _clientRepository;
        private readonly IGenericRepository<Product> _productRepository;

        public OrderController(IGenericRepository<Order> orderRepository, IGenericRepository<Client> clientRepository, IGenericRepository<Product> productRepository)
        {
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
            _productRepository = productRepository;
        }

        // GET: Order
        public ActionResult Index()
        {
            var model = new OrderViewModel()
            {
                ClientsList = GetClients()
            };

            return View(model);
        }

        // GET: Order/GetProducts
        public JsonResult GetProducts(string wildcard)
        {
            var result = new List<KeyValuePair<string, string>>();

            foreach(var product in _productRepository.GetAll().Where(s => s.Name.Contains(wildcard)))
            {
                var name = product.Name.ToString() + " (Left: " + product.StockQuantity + ")";
                result.Add(new KeyValuePair<string, string>(product.ProductId.ToString(), name));
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Order/GetProductDetails/id
        public JsonResult GetProductDetails(int id)
        {
            var product = _productRepository.Find(i => i.ProductId == id);
            var model = Mapper.Map<Product, ProductViewModel>(product);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // POST: Order/Create
        /// <summary>
        /// This saves a new order 
        /// </summary>
        /// <param name="model">The ProductViewModel object.</param>
        /// <returns>A partial view containing the result of the process.</returns>
        [HttpPost]
        public ActionResult Create(OrderViewModel model)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var order = Mapper.Map<OrderViewModel, Order>(model);
                _orderRepository.Add(order);

                messages = new List<ViewMessage>
                {
                    new ViewMessage()
                    {
                        Type = MessageType.Success,
                        Message = "Order successfully saved."
                    }
                };
            }
            catch (Exception ex)
            {
                messages = new List<ViewMessage>
                {
                    new ViewMessage()
                    {
                        Type = MessageType.Error,
                        Message = ex.Message.ToString()
                    }
                };
            }

            return this.PartialView("_ViewMessageList", messages);
        }

        #region Private Methods

        /// <summary>
        /// Get all clients and assign to SelectListItem variable
        /// </summary>
        /// <returns>The SelectListItem variable of all clients</returns>
        private List<SelectListItem> GetClients()
        {
            var clientList = new List<SelectListItem>();
            foreach (var client in _clientRepository.GetAll())
            {
                clientList.Add(new SelectListItem()
                {
                    Text = client.Name,
                    Value = client.ClientId.ToString()
                });
            }

            return clientList;
        }

        #endregion Private Methods

    }
}