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
    public class UserRepository : IUserRepository
    {
        protected readonly Shoppite_masterContext _MasterContext;
        public UserRepository(Shoppite_masterContext dbContext)
        {
            _MasterContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<List<UserDTO>> GetUserById(int OrgId, int UserId)
        {
            List<UserDTO> UserDTOs = new List<UserDTO>();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_GetUserById";

                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();             
                command.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                command.Parameters.Add(new SqlParameter("@UserId", UserId));
                           
                await this._MasterContext.Database.OpenConnectionAsync();

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        UserDTO userDTO = new UserDTO();
                        userDTO.userId = Convert.ToInt32(UserId);
                        userDTO.OrgId = Convert.ToInt32(OrgId);                      
                        userDTO.ChangeName = result["ChangeName"].ToString();
                        userDTO.ChangeEmail = result["ChangeEmail"].ToString();
                        userDTO.ChangePhoneNumber = result["ChangePhoneNumber"].ToString();
                        userDTO.ChangeAddress = result["ChangeAddress"].ToString();
                        userDTO.ChangeState = result["ChangeState"].ToString();
                        userDTO.ChangeCity = result["ChangeCity"].ToString();
                        userDTO.Zipcode = result["Zipcode"].ToString();

                        UserDTOs.Add(userDTO); 
                    }
                }
            }
            return UserDTOs;

        }
        public async Task UserRegistration(UserRegistrationDTO userRegistration)
        {          
            User us = new();
            {
                us.Username = userRegistration.Username;
                us.Password = userRegistration.Password;
                userRegistration.ConfirmPassword = userRegistration.Password;
                us.CreatedDate = DateTime.Now;
                us.Email = userRegistration.Email;
                us.Guid = Guid.NewGuid();
                us.OrgId = userRegistration.OrgId;
            }
            _MasterContext.Users.Add(us);
            await _MasterContext.SaveChangesAsync();
            UsersProfile profile = new();
            {
                var userGuid = _MasterContext.Users.FirstOrDefault(x => x.Guid == us.Guid);
                profile.Type = "Client";
                profile.InsertDate = DateTime.Now;
                profile.ProfileGuid = userGuid.Guid;
                profile.OrgId = userRegistration.OrgId;
                profile.UserName = userRegistration.Username;
                profile.ContactNumber = userRegistration.ContactNumber;
                profile.Address = userRegistration.Address;
                profile.City = userRegistration.city;
                profile.State = userRegistration.State;
                profile.Zip=userRegistration.Zipcode;
            }
            _MasterContext.UsersProfiles.Add(profile);
            await _MasterContext.SaveChangesAsync();
        }
    }
}
 