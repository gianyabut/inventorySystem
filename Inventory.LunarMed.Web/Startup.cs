using AutoMapper;
using Inventory.LunarMed.Data.Entities;
using Inventory.LunarMed.Web.Models;
using Inventory.LunarMed.Web.Models.DTO;
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
                cfg.CreateMap<Stock, StockViewModel>()
                    .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate.ToString("MM/dd/yyyy")))
                    .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => src.PurchaseDate.ToString("MM/dd/yyyy")))
                    .ForMember(dest => dest.UnitSizeName, opt => opt.MapFrom(src => src.UnitSize.Name))
                    .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.Name))
                    .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name));
                cfg.CreateMap<StockViewModel, Stock>()
                    .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => DateTime.ParseExact(src.ExpirationDate, "MM/dd/yyyy", CultureInfo.InvariantCulture)))
                    .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => DateTime.ParseExact(src.PurchaseDate, "MM/dd/yyyy", CultureInfo.InvariantCulture)));

                cfg.CreateMap<Price, PriceViewModel>()
                   .ForMember(dest => dest.UnitSizeName, opt => opt.MapFrom(src => src.UnitSize.Name))
                   .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.Name))
                   .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name));
                cfg.CreateMap<PriceViewModel, Price>();

                cfg.CreateMap<OrderViewModel, Order>()
                    .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => DateTime.ParseExact(src.DueDate, "MM/dd/yyyy", CultureInfo.InvariantCulture)));
                cfg.CreateMap<Order, OrderViewModel>()
                    .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate.ToString("MM/dd/yyyy")))
                    .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.Name));

                cfg.CreateMap<OrderDetailViewModel, OrderDetails>();
                cfg.CreateMap<OrderDetails, OrderDetailViewModel>();

                cfg.CreateMap<CollectionViewModel, Collection>();
                cfg.CreateMap<Collection, CollectionViewModel>()
                    .ForMember(dest => dest.SalesInvoice, opt => opt.MapFrom(src => src.Order.SalesInvoice));

                cfg.CreateMap<DisplayOrderViewModel, Order>()
                   .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => DateTime.ParseExact(src.DueDate, "MM/dd/yyyy", CultureInfo.InvariantCulture)));
                cfg.CreateMap<Order, DisplayOrderViewModel>()
                    .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate.ToString("MM/dd/yyyy")))
                    .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.Name));

                cfg.CreateMap<DisplayOrderDetailsViewModel, OrderDetails>();
                cfg.CreateMap<OrderDetails, DisplayOrderDetailsViewModel>()
                    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Stock.Brand.Name))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

                // Generic -> GenericViewModel and vice-versa
                cfg.CreateMap<Generic, GenericViewModel>();
                cfg.CreateMap<GenericViewModel, Generic>();

                // Brand -> BrandViewModel and vice-versa
                cfg.CreateMap<Brand, BrandViewModel>()
                    .ForMember(dest => dest.GenericName, opt => opt.MapFrom(src => src.Generic.Name));
                cfg.CreateMap<BrandViewModel, Brand>();

            });
        }
    }
}
