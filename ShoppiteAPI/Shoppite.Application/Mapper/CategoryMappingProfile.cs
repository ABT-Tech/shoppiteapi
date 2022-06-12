using AutoMapper;
using Shoppite.Application.Commands;
using Shoppite.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Mapper
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Shoppite.Core.Entities.Category, CategoryResponse>().ReverseMap();
            CreateMap<Shoppite.Core.Entities.Category, CreateCategoryCommand>().ReverseMap();
        }
    }
}
