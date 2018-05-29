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
    public class BrandController : Controller
    {
        private readonly IGenericRepository<Brand> _brandRepository;
        private readonly IGenericRepository<Generic> _genericRepository;

        public BrandController(IGenericRepository<Brand> brandRepository, IGenericRepository<Generic> genericRepository)
        {
            _brandRepository = brandRepository;
            _genericRepository = genericRepository;
        }

        // GET: Brand
        /// <summary>
        /// Gets all the generics and pass it on the view
        /// </summary>
        /// <returns>Returns a view containing all the generics</returns>
        public ActionResult Index()
        {
            return View(GetBrandsList());
        }

        // GET: Brand/List
        /// <summary>
        /// Gets the list of brands and pass it in our modal
        /// </summary>
        /// <returns>Returns a partial view that contains the list of all brands</returns>
        [HttpGet]
        public ActionResult List()
        {
            return this.PartialView("_ListBrands", GetBrandsList().Brands);
        }

        // GET: Brand/Create
        /// <summary>
        /// Displays a partial view used for creating a brand
        /// </summary>
        /// <returns>Returns a partial view used for creating a brand</returns>
        [HttpGet]
        public ActionResult Create()
        {
            var model = new BrandViewModel
            {
                GenericId = 0,
                GenericList = GetGenericsList()
            };

            return this.PartialView("_AddOrEditBrandModal", model);
        }

        // POST: Brand/Create
        /// <summary>
        /// This saves a new brand using the parameter brand model
        /// </summary>
        /// <param name="model">The BrandViewModel object.</param>
        /// <returns>A partial view containing the result of the process.</returns>
        [HttpPost]
        public ActionResult Create(BrandViewModel model)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var brand = Mapper.Map<BrandViewModel, Brand>(model);
                _brandRepository.Add(brand);

                messages = new List<ViewMessage>
                {
                    new ViewMessage()
                    {
                        Type = MessageType.Success,
                        Message = "New brand successfully saved."
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

        // GET: Brand/Edit/5
        /// <summary>
        /// This displays the details of the selected brand that is about to be edited
        /// </summary>
        /// <param name="id">The Brand ID</param>
        /// <returns>Returns a partial view containing the details of the brand.</returns>
        public ActionResult Edit(int id)
        {
            var brand = _brandRepository.Get(id);
            var model = Mapper.Map<Brand, BrandViewModel>(brand);
            model.GenericList = GetGenericsList();

            return PartialView("_AddOrEditBrandModal", model);
        }

        // POST: Brand/Edit/5
        /// <summary>
        /// This updates the brand based on the passed brand model
        /// </summary>
        /// <param name="model">The BrandViewModel object.</param>
        /// <returns>A partial view containing the result of the process.</returns>
        [HttpPost]
        public ActionResult Edit(BrandViewModel model)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var brand = _brandRepository.Get(model.BrandId);
                if (brand != null)
                {
                    brand.Name = model.Name;
                    brand.Description = model.Description;
                    brand.GenericId = model.GenericId;
                    _brandRepository.Update(brand);
                }
                else
                {
                    messages = new List<ViewMessage>
                    {
                        new ViewMessage()
                        {
                            Type = MessageType.Error,
                            Message = "Brand cannot be found."
                        }
                    };
                    return this.PartialView("_ViewMessageList", messages);
                }


                messages = new List<ViewMessage>
                {
                    new ViewMessage()
                    {
                        Type = MessageType.Success,
                        Message = "Brand successfully updated."
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

        // POST: Brand/Delete
        /// <summary>
        /// This deletes passed brand id
        /// </summary>
        /// <param name="id">The ID of the brand.</param>
        /// <returns>A partial view containing the result of the process.</returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var brand = _brandRepository.Get(id);
                if (brand == null)
                {
                    messages.Add(new ViewMessage()
                    {
                        Type = MessageType.Error,
                        Message = "Brand cannot be found."
                    });
                    return this.PartialView("_ViewMessageList", messages);
                }

                _brandRepository.Delete(brand);
                messages.Add(new ViewMessage()
                {
                    Type = MessageType.Success,
                    Message = "Brand successfully deleted."
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
        /// This gets all the brands and assign maps it to ListBrandsViewModel
        /// </summary>
        /// <returns>Returns a ListBrandsViewModel object</returns>
        private ListBrandsViewModel GetBrandsList()
        {
            var brands = _brandRepository.GetAll();
            var model = new ListBrandsViewModel();

            var brandList = Mapper.Map<List<Brand>, List<BrandViewModel>>(brands.ToList());
            model.Brands = brandList;
            model.Messages = new List<ViewMessage>();

            return model;
        }

        /// <summary>
        /// Get all generics and assign to SelectListItem variable
        /// </summary>
        /// <returns>The SelectListItem variable of all Generic</returns>
        private List<SelectListItem> GetGenericsList()
        {
            var genericListItem = new List<SelectListItem>();
            foreach (var generic in _genericRepository.GetAll())
            {
                genericListItem.Add(new SelectListItem()
                {
                    Text = generic.Name,
                    Value = generic.GenericId.ToString()
                });
            }

            return genericListItem;
        }

        #endregion Private Methods
    }
}