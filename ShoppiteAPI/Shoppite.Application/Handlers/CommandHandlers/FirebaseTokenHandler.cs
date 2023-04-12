using MediatR;
using Shoppite.Application.Commands;
using Shoppite.Core.DTOs;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shoppite.Application.Handlers.CommandHandlers
{
    public class FirebaseTokenHandler : IRequestHandler<AddFirebaseToken, string>
    {
        private readonly IFirebaseRepository _firebase;

        public FirebaseTokenHandler(IFirebaseRepository firebase)
        {
            _firebase = firebase;
        }
        public async Task<string> Handle(AddFirebaseToken request, CancellationToken cancellationToken)
        {
            return await _firebase.SetNotificationToken(request.fireBaseToken); 
        }
    }
}
