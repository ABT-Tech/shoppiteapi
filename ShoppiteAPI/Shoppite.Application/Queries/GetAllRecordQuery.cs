using MediatR;
using Shoppite.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Queries
{
    public record GetAllCategoriesQuery(int org_id) : IRequest<List<CategoryResponse>>;
}
