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
    public class CollectionController : Controller
    {
        private readonly IGenericRepository<Collection> _collectionRepository;
        private readonly IGenericRepository<Order> _orderRepository;

        public CollectionController(IGenericRepository<Collection> collectionRepository, IGenericRepository<Order> orderRepository)
        {
            _collectionRepository = collectionRepository;
            _orderRepository = orderRepository;
        }

        // GET: Collection
        /// <summary>
        /// Gets all the collection and pass it on the view
        /// </summary>
        /// <returns>Returns a view containing all the collections</returns>
        public ActionResult Index()
        {
            return View(GetListCollectionsViewModel());
        }

        // GET: Collection/List
        /// <summary>
        /// Gets the list of collections and pass it in our modal
        /// </summary>
        /// <returns>Returns a partial view that contains the list of all collections</returns>
        [HttpGet]
        public ActionResult List()
        {
            return this.PartialView("_ListCollections", GetListCollectionsViewModel().Collections);
        }

        // GET: Collection/Create
        /// <summary>
        /// Displays a partial view used for creating a collection
        /// </summary>
        /// <returns>Returns a partial view used for creating a unit size</returns>
        [HttpGet]
        public ActionResult Create()
        {
            var model = new CollectionViewModel
            {
                CollectionId = 0,
                CustomerPONumberList = GetCustomerPONumberList()
            };

            return this.PartialView("_AddOrEditCollection", model);
        }

        // POST: Collection/Create
        /// <summary>
        /// This saves a new collection using the parameter collection model
        /// </summary>
        /// <param name="model">The CollectionViewModel object.</param>
        /// <returns>A partial view containing the result of the process.</returns>
        [HttpPost]
        public ActionResult Create(CollectionViewModel model)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var collection = Mapper.Map<CollectionViewModel, Collection>(model);
                var order = _orderRepository.Find(i => i.CustomerPONumber == model.CustomerPONumber);
                collection.OrderId = order.OrderId;
                _collectionRepository.Add(collection);

                messages = new List<ViewMessage>
                {
                    new ViewMessage()
                    {
                        Type = MessageType.Success,
                        Message = "New collection successfully saved."
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

        // GET: Collection/Edit/5
        /// <summary>
        /// This displays the details of the selected collection that is about to be edited
        /// </summary>
        /// <param name="id">The Collection ID</param>
        /// <returns>Returns a partial view containing the details of the collection.</returns>
        public ActionResult Edit(int id)
        {
            var collection = _collectionRepository.Get(id);
            var model = Mapper.Map<Collection, CollectionViewModel>(collection);
            model.CustomerPONumberList = GetCustomerPONumberList();

            return PartialView("_AddOrEditCollection", model);
        }

        // POST: Collection/Edit/5
        /// <summary>
        /// This updates the collection based on the passed collection model
        /// </summary>
        /// <param name="model">The CollectionViewModel object.</param>
        /// <returns>A partial view containing the result of the process.</returns>
        [HttpPost]
        public ActionResult Edit(CollectionViewModel model)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var collection = _collectionRepository.Get(model.CollectionId);
                if (collection != null)
                {
                    var order = _orderRepository.Find(i => i.CustomerPONumber == model.CustomerPONumber);

                    collection.OrderId = order.OrderId;
                    collection.CheckNumber = model.CheckNumber;
                    collection.Amount = model.Amount;
                    collection.Balance = model.Balance;
                    _collectionRepository.Update(collection);
                }
                else
                {
                    messages = new List<ViewMessage>
                    {
                        new ViewMessage()
                        {
                            Type = MessageType.Error,
                            Message = "Collection cannot be found."
                        }
                    };
                    return this.PartialView("_ViewMessageList", messages);
                }

                messages = new List<ViewMessage>
                {
                    new ViewMessage()
                    {
                        Type = MessageType.Success,
                        Message = "Collection successfully updated."
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

        // POST: Collection/Delete
        /// <summary>
        /// This deletes passed collection id
        /// </summary>
        /// <param name="id">The ID of the collection.</param>
        /// <returns>A partial view containing the result of the process.</returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var collection = _collectionRepository.Get(id);
                if (collection == null)
                {
                    messages.Add(new ViewMessage()
                    {
                        Type = MessageType.Error,
                        Message = "Collection cannot be found."
                    });
                    return this.PartialView("_ViewMessageList", messages);
                }

                _collectionRepository.Delete(collection);
                messages.Add(new ViewMessage()
                {
                    Type = MessageType.Success,
                    Message = "Collection successfully deleted."
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
        /// This gets all the collections and assign maps it to ListCollectionsViewModel
        /// </summary>
        /// <returns>Returns a ListUnitSizeViewModel object</returns>
        private ListCollectionsViewModel GetListCollectionsViewModel()
        {
            var collections = _collectionRepository.GetAll();
            var model = new ListCollectionsViewModel();

            var collectionList = Mapper.Map<List<Collection>, List<CollectionViewModel>>(collections.ToList());
            model.Collections = collectionList;
            model.Messages = new List<ViewMessage>();

            return model;
        }

        /// <summary>
        /// Get all customer PO number and assign to SelectListItem variable
        /// </summary>
        /// <returns>The SelectListItem variable of all Customer PO Number</returns>
        private List<SelectListItem> GetCustomerPONumberList()
        {
            var poNumberListItems = new List<SelectListItem>();
            foreach (var order in _orderRepository.GetAll())
            {
                poNumberListItems.Add(new SelectListItem()
                {
                    Text = order.CustomerPONumber,
                    Value = order.CustomerPONumber
                });
            }

            return poNumberListItems;
        }

        #endregion Private Methods
    }
}