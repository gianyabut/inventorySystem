using AutoMapper;
using Inventory.LunarMed.Data.Entities;
using Inventory.LunarMed.Web.Models;
using Microsoft.Owin;
using Owin;
using System;
using System.Globalization;

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
                cfg.CreateMap<UnitSize, UnitSizeViewModel>();
                cfg.CreateMap<UnitSizeViewModel, UnitSize>();
                cfg.CreateMap<ProductGroup, ProductGroupViewModel>();
                cfg.CreateMap<ProductGroupViewModel, ProductGroup>();
                cfg.CreateMap<Product, ProductViewModel>()
                    .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate.ToString("MM/dd/yyyy")))
                    .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => src.PurchaseDate.ToString("MM/dd/yyyy")));
                cfg.CreateMap<ProductViewModel, Product>()
                    .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => DateTime.ParseExact(src.ExpirationDate, "MM/dd/yyyy", CultureInfo.InvariantCulture)))
                    .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => DateTime.ParseExact(src.PurchaseDate, "MM/dd/yyyy", CultureInfo.InvariantCulture)));
            });
        }
    }
}
