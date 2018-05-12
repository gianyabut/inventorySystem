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
    public class ProductGroupController : Controller
    {
        private readonly IGenericRepository<ProductGroup> _productGroupRepository;

        public ProductGroupController(IGenericRepository<ProductGroup> productGroupRepository)
        {
            _productGroupRepository = productGroupRepository;
        }

        // GET: ProductGroup
        /// <summary>
        /// Gets all the product groups and pass it on the view
        /// </summary>
        /// <returns>Returns a view containing all the clients</returns>
        public ActionResult Index()
        {
            return View(GetListProductGroupsViewModel());
        }

        // GET: ProductGroup/List
        /// <summary>
        /// Gets the list of product group and pass it in our modal
        /// </summary>
        /// <returns>Returns a partial view that contains the list of all product groups</returns>
        [HttpGet]
        public ActionResult List()
        {
            return this.PartialView("_ListProductGroups", GetListProductGroupsViewModel().ProductGroups);
        }

        // GET: ProductGroup/Create
        /// <summary>
        /// Displays a partial view used for creating a product group
        /// </summary>
        /// <returns>Returns a partial view used for creating a product group</returns>
        [HttpGet]
        public ActionResult Create()
        {
            var model = new ProductGroupViewModel
            {
                ProductGroupId = 0
            };

            return this.PartialView("_AddOrEditProductGroupModal", model);
        }

        // POST: ProductGroup/Create
        /// <summary>
        /// This saves a new product group
        /// </summary>
        /// <param name="model">The ProductGroupViewModel object.</param>
        /// <returns>A partial view containing the result of the process.</returns>
        [HttpPost]
        public ActionResult Create(ProductGroupViewModel model)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var productGroup = Mapper.Map<ProductGroupViewModel, ProductGroup>(model);
                _productGroupRepository.Add(productGroup);

                messages = new List<ViewMessage>
                {
                    new ViewMessage()
                    {
                        Type = MessageType.Success,
                        Message = "New product group successfully saved."
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

        // GET: ProductGroup/Edit/5
        /// <summary>
        /// This displays the details of the selected product group that is about to be edited
        /// </summary>
        /// <param name="id">The Product Group ID</param>
        /// <returns>Returns a partial view containing the details of the product group.</returns>
        public ActionResult Edit(int id)
        {
            var productGroup = _productGroupRepository.Get(id);
            var model = Mapper.Map<ProductGroup, ProductGroupViewModel>(productGroup);

            return PartialView("_AddOrEditProductGroupModal", model);
        }

        // POST: ProductGroup/Edit/5
        /// <summary>
        /// This updates the product group
        /// </summary>
        /// <param name="model">The ProductGroupViewModel object.</param>
        /// <returns>A partial view containing the result of the process.</returns>
        [HttpPost]
        public ActionResult Edit(ProductGroupViewModel model)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var productGroup = _productGroupRepository.Get(model.ProductGroupId);
                if (productGroup != null)
                {
                    productGroup.Name = model.Name;
                    productGroup.Description = model.Description;
                    _productGroupRepository.Update(productGroup);
                }
                else
                {
                    messages = new List<ViewMessage>
                    {
                        new ViewMessage()
                        {
                            Type = MessageType.Error,
                            Message = "Product Group cannot be found."
                        }
                    };
                    return this.PartialView("_ViewMessageList", messages);
                }


                messages = new List<ViewMessage>
                {
                    new ViewMessage()
                    {
                        Type = MessageType.Success,
                        Message = "Product Group successfully updated."
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

        // POST: ProductGroup/Delete
        /// <summary>
        /// This deletes passed product group id
        /// </summary>
        /// <param name="id">The ID of the product group.</param>
        /// <returns>A partial view containing the result of the process.</returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var client = _productGroupRepository.Get(id);
                if (client == null)
                {
                    messages.Add(new ViewMessage()
                    {
                        Type = MessageType.Error,
                        Message = "Product group cannot be found."
                    });
                    return this.PartialView("_ViewMessageList", messages);
                }

                _productGroupRepository.Delete(client);
                messages.Add(new ViewMessage()
                {
                    Type = MessageType.Success,
                    Message = "Product Group successfully deleted."
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
        /// This gets all the product groups and assign maps it to ListProductGroupsViewModel
        /// </summary>
        /// <returns>Returns a ListProductGroupsViewModel object</returns>
        private ListProductGroupsViewModel GetListProductGroupsViewModel()
        {
            var productGroups = _productGroupRepository.GetAll();
            var model = new ListProductGroupsViewModel();

            var productGroupsList = Mapper.Map<List<ProductGroup>, List<ProductGroupViewModel>>(productGroups.ToList());
            model.ProductGroups = productGroupsList;
            model.Messages = new List<ViewMessage>();

            return model;
        }

        #endregion Private Methods
    }
}