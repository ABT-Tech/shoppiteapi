using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Application.Queries
{
    public class GetAllSidebarQuery : IRequest<List<Core.DTOs.Sidebar_DTO>>
    {

    }
}
