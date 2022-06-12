using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Queries
{
    public class GetAllCategoryQuery : IRequest<List<Core.DTOs.SubCategory_Category_DTO>>
    {

    }
}
