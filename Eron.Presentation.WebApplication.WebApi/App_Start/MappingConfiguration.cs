using System;
using AutoMapper.Attributes;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Financial.Order.Tariff.Dto;
using Eron.Core.Entities.Financial.Order;
using Eron.Core.Entities.Financial.Shop;
using Eron.Core.Entities.User;
using Eron.Core.Infrastructure;
using Eron.Core.ValueObjects;
using Nelibur.ObjectMapper;

namespace Eron.Presentation.WebApplication.WebApi
{
    public static class MappingConfig
    {
        public static void RegisterTinyMapperMappings()
        {
            TinyMapper.Bind<Entity<int>, EntityDto<int>>();
            TinyMapper.Bind<Entity<long>, EntityDto<long>>();
            TinyMapper.Bind<Entity<Guid>, EntityDto<Guid>>();
            TinyMapper.Bind<Entity<string>, EntityDto<string>>();

            TinyMapper.Bind<TariffItem, TariffItemDto>();
        }

        public static void RegisterAutoMapperMappings()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                typeof(EntityDto<object>).Assembly.MapTypes(config);      //or use typeof(Program).GetTypeInfo().Assembly if using .NET Standard
                config.CreateMap<Product, Product>()
                    .ForMember(dest => dest.Properties, opt => opt.Ignore())
                    .ForMember(dest => dest.Prices, opt => opt.Ignore())
                    .ForMember(dest => dest.ProductImages, opt => opt.Ignore());
                config.CreateMap<ProductProperty, ProductProperty>();
                config.CreateMap<ApplicationUser, ApplicationUser>();
                config.CreateMap<TariffCategory, TariffCategory>();
                config.CreateMap<Tariff, Tariff>();
                config.CreateMap(typeof(PagedListResult<>), typeof(PagedListResult<>));
            });
        }
    }
}