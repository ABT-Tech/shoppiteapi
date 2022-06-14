using Shoppite.Application.Queries;
using MediatR;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Shoppite.Application.Handlers.QueryHandlers
{
    public class GetAllSidebarHandler : IRequestHandler<GetAllSidebarQuery, List<Core.DTOs.Sidebar_DTO>>
    {
        private readonly ISidebarRepository _sidebarRepo;

        public GetAllSidebarHandler(ISidebarRepository sidebarRepo)
        {
            _sidebarRepo = sidebarRepo;
        }
        public async Task<List<Core.DTOs.Sidebar_DTO>> Handle(GetAllSidebarQuery request, CancellationToken cancellationToken)
        {
            return (List<Core.DTOs.Sidebar_DTO>)await _sidebarRepo.GetsidebarNavList();
        }
    }
}
