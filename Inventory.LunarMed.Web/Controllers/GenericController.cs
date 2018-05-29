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
    public class GenericController : Controller
    {
        private readonly IGenericRepository<Generic> _genericRepository;

        public GenericController(IGenericRepository<Generic> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        // GET: Generic
        /// <summary>
        /// Gets all the generics and pass it on the view
        /// </summary>
        /// <returns>Returns a view containing all the generics</returns>
        public ActionResult Index()
        {
            return View(GetGenericsList());
        }

        // GET: Generic/List
        /// <summary>
        /// Gets the list of generics and pass it in our modal
        /// </summary>
        /// <returns>Returns a partial view that contains the list of all generics</returns>
        [HttpGet]
        public ActionResult List()
        {
            return this.PartialView("_ListGenerics", GetGenericsList().Generics);
        }

        // GET: Generic/Create
        /// <summary>
        /// Displays a partial view used for creating a generic
        /// </summary>
        /// <returns>Returns a partial view used for creating a generic</returns>
        [HttpGet]
        public ActionResult Create()
        {
            var model = new GenericViewModel
            {
                GenericId = 0
            };

            return this.PartialView("_AddOrEditGenericModal", model);
        }

        // POST: Generic/Create
        /// <summary>
        /// This saves a new generic using the parameter generic model
        /// </summary>
        /// <param name="model">The GenericViewModel object.</param>
        /// <returns>A partial view containing the result of the process.</returns>
        [HttpPost]
        public ActionResult Create(GenericViewModel model)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var generic = Mapper.Map<GenericViewModel, Generic>(model);
                _genericRepository.Add(generic);

                messages = new List<ViewMessage>
                {
                    new ViewMessage()
                    {
                        Type = MessageType.Success,
                        Message = "New generic successfully saved."
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

        // GET: Generic/Edit/5
        /// <summary>
        /// This displays the details of the selected generic that is about to be edited
        /// </summary>
        /// <param name="id">The generic ID</param>
        /// <returns>Returns a partial view containing the details of the generic.</returns>
        public ActionResult Edit(int id)
        {
            var generic = _genericRepository.Get(id);
            var model = Mapper.Map<Generic, GenericViewModel>(generic);

            return PartialView("_AddOrEditGenericModal", model);
        }

        // POST: Generic/Edit/5
        /// <summary>
        /// This updates the generic based on the passed generic model
        /// </summary>
        /// <param name="model">The GenericViewModel object.</param>
        /// <returns>A partial view containing the result of the process.</returns>
        [HttpPost]
        public ActionResult Edit(GenericViewModel model)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var generic = _genericRepository.Get(model.GenericId);
                if (generic != null)
                {
                    generic.Name = model.Name;
                    generic.Description = model.Description;
                    _genericRepository.Update(generic);
                }
                else
                {
                    messages = new List<ViewMessage>
                    {
                        new ViewMessage()
                        {
                            Type = MessageType.Error,
                            Message = "Generic cannot be found."
                        }
                    };
                    return this.PartialView("_ViewMessageList", messages);
                }


                messages = new List<ViewMessage>
                {
                    new ViewMessage()
                    {
                        Type = MessageType.Success,
                        Message = "Generic successfully updated."
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

        // POST: Generic/Delete
        /// <summary>
        /// This deletes passed generic id
        /// </summary>
        /// <param name="id">The ID of the generic.</param>
        /// <returns>A partial view containing the result of the process.</returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var generic = _genericRepository.Get(id);
                if (generic == null)
                {
                    messages.Add(new ViewMessage()
                    {
                        Type = MessageType.Error,
                        Message = "Generic cannot be found."
                    });
                    return this.PartialView("_ViewMessageList", messages);
                }

                _genericRepository.Delete(generic);
                messages.Add(new ViewMessage()
                {
                    Type = MessageType.Success,
                    Message = "Generic successfully deleted."
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
        /// This gets all the generics and assign maps it to ListGenericsViewModel
        /// </summary>
        /// <returns>Returns a ListGenericsViewModel object</returns>
        private ListGenericsViewModel GetGenericsList()
        {
            var generics = _genericRepository.GetAll();
            var model = new ListGenericsViewModel();

            var genericsList = Mapper.Map<List<Generic>, List<GenericViewModel>>(generics.ToList());
            model.Generics = genericsList;
            model.Messages = new List<ViewMessage>();

            return model;
        }

        #endregion Private Methods
    }
}