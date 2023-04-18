using MediatR;
using Shoppite.Application.Mapper;
using Shoppite.Application.Queries;
using Shoppite.Application.Responses;
using Shoppite.Core.DTOs;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shoppite.Application.Handlers.QueryHandlers
{
    public class GetDeviceDetailsHandler : IRequestHandler<GetDeviceListToSendNotifications, List<DeviceListDTO>>
    {
        private readonly IFirebaseRepository _firebase;
        public GetDeviceDetailsHandler(IFirebaseRepository firebase)
        {
            _firebase = firebase;
        }
        public async Task<List<DeviceListDTO>> Handle(GetDeviceListToSendNotifications request, CancellationToken cancellationToken)
        {
            return await _firebase.GetDeviceListToSendNotifications(request.Type,request.UserID);
        }
    }
}
