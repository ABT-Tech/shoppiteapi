using AutoMapper;
using Shoppite.Application.Responses;
using Shoppite.Core.DTOs;
using Shoppite.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Mapper
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<ShoppiteMaterDtoMapper>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }
    public class ShoppiteMaterDtoMapper : AutoMapper.Profile
    {
        public ShoppiteMaterDtoMapper()
        {
            CreateMap<CategoryMaster, CategoryResponse>().ReverseMap();
            CreateMap<OrganizationDTO, OrganizationResponse>().ReverseMap();
            CreateMap<ProductsDTO, ProductResponse>().ReverseMap();
            CreateMap<CartDTO, CartResponse>().ReverseMap();
            CreateMap<MyOrdersDTO, MyOrderResponse>().ReverseMap();
            CreateMap<UserDTO, UserResponse>().ReverseMap();
            CreateMap<AddressResponse, ChangeAddress>().ReverseMap();
        }
    }
 }
