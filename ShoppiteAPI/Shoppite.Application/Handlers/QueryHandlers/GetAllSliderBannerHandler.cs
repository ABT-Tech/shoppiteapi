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
    public class GetAllSliderBannerHandler : IRequestHandler<GetAllSliderBannerQuery,List<Core.DTOs.SliderBanner_DTO>>
    {
        private readonly ISliderBannerRepository _sliderBanRepo;

        public GetAllSliderBannerHandler(ISliderBannerRepository sliderBanRepo)
        {
            _sliderBanRepo = sliderBanRepo;
        }
        public async Task<List<Core.DTOs.SliderBanner_DTO>> Handle(GetAllSliderBannerQuery request, CancellationToken cancellationToken)
        {
            return (List<Core.DTOs.SliderBanner_DTO>)await _sliderBanRepo.GetsliderBannerNavList();
        }
    }
}
