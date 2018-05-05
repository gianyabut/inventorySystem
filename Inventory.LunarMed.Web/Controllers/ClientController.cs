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
    public class ClientController : Controller
    {
        private readonly IGenericRepository<Client> _clientRepository;

        public ClientController(IGenericRepository<Client> clientRepository)
        {
            _clientRepository = clientRepository;
        }

        // GET: Client
        public ActionResult Index()
        {
            var clients = _clientRepository.GetAll();
            var clientList = Mapper.Map<List<Client>, List<ClientViewModel>>(clients.ToList());

            return View(clientList);
        }

        // GET: Client/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // POST: Client/Create
        [HttpPost]
        public JsonResult Create(ClientViewModel model)
        {
            var response = new ResponseViewModel();

            try
            {
                var client = Mapper.Map<ClientViewModel, Client>(model);
                _clientRepository.Add(client);

                response.IsSuccessful = true;
                var messages = new List<ViewMessage>
                {
                    new ViewMessage()
                    {
                        Type = MessageType.Success,
                        Message = "Client successfully saved."
                    }
                };

                return Json(response);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                var messages = new List<ViewMessage>
                {
                    new ViewMessage()
                    {
                        Type = MessageType.Error,
                        Message = ex.Message.ToString()
                    }
                };

                return Json(response);
            }
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Client/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Client/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
