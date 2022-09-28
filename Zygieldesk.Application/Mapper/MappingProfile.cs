using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Functions.Categories.Queries.GetCategoryList;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryListViewModel>();
        }
    }
}
