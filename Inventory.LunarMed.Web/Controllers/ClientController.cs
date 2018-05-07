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
            return View(GetListClientsModel());
        }

        [HttpGet]
        public ActionResult List()
        {
            var model = new ClientViewModel();

            return this.PartialView("_ListClients", GetListClientsModel().Clients);
        }

        // GET: Client/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new ClientViewModel();
            model.ClientId = 0;

            return this.PartialView("_AddNewClientModal", model);
        }

        // POST: Client/Create
        [HttpPost]
        public ActionResult Create(ClientViewModel model)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var client = Mapper.Map<ClientViewModel, Client>(model);
                _clientRepository.Add(client);

                messages = new List<ViewMessage>
                {
                    new ViewMessage()
                    {
                        Type = MessageType.Success,
                        Message = "New client successfully saved."
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

        // GET: Client/Edit/5
        public ActionResult Edit(int id)
        {
            var client = _clientRepository.Get(id);
            var model = Mapper.Map<Client, ClientViewModel>(client);

            return PartialView("_AddNewClientModal", model);
        }

        // POST: Client/Edit/5
        [HttpPost]
        public ActionResult Edit(ClientViewModel model)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var client = _clientRepository.Get(model.ClientId);
                if(client != null)
                {
                    client.Name = model.Name;
                    client.Address = model.Address;
                    client.ContactNumber = model.ContactNumber;
                    _clientRepository.Update(client);
                }
                else
                {
                    messages = new List<ViewMessage>
                    {
                        new ViewMessage()
                        {
                            Type = MessageType.Error,
                            Message = "Client cannot be found."
                        }
                    };
                    return this.PartialView("_ViewMessageList", messages);
                }


                messages = new List<ViewMessage>
                {
                    new ViewMessage()
                    {
                        Type = MessageType.Success,
                        Message = "Client successfully updated."
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

        #region Private Methods

        private ListClientsViewModel GetListClientsModel()
        {
            var clients = _clientRepository.GetAll();
            var model = new ListClientsViewModel();

            var clientList = Mapper.Map<List<Client>, List<ClientViewModel>>(clients.ToList());
            model.Clients = clientList;
            model.Messages = new List<ViewMessage>();

            return model;
        }

        #endregion Private Methods

    }
}
