using AutoMapper;
using Inventory.LunarMed.Data.Entities;
using Inventory.LunarMed.Web.Models;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Inventory.LunarMed.Web.Startup))]
namespace Inventory.LunarMed.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            Mapper.Initialize(cfg => {
                cfg.CreateMap<Client, ClientViewModel>();
                cfg.CreateMap<ClientViewModel, Client>();
            });
        }
    }
}
