using Shoppite.Core.Repositories;
using Shoppite.Infrastructure.Data;
using Shoppite.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using Shoppite.Core.DTOs;
using Shoppite.Core.Extensions;
using Shoppite.Core.Entities;
using Shoppite.Core.Model;
namespace Shoppite.Infrastructure.Repositories
{
    public class FirebaseRepository : IFirebaseRepository
    {
        protected readonly Shoppite_masterContext _MasterContext;
        public FirebaseRepository(Shoppite_masterContext dbContext)
        {
            _MasterContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
       
        public async Task<string> SetNotificationToken(FireBaseToken fireBaseToken)
        {
            var findMac = _MasterContext.NotificationsTokens.FirstOrDefault(x => x.MacID == fireBaseToken.MacID);
            if(findMac != null)
            {
                var TokenDetailId = await _MasterContext.NotificationsTokens.FindAsync(findMac.Id);
                TokenDetailId.Token = fireBaseToken.Token;
                _MasterContext.Entry(TokenDetailId).State = EntityState.Detached;
                _MasterContext.Entry(TokenDetailId).State = EntityState.Modified;
                await _MasterContext.SaveChangesAsync();
            }
            else
            {
                Notifications_Token us = new();
                {
                    us.MacID = fireBaseToken.MacID;
                    us.UserId = fireBaseToken.UserID;
                    us.Token = fireBaseToken.Token;
                }
                _MasterContext.NotificationsTokens.Add(us);
                await _MasterContext.SaveChangesAsync();
            }
            return "Success";
        }
    }
}
 