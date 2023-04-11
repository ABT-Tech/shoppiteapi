using Shoppite.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Repositories
{
    public interface IFirebaseRepository
    {
        Task<string> SetNotificationToken(FireBaseToken fireBaseToken);
    }
}
