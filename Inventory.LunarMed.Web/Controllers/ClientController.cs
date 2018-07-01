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
        /// <summary>
        /// Gets all the clients and pass it on the view
        /// </summary>
        /// <returns>Returns a view containing all the clients</returns>
        public ActionResult Index(string type)
        {
            var isSupplier = (type == "client") ? false : true;
            return View(GetListClientsModel(isSupplier));
        }
        
        // GET: Client/List
        /// <summary>
        /// Gets the list of clients and pass it in our modal
        /// </summary>
        /// <returns>Returns a partial view that contains the list of all clients</returns>
        [HttpGet]
        public ActionResult List(string type)
        {
            var isSupplier = (type == "client") ? false : true;
            return this.PartialView("_ListClients", GetListClientsModel(isSupplier).Clients);
        }

        // GET: Client/Create
        /// <summary>
        /// Displays a partial view used for creating a client
        /// </summary>
        /// <returns>Returns a partial view used for creating a client</returns>
        [HttpGet]
        public ActionResult Create(string type)
        {
            var isSupplier = (type == "client") ? false : true;
            var model = new ClientViewModel();
            model.ClientId = 0;
            model.IsSupplier = isSupplier;

            return this.PartialView("_AddOrEditClientModal", model);
        }

        // POST: Client/Create
        /// <summary>
        /// This saves a new client using the passed client model
        /// </summary>
        /// <param name="model">The ClientViewModel object.</param>
        /// <returns>A partial view containing the result of the process.</returns>
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
        /// <summary>
        /// This displays the details of the selected client that is about to be edited
        /// </summary>
        /// <param name="id">The Client ID</param>
        /// <returns>Returns a partial view containing the details of the client.</returns>
        public ActionResult Edit(int id)
        {
            var client = _clientRepository.Get(id);
            var model = Mapper.Map<Client, ClientViewModel>(client);

            return PartialView("_AddOrEditClientModal", model);
        }

        // POST: Client/Edit/5
        /// <summary>
        /// This updates the client based on the passed Client model
        /// </summary>
        /// <param name="model">The ClientViewModel object.</param>
        /// <returns>A partial view containing the result of the process.</returns>
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
                    client.IsSupplier = model.IsSupplier;
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

        // POST: Client/Delete
        /// <summary>
        /// This deletes passed client id
        /// </summary>
        /// <param name="id">The ID of the client.</param>
        /// <returns>A partial view containing the result of the process.</returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var messages = new List<ViewMessage>();
            try
            {
                var client = _clientRepository.Get(id);
                if (client == null)
                {
                    messages.Add(new ViewMessage()
                    {
                        Type = MessageType.Error,
                        Message = "Client cannot be found."
                    });
                    return this.PartialView("_ViewMessageList", messages);
                }

                _clientRepository.Delete(client);
                messages.Add(new ViewMessage()
                {
                    Type = MessageType.Success,
                    Message = "Client successfully deleted."
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
        /// This gets all the clients and assign maps it to ListClientsViewModel
        /// </summary>
        /// <returns>Returns a ListClientsViewModel object</returns>
        private ListClientsViewModel GetListClientsModel(bool isSupplier)
        {
            var clients = _clientRepository.List(i => i.IsSupplier == isSupplier);
            var model = new ListClientsViewModel();

            var clientList = Mapper.Map<List<Client>, List<ClientViewModel>>(clients.ToList());
            model.Clients = clientList;
            model.Messages = new List<ViewMessage>();

            return model;
        }

        #endregion Private Methods

    }
}
