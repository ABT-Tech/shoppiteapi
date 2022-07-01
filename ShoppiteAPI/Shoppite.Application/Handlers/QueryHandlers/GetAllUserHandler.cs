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
    public class GetAllUserHandler: IRequestHandler<GetAllUserQuery, List<Core.DTOs.UserInfo_DTO>>
    {
        private readonly IUserRepository _productRepository;

        public GetAllUserHandler(IUserRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<Core.DTOs.UserInfo_DTO>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            return (List<Core.DTOs.UserInfo_DTO>)await _productRepository.GetAlluser(request.org_id,request.id);
        }
    }
}
