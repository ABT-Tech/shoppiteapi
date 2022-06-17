using MediatR;
using Shoppite.Application.Queries;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shoppite.Application.Handlers.QueryHandlers
{
    public class GetAllCenterBannerHandler : IRequestHandler<GetAllCenterBannerQuery, List<Core.DTOs.Center_Banner_DTO>>
    {
        private readonly ICenterBannerRepository _centerBanRepo;

        public GetAllCenterBannerHandler(ICenterBannerRepository centerBanRepo)
        {
            _centerBanRepo = centerBanRepo;
        }
        public async Task<List<Core.DTOs.Center_Banner_DTO>> Handle(GetAllCenterBannerQuery request, CancellationToken cancellationToken)
        {
            return (List<Core.DTOs.Center_Banner_DTO>)await _centerBanRepo.GetcenterBannerNavList();
        }
    }
}
