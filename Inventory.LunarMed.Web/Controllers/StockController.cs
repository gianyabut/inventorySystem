using AutoMapper;
using Inventory.LunarMed.Data.Entities;
using Inventory.LunarMed.Web.Business.Interfaces;
using Inventory.LunarMed.Web.Enum;
using Inventory.LunarMed.Web.Models;
using Inventory.LunarMed.Web.Models.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.LunarMed.Web.Controllers
{
    public class StockController : Controller
    {
        private readonly IGenericRepository<Stock> _stockRepository;
        private readonly IGenericRepository<UnitSize> _unitSizeRepository;
        private readonly IGenericRepository<Brand> _brandRepository;
        private readonly IGenericRepository<Client> _clientRepository;

        public StockController(IGenericRepository<Stock> stockRepository, IGenericRepository<UnitSize> unitSizeRepository, 
            IGenericRepository<Brand> brandRepository, IGenericRepository<Client> clientRepository)
        {
            _stockRepository = stockRepository;
            _unitSizeRepository = unitSizeRepository;
            _brandRepository = brandRepository;
            _clientRepository = clientRepository;
        }

        // GET: Product
        /// <summary>
        /// Gets all the products and pass it on the view
        /// </summary>
        /// <returns>Returns a view containing all the products</returns>
        public ActionResult Index()
        {
            return View(GetListStocksModel());
        }

        // GET: Stock/List
        /// <summary>
        /// Gets the list of stocks and pass it in our modal
        /// </summary>
        /// <returns>Returns a partial view that contains the list of all stocks</returns>
        [HttpGet]
        public ActionResult List()
        {
            return this.PartialView("_ListStocks", GetListStocksModel().Stocks);
        }

        // GET: Stock/Create
        /// <summary>
        /// Displays a partial view used for creating a stock
        /// </summary>
        /// <returns>Returns a partial view used for creating a stock</returns>
        [HttpGet]
        public ActionResult Create()
        {
            var model = new StockViewModel
            {
                StockId = 0,
                ExpirationDate = DateTime.Now.ToString("MM/dd/yyyy"),
                PurchaseDate = DateTime.Now.ToString("MM/dd/yyyy"),
                StockQuantity = 1,
                MarkUp = 10,
                UnitSizeList = GetUnitSizeList(),
                BrandList = GetBrandListItems(),
                SupplierList = GetSuppliersList()
            };

            return this.PartialView("_AddOrEditStockModal", model);
        }

        // POST: Stock/Create
        /// <summary>
        /// This saves a new stock 
        /// </summary>
        /// <param name="model">The StockViewModel object.</param>
        /// <returns>A partial view containing the result of the process.</returns>
        [HttpPost]
        public ActionResult Create(StockViewModel model)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var stock = Mapper.Map<StockViewModel, Stock>(model);
                _stockRepository.Add(stock);

                messages = new List<ViewMessage>
                {
                    new ViewMessage()
                    {
                        Type = MessageType.Success,
                        Message = "New Stock successfully saved."
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

        // GET: Stock/Edit/5
        /// <summary>
        /// This displays the details of the selected stock that is about to be edited
        /// </summary>
        /// <param name="id">The Stock ID</param>
        /// <returns>Returns a partial view containing the details of the stock.</returns>
        public ActionResult Edit(int id)
        {
            var stock = _stockRepository.Get(id);
            var model = Mapper.Map<Stock, StockViewModel>(stock);
            model.UnitSizeList = GetUnitSizeList();
            model.BrandList = GetBrandListItems();
            model.SupplierList = GetSuppliersList();

            return PartialView("_AddOrEditStockModal", model);
        }

        // POST: Stock/Edit/5
        /// <summary>
        /// This updates the client based on the passed Stock model
        /// </summary>
        /// <param name="model">The StockViewModel object.</param>
        /// <returns>A partial view containing the result of the process.</returns>
        [HttpPost]
        public ActionResult Edit(StockViewModel model)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var stock = _stockRepository.Get(model.StockId);
                if (stock != null)
                {
                    stock.BrandId = model.BrandId;
                    stock.BatchNumber = model.BatchNumber;
                    stock.Cost = model.Cost;
                    stock.SRP = model.SRP;
                    stock.MarkUp = model.MarkUp;
                    stock.StockQuantity = model.StockQuantity;
                    stock.ExpirationDate = DateTime.ParseExact(model.ExpirationDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    stock.UnitSizeId = model.UnitSizeId;
                    stock.ClientId = model.ClientId;
                    stock.PurchaseDate = DateTime.ParseExact(model.PurchaseDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    _stockRepository.Update(stock);
                }
                else
                {
                    messages = new List<ViewMessage>
                    {
                        new ViewMessage()
                        {
                            Type = MessageType.Error,
                            Message = "Stock cannot be found."
                        }
                    };
                    return this.PartialView("_ViewMessageList", messages);
                }


                messages = new List<ViewMessage>
                {
                    new ViewMessage()
                    {
                        Type = MessageType.Success,
                        Message = "Stock successfully updated."
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

        // POST: Stock/Delete
        /// <summary>
        /// This deletes passed Stock id
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>A partial view containing the result of the process.</returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var stock = _stockRepository.Get(id);
                if (stock == null)
                {
                    messages.Add(new ViewMessage()
                    {
                        Type = MessageType.Error,
                        Message = "Stock cannot be found."
                    });
                    return this.PartialView("_ViewMessageList", messages);
                }

                _stockRepository.Delete(stock);
                messages.Add(new ViewMessage()
                {
                    Type = MessageType.Success,
                    Message = "Stock successfully deleted."
                });
            }
            catch (Exception ex)
            {
                messages.Add(new ViewMessage()
                {
                    Type = MessageType.Error,
                    Message = ex.Message.ToString()
                });
            }

            return this.PartialView("_ViewMessageList", messages);
        }

        #region Private Methods

        /// <summary>
        /// This gets all the unit sizes and assign maps it to ListStocksViewModel
        /// </summary>
        /// <returns>Returns a ListStocksViewModel object</returns>
        private ListStocksViewModel GetListStocksModel()
        {
            var stocks = _stockRepository.GetAll();
            var model = new ListStocksViewModel();

            var productsList = Mapper.Map<List<Stock>, List<StockViewModel>>(stocks.ToList());
            model.Stocks = productsList;
            model.Messages = new List<ViewMessage>();

            return model;
        }

        /// <summary>
        /// Get all unit size and assign to SelectListItem variable
        /// </summary>
        /// <returns>The SelectListItem variable of all UnitSize</returns>
        private List<SelectListItem> GetUnitSizeList()
        {
            var unitSizeListItem = new List<SelectListItem>();
            foreach (var unitSize in _unitSizeRepository.GetAll())
            {
                unitSizeListItem.Add(new SelectListItem()
                {
                    Text = unitSize.Name,
                    Value = unitSize.UnitSizeId.ToString()
                });
            }

            return unitSizeListItem;
        }

        /// <summary>
        /// Get all supplier and assign to SelectListItem variable
        /// </summary>
        /// <returns>The SelectListItem variable of all Suppliers</returns>
        private List<SelectListItem> GetSuppliersList()
        {
            var supplierItem = new List<SelectListItem>();
            foreach (var supplier in _clientRepository.List(i => i.IsSupplier))
            {
                supplierItem.Add(new SelectListItem()
                {
                    Text = supplier.Name,
                    Value = supplier.ClientId.ToString()
                });
            }

            return supplierItem;
        }

        /// <summary>
        /// Get all brands and assign to SelectListItem variable
        /// </summary>
        /// <returns>The GetBrandListItems variable of all brands</returns>
        private List<SelectListItem> GetBrandListItems()
        {
            var brandListItem = new List<SelectListItem>();
            foreach (var brand in _brandRepository.GetAll())
            {
                brandListItem.Add(new SelectListItem()
                {
                    Text = brand.Name,
                    Value = brand.BrandId.ToString()
                });
            }

            return brandListItem;
        }

        #endregion Private Methods
    }
}