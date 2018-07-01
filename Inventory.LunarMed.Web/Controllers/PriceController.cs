using AutoMapper;
using Inventory.LunarMed.Data.Entities;
using Inventory.LunarMed.Web.Business.Interfaces;
using Inventory.LunarMed.Web.Enum;
using Inventory.LunarMed.Web.Models;
using Inventory.LunarMed.Web.Models.DTO;
using Inventory.LunarMed.Web.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.LunarMed.Web.Controllers
{
    public class PriceController : Controller
    {
        private readonly IGenericRepository<Price> _priceRepository;
        private readonly IGenericRepository<Stock> _stockRepository;
        private readonly IGenericRepository<UnitSize> _unitSizeRepository;
        private readonly IGenericRepository<Brand> _brandRepository;
        private readonly IGenericRepository<Client> _clientRepository;

        public PriceController(IGenericRepository<Price> priceRepository, IGenericRepository<Stock> stockRepository, IGenericRepository<UnitSize> unitSizeRepository,
            IGenericRepository<Brand> brandRepository, IGenericRepository<Client> clientRepository)
        {
            _priceRepository = priceRepository;
            _stockRepository = stockRepository;
            _unitSizeRepository = unitSizeRepository;
            _brandRepository = brandRepository;
            _clientRepository = clientRepository;
        }

        // GET: Product
        /// <summary>
        /// Gets all the prices and pass it on the view
        /// </summary>
        /// <returns>Returns a view containing all the products</returns>
        public ActionResult Index()
        {
            return View(GetListPricesViewModel());
        }

        // GET: Stock/List
        /// <summary>
        /// Gets the list of stocks and pass it in our modal
        /// </summary>
        /// <returns>Returns a partial view that contains the list of all stocks</returns>
        [HttpGet]
        public ActionResult List()
        {
            return this.PartialView("_ListPrices", GetListPricesViewModel().Prices);
        }

        // GET: Stock/Create
        /// <summary>
        /// Displays a partial view used for creating a stock
        /// </summary>
        /// <returns>Returns a partial view used for creating a stock</returns>
        [HttpGet]
        public ActionResult Create()
        {
            var model = new PriceViewModel
            {
                PriceId = 0,
                UnitSizeList = GetUnitSizeList(),
                BrandList = GetBrandListItems(),
                SupplierList = GetSuppliersList()
            };

            return this.PartialView("_AddOrEditPriceModal", model);
        }

        // POST: Price/Create
        /// <summary>
        /// This saves a new price 
        /// </summary>
        /// <param name="model">The PriceViewModel object.</param>
        /// <returns>A partial view containing the result of the process.</returns>
        [HttpPost]
        public ActionResult Create(PriceViewModel model)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var price = Mapper.Map<PriceViewModel, Price>(model);
                _priceRepository.Add(price);

                messages = new List<ViewMessage>
                {
                    new ViewMessage()
                    {
                        Type = MessageType.Success,
                        Message = "New Price successfully saved."
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

        // GET: Price/Edit/5
        /// <summary>
        /// This displays the details of the selected price that is about to be edited
        /// </summary>
        /// <param name="id">The Price ID</param>
        /// <returns>Returns a partial view containing the details of the stock.</returns>
        public ActionResult Edit(int id)
        {
            var price = _priceRepository.Get(id);
            var model = Mapper.Map<Price, PriceViewModel>(price);
            model.UnitSizeList = GetUnitSizeList();
            model.BrandList = GetBrandListItems();
            model.SupplierList = GetSuppliersList();

            return PartialView("_AddOrEditPriceModal", model);
        }

        // POST: Price/Edit/5
        /// <summary>
        /// This updates the client based on the passed Price model
        /// </summary>
        /// <param name="model">The PriceViewModel object.</param>
        /// <returns>A partial view containing the result of the process.</returns>
        [HttpPost]
        public ActionResult Edit(PriceViewModel model)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var price = _priceRepository.Get(model.PriceId);
                if (price != null)
                {
                    price.BrandId = model.BrandId;
                    price.Cost = model.Cost;
                    price.UnitSizeId = model.UnitSizeId;
                    price.ClientId = model.ClientId;
                    _priceRepository.Update(price);
                }
                else
                {
                    messages = new List<ViewMessage>
                    {
                        new ViewMessage()
                        {
                            Type = MessageType.Error,
                            Message = "Price cannot be found."
                        }
                    };
                    return this.PartialView("_ViewMessageList", messages);
                }


                messages = new List<ViewMessage>
                {
                    new ViewMessage()
                    {
                        Type = MessageType.Success,
                        Message = "Price successfully updated."
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

        // POST: Price/Delete
        /// <summary>
        /// This deletes passed Stock id
        /// </summary>
        /// <param name="id">The ID of the price.</param>
        /// <returns>A partial view containing the result of the process.</returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var price = _priceRepository.Get(id);
                if (price == null)
                {
                    messages.Add(new ViewMessage()
                    {
                        Type = MessageType.Error,
                        Message = "Price cannot be found."
                    });
                    return this.PartialView("_ViewMessageList", messages);
                }

                _priceRepository.Delete(price);
                messages.Add(new ViewMessage()
                {
                    Type = MessageType.Success,
                    Message = "Price successfully deleted."
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
        private ListPricesViewModel GetListPricesViewModel()
        {
            var prices = _priceRepository.GetAll();
            var model = new ListPricesViewModel();

            var priceList = Mapper.Map<List<Price>, List<PriceViewModel>>(prices.ToList());
            model.Prices = priceList;
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