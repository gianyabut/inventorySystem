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
        private readonly IGenericRepository<Stock> _stockRepository;
        private readonly IGenericRepository<OrderDetails> _orderDetailsRepository;
        private readonly IGenericRepository<Brand> _brandRepository;
        private readonly IGenericRepository<Generic> _genericRepository;

        public OrderController(IGenericRepository<Order> orderRepository, IGenericRepository<Client> clientRepository, 
            IGenericRepository<Stock> stockRepository, IGenericRepository<OrderDetails> orderDetailsRepository, IGenericRepository<Brand> brandRepository,
            IGenericRepository<Generic> genericRepository)
        {
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
            _stockRepository = stockRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _brandRepository = brandRepository;
            _genericRepository = genericRepository;
        }

        // GET: Order
        public ActionResult Index(string type)
        {
            bool isSupplier = (type == "client" ? false : true);
            var model = new OrderViewModel()
            {
                ClientsList = GetClients(isSupplier)
            };

            model.Type = type;

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
                    var productId = item.StockId;
                    var quantity = item.Quantity;

                    var product = _stockRepository.Find(i => i.StockId == productId);
                    total += product.SRP * quantity;
                }
                order.Total = total;
            }

            return View(model);
        }

        // GET: Order/GetProducts
        public JsonResult GetProducts(string wildcard)
        {
            var result = new List<KeyValuePair<string, string>>();

            foreach (var generic in _genericRepository.List(i => i.Name.Contains(wildcard.ToLower().ToString())))
            {
                result.Add(new KeyValuePair<string, string>(generic.GenericId.ToString(), generic.Name));
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Order/GetProductDetails/id
        public JsonResult GetProductDetails(int id)
        {
            var product = _stockRepository.List(i => i.Brand.GenericId == id);
            var model = Mapper.Map<List<Stock>, List<StockViewModel>>(product);

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
                var product = _stockRepository.Get(orderDetail.StockId);
                ordersListModel.Add(new DisplayOrderDetailsViewModel()
                {
                    // ToDo
                    ProductName = product.Brand.Name,
                    Quantity = orderDetail.Quantity,
                    Total = orderDetail.Quantity * product.SRP
                });
                total += orderDetail.Quantity * product.SRP;
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
                    var product = _stockRepository.Find(i => i.StockId == orderDetails.StockId);
                    if(product.StockQuantity > 0)
                    {
                        if (product.StockQuantity >= orderDetails.Quantity)
                        {
                            product.StockQuantity = product.StockQuantity - orderDetails.Quantity;
                        }
                        else
                        {
                            messages = new List<ViewMessage>
                            {
                                new ViewMessage()
                                {
                                    Type = MessageType.Error,
                                    Message = "Quantity is greater current stock"
                                }
                            };
                            return this.PartialView("_ViewMessageList", messages);
                        }
                    }
                    _stockRepository.Update(product);
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
        private List<SelectListItem> GetClients(bool isSupplier)
        {
            var clientList = new List<SelectListItem>();
            foreach (var client in _clientRepository.List(i => i.IsSupplier == isSupplier))
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