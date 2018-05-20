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
        private readonly IGenericRepository<OrderDetails> _orderDetailsRepository;

        public OrderController(IGenericRepository<Order> orderRepository, IGenericRepository<Client> clientRepository, 
            IGenericRepository<Product> productRepository, IGenericRepository<OrderDetails> orderDetailsRepository)
        {
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
            _productRepository = productRepository;
            _orderDetailsRepository = orderDetailsRepository;
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

        // GET: Order/List
        public ActionResult List()
        {
            var orders = _orderRepository.GetAll();

            var model = new ListOrdersViewModel
            {
                Orders = Mapper.Map<List<Order>, List<OrderViewModel>>(orders.ToList())
            };

            foreach (var order in model.Orders)
            {
                var orderDetails = _orderDetailsRepository.List(i => i.OrderId == order.OrderId);
                decimal total = 0;
                foreach(var item in orderDetails)
                {
                    var productId = item.ProductId;
                    var quantity = item.Quantity;

                    var product = _productRepository.Find(i => i.ProductId == productId);
                    total += product.SellingPrice * quantity;
                }
                order.Total = total;
            }

            return View(model);
        }

        // GET: Order/GetProducts
        public JsonResult GetProducts(string wildcard)
        {
            var result = new List<KeyValuePair<string, string>>();

            foreach(var product in _productRepository.GetAll().Where(s => s.Name.ToLower().Contains(wildcard.ToLower())))
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

        // GET: Order/GetDetails/id
        public ActionResult GetDetails(int id)
        {
            var order = _orderRepository.Get(id);
            var model = Mapper.Map<Order, DisplayOrderViewModel>(order);

            var ordersListModel = new List<DisplayOrderDetailsViewModel>();
            var orderDetails = _orderDetailsRepository.List(i => i.OrderId == model.OrderId);
            decimal total = 0;
            foreach(var orderDetail in orderDetails)
            {
                var product = _productRepository.Get(orderDetail.ProductId);
                ordersListModel.Add(new DisplayOrderDetailsViewModel()
                {
                    ProductName = product.Name,
                    Quantity = orderDetail.Quantity,
                    Total = orderDetail.Quantity * product.SellingPrice
                });
                total += orderDetail.Quantity * product.SellingPrice;
            }

            model.Total = total;
            model.OrderDetails = ordersListModel;

            return this.PartialView("_OrderDetails", model);
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

                // Subtract current order to quantity
                foreach(var orderDetails in order.OrderDetails)
                {
                    var product = _productRepository.Find(i => i.ProductId == orderDetails.ProductId);
                    product.StockQuantity = product.StockQuantity - orderDetails.Quantity;
                    _productRepository.Update(product);
                }

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