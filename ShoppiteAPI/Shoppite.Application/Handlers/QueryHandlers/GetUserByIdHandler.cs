using Shoppite.Application.Queries;
using Shoppite.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shoppite.Application.Responses;
using Shoppite.Application.Mapper;

namespace Shoppite.Application.Handlers.QueryHandlers
{
   
      public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, List<UserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var products = await _userRepository.GetUserById(request.org_id, request.user_id);
            var mapper = ObjectMapper.Mapper.Map<List<UserResponse>>(products);
            return mapper;   
        }
    }
}
