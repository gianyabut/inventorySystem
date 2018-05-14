using Inventory.LunarMed.Data.Entities;
using Inventory.LunarMed.Web.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.LunarMed.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IGenericRepository<Sale> _orderRepository;

        public OrderController(IGenericRepository<Sale> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
    }
}