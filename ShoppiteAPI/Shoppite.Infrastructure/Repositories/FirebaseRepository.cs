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
                TokenDetailId.InsertedAt = DateTime.Now;

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
                    us.InsertedAt = DateTime.Now;
                }
                _MasterContext.NotificationsTokens.Add(us);
                await _MasterContext.SaveChangesAsync();
            }
            return "Success";
        }
        public async Task<List<NotificationsDataDTO>> GetNotificationDetails(int NotificationID)
        {
            List<NotificationsDataDTO> notificationsDataDTOs = new();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "Proc_GetNotificationDetails";
                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                command.Parameters.Add(new SqlParameter("@NotificationID", NotificationID));
                await this._MasterContext.Database.OpenConnectionAsync();
                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        NotificationsDataDTO notificationsData = new NotificationsDataDTO();
                        notificationsData.Id = Convert.ToInt32(result["Id"]);
                        notificationsData.Title = result["Title"].ToString();
                        notificationsData.OrgId = Convert.ToInt32(result["OrgId"].ToString());
                        notificationsData.UserID = result["UserID"]!=DBNull.Value? Convert.ToInt32(result["UserID"].ToString()):0;
                        notificationsData.org_name = result["org_name"].ToString();
                        notificationsData.Details = result["Details"].ToString();
                        notificationsDataDTOs.Add(notificationsData);
                    }
                }
            }
            return notificationsDataDTOs;
        }
        public async Task<List<DeviceListDTO>> GetDeviceListToSendNotifications(string Type,int UserID)
        {
            List<DeviceListDTO> deviceListDTOs = new();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "Proc_GetDeviceListToSendNotifications";
                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                command.Parameters.Add(new SqlParameter("@Type", Type));
                command.Parameters.Add(new SqlParameter("@UserID", UserID));
                await this._MasterContext.Database.OpenConnectionAsync();
                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        DeviceListDTO deviceList = new DeviceListDTO();
                        deviceList.ID = Convert.ToInt32(result["ID"]);
                        deviceList.IsIos = Convert.ToBoolean(result["IsIos"].ToString());
                        deviceList.Token = result["Token"].ToString();
                        deviceListDTOs.Add(deviceList);
                    }
                }
            }
            return deviceListDTOs;
        }

        public async Task<string> UpdateNotificationStatus(int NotifyID)
        {
            try
            {
                var notificationData = _MasterContext.Notifications.Where(x => x.Id == NotifyID).FirstOrDefault();
                notificationData.Sent = true;
                notificationData.SentDate = DateTime.Now;
                _MasterContext.Entry(notificationData.Id).State = EntityState.Detached;
                _MasterContext.Entry(notificationData.Id).State = EntityState.Modified;
                await _MasterContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
           
            return "success";
        }
    }
}
 