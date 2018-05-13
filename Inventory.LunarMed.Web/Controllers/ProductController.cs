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

        // GET: Product/List
        /// <summary>
        /// Gets the list of products and pass it in our modal
        /// </summary>
        /// <returns>Returns a partial view that contains the list of all products</returns>
        [HttpGet]
        public ActionResult List()
        {
            return this.PartialView("_ListProducts", GetListProductsModel().Products);
        }

        // GET: Product/Create
        /// <summary>
        /// Displays a partial view used for creating a product
        /// </summary>
        /// <returns>Returns a partial view used for creating a product</returns>
        [HttpGet]
        public ActionResult Create()
        {
            var model = new ProductViewModel
            {
                ProductId = 0,
                ExpirationDate = DateTime.Now.ToString("MM/dd/yyyy")
            };

            return this.PartialView("_AddOrEditProductModal", model);
        }

        // POST: Product/Create
        /// <summary>
        /// This saves a new product 
        /// </summary>
        /// <param name="model">The ProductViewModel object.</param>
        /// <returns>A partial view containing the result of the process.</returns>
        [HttpPost]
        public ActionResult Create(ProductViewModel model)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var product = Mapper.Map<ProductViewModel, Product>(model);
                _productRepository.Add(product);

                messages = new List<ViewMessage>
                {
                    new ViewMessage()
                    {
                        Type = MessageType.Success,
                        Message = "New product successfully saved."
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

        // GET: Product/Edit/5
        /// <summary>
        /// This displays the details of the selected product that is about to be edited
        /// </summary>
        /// <param name="id">The Product ID</param>
        /// <returns>Returns a partial view containing the details of the unit size.</returns>
        public ActionResult Edit(int id)
        {
            var product = _productRepository.Get(id);
            var model = Mapper.Map<Product, ProductViewModel>(product);

            return PartialView("_AddOrEditProductModal", model);
        }

        // POST: Product/Edit/5
        /// <summary>
        /// This updates the client based on the passed product model
        /// </summary>
        /// <param name="model">The ProductViewModel object.</param>
        /// <returns>A partial view containing the result of the process.</returns>
        [HttpPost]
        public ActionResult Edit(ProductViewModel model)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var product = _productRepository.Get(model.ProductId);
                if (product != null)
                {
                    product.Name = model.Name;
                    product.BatchNumber = model.BatchNumber;
                    _productRepository.Update(product);
                }
                else
                {
                    messages = new List<ViewMessage>
                    {
                        new ViewMessage()
                        {
                            Type = MessageType.Error,
                            Message = "Product cannot be found."
                        }
                    };
                    return this.PartialView("_ViewMessageList", messages);
                }


                messages = new List<ViewMessage>
                {
                    new ViewMessage()
                    {
                        Type = MessageType.Success,
                        Message = "Product successfully updated."
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

        // POST: Product/Delete
        /// <summary>
        /// This deletes passed product id
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>A partial view containing the result of the process.</returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var product = _productRepository.Get(id);
                if (product == null)
                {
                    messages.Add(new ViewMessage()
                    {
                        Type = MessageType.Error,
                        Message = "Product cannot be found."
                    });
                    return this.PartialView("_ViewMessageList", messages);
                }

                _productRepository.Delete(product);
                messages.Add(new ViewMessage()
                {
                    Type = MessageType.Success,
                    Message = "Product successfully deleted."
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