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
    public class GetNotificationDetailsHandler : IRequestHandler<GetNotificationDetails, List<NotificationsDataDTO>>
    {
        private readonly IFirebaseRepository _firebase;
        public GetNotificationDetailsHandler(IFirebaseRepository firebase)
        {
            _firebase = firebase;
        }
        public async Task<List<NotificationsDataDTO>> Handle(GetNotificationDetails request, CancellationToken cancellationToken)
        {
            return await _firebase.GetNotificationDetails(request.NotificationID);
        }
    }
}
