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
    public class GetAllCloth_subHandler : IRequestHandler<GetAllCloth_subQuery, List<Core.DTOs.Cloth_SubCategory_DTO>>
    {
        private readonly ICloth_subRepository _clothSubRepo;

        public GetAllCloth_subHandler(ICloth_subRepository clothSubRepo)
        {
            _clothSubRepo = clothSubRepo;
        }
        public async Task<List<Core.DTOs.Cloth_SubCategory_DTO>> Handle(GetAllCloth_subQuery request, CancellationToken cancellationToken)
        {
            return (List<Core.DTOs.Cloth_SubCategory_DTO>)await _clothSubRepo.GetclothSubNavList();
        }
    }
}
