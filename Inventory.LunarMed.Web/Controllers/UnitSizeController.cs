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
    public class UnitSizeController : Controller
    {
        private readonly IGenericRepository<UnitSize> _unitSizeRepository;

        public UnitSizeController(IGenericRepository<UnitSize> unitSizeRepository)
        {
            _unitSizeRepository = unitSizeRepository;
        }

        // GET: UnitSize
        /// <summary>
        /// Gets all the clients and pass it on the view
        /// </summary>
        /// <returns>Returns a view containing all the clients</returns>
        public ActionResult Index()
        {
            return View(GetListUnitSizesModel());
        }

        // GET: UnitSize/List
        /// <summary>
        /// Gets the list of unit sizes and pass it in our modal
        /// </summary>
        /// <returns>Returns a partial view that contains the list of all unit sizes</returns>
        [HttpGet]
        public ActionResult List()
        {
            return this.PartialView("_ListUnitSizes", GetListUnitSizesModel().UnitSizes);
        }

        // GET: UnitSize/Create
        /// <summary>
        /// Displays a partial view used for creating a unit size
        /// </summary>
        /// <returns>Returns a partial view used for creating a unit size</returns>
        [HttpGet]
        public ActionResult Create()
        {
            var model = new UnitSizeViewModel
            {
                UnitSizeId = 0
            };

            return this.PartialView("_AddOrEditUnitSizeModal", model);
        }

        // POST: UnitSize/Create
        /// <summary>
        /// This saves a new unit size using the parameter unit size model
        /// </summary>
        /// <param name="model">The UnitSizeViewModel object.</param>
        /// <returns>A partial view containing the result of the process.</returns>
        [HttpPost]
        public ActionResult Create(UnitSizeViewModel model)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var client = Mapper.Map<UnitSizeViewModel, UnitSize>(model);
                _unitSizeRepository.Add(client);

                messages = new List<ViewMessage>
                {
                    new ViewMessage()
                    {
                        Type = MessageType.Success,
                        Message = "New unit size successfully saved."
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

        // GET: UnitSize/Edit/5
        /// <summary>
        /// This displays the details of the selected unit size that is about to be edited
        /// </summary>
        /// <param name="id">The Unit Size ID</param>
        /// <returns>Returns a partial view containing the details of the unit size.</returns>
        public ActionResult Edit(int id)
        {
            var unitSize = _unitSizeRepository.Get(id);
            var model = Mapper.Map<UnitSize, UnitSizeViewModel>(unitSize);

            return PartialView("_AddOrEditUnitSizeModal", model);
        }

        // POST: UnitSize/Edit/5
        /// <summary>
        /// This updates the client based on the passed unit size model
        /// </summary>
        /// <param name="model">The UnitSizeViewModel object.</param>
        /// <returns>A partial view containing the result of the process.</returns>
        [HttpPost]
        public ActionResult Edit(UnitSizeViewModel model)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var client = _unitSizeRepository.Get(model.UnitSizeId);
                if (client != null)
                {
                    client.Name = model.Name;
                    client.Description = model.Description;
                    _unitSizeRepository.Update(client);
                }
                else
                {
                    messages = new List<ViewMessage>
                    {
                        new ViewMessage()
                        {
                            Type = MessageType.Error,
                            Message = "Unit Size cannot be found."
                        }
                    };
                    return this.PartialView("_ViewMessageList", messages);
                }


                messages = new List<ViewMessage>
                {
                    new ViewMessage()
                    {
                        Type = MessageType.Success,
                        Message = "Unit Size successfully updated."
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

        // POST: UnitSize/Delete
        /// <summary>
        /// This deletes passed unit size id
        /// </summary>
        /// <param name="id">The ID of the unit size.</param>
        /// <returns>A partial view containing the result of the process.</returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var client = _unitSizeRepository.Get(id);
                if (client == null)
                {
                    messages.Add(new ViewMessage()
                    {
                        Type = MessageType.Error,
                        Message = "Unit Size cannot be found."
                    });
                    return this.PartialView("_ViewMessageList", messages);
                }

                _unitSizeRepository.Delete(client);
                messages.Add(new ViewMessage()
                {
                    Type = MessageType.Success,
                    Message = "Unit Size successfully deleted."
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
        private ListUnitSizeViewModel GetListUnitSizesModel()
        {
            var unitSizes = _unitSizeRepository.GetAll();
            var model = new ListUnitSizeViewModel();

            var unitSizesList = Mapper.Map<List<UnitSize>, List<UnitSizeViewModel>>(unitSizes.ToList());
            model.UnitSizes = unitSizesList;
            model.Messages = new List<ViewMessage>();

            return model;
        }

        #endregion Private Methods
    }
}