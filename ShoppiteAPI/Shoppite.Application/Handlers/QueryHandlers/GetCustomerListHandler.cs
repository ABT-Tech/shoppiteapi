using AutoMapper;
using MediatR;
using Shoppite.Application.Mapper;
using Shoppite.Application.Queries;
using Shoppite.Application.Responses;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shoppite.Application.Handlers.QueryHandlers
{
    public class GetCustomerListHandler : IRequestHandler<GetCustomerDetails, List<CustomerInfoResponse>>
    {
        public readonly IUserRepository _userRepository;
        public GetCustomerListHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<CustomerInfoResponse>> Handle(GetCustomerDetails request, CancellationToken cancellationToken)
        {
            var customerDetails = await _userRepository.GetCustomerDetails(request.OrgId);
            var mapper = ObjectMapper.Mapper.Map<List<CustomerInfoResponse>>(customerDetails);
            return mapper;
        }
    }
}
