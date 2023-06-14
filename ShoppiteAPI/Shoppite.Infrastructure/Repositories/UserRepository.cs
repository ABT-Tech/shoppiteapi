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
                    if (result != null)
                    {
                        while (await result.ReadAsync())
                        {
                            UserDTO userDTO = new();
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
            }
            return UserDTOs;

        }
        public async Task<string> UserRegistration(UserRegistrationDTO userRegistration)
        {
            var findemail = _MasterContext.Users.FirstOrDefault(x => x.Email == userRegistration.Email&&x.OrgId==userRegistration.OrgId);
            if(findemail!=null)
            {
              return "User Exist!! Please Try with new Email";
                
            }
            else
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
                    profile.UserName = userRegistration.Email;
                    profile.ContactNumber = userRegistration.ContactNumber;
                    profile.Address = userRegistration.Address;
                    profile.City = userRegistration.city;
                    profile.State = userRegistration.State;
                    profile.Zip = userRegistration.Zipcode;
                    profile.Status = "Active";
                }
                _MasterContext.UsersProfiles.Add(profile);
                await _MasterContext.SaveChangesAsync();
                return "You are Registered!!";
            }
            
        }
        public async Task UpdateUserProfile(UserDTO userDTO)
        {
            var users =  _MasterContext.Users.FirstOrDefault(u=>u.UserId==userDTO.userId & u.OrgId==userDTO.OrgId);
            var username = _MasterContext.UsersProfiles.FirstOrDefault(a => a.UserName == users.Email & a.OrgId==users.OrgId);
            if (users != null)
            {
                users.Username = userDTO.ChangeName;         
                users.Email = userDTO.ChangeEmail;   
                _MasterContext.Entry(users).State = EntityState.Detached;          
                _MasterContext.Entry(users).State = EntityState.Modified;
                await _MasterContext.SaveChangesAsync();
            }

            UsersProfile profile = await _MasterContext.UsersProfiles.FindAsync(username.ProfileId);
            if (profile != null)
            {
                profile.UserName = userDTO.ChangeEmail;
                profile.ContactNumber = userDTO.ChangePhoneNumber;
                profile.Address = userDTO.ChangeAddress;
                profile.City = userDTO.ChangeCity;
                profile.State = userDTO.ChangeState;
                profile.Zip = userDTO.Zipcode;

                _MasterContext.Entry(profile).State = EntityState.Detached;          
                _MasterContext.Entry(profile).State = EntityState.Modified;
                await _MasterContext.SaveChangesAsync();
            }
        }
        public async Task<string> ForgetPassword(ForgetPassword password)
        {
            var userDetails =  _MasterContext.Users.Where(u => u.Email == password.Email && u.OrgId == password.OrgId).FirstOrDefault();
            if(userDetails!=null)
            {
                if ( password.Password == password.ConfirmPassword)
                {
                    userDetails.Password = password.Password;
                    _MasterContext.Entry(userDetails).State = EntityState.Detached;
                    _MasterContext.Entry(userDetails).State = EntityState.Modified;
                    await _MasterContext.SaveChangesAsync();
                    return "Password Changed Successfully!!";
                }
                else
                {
                    return "Password and Confirm Password Should match";
                }
            }
            else
            {
                return "User Not Found!!";
            }
        }
        public async Task<List<CustomerInfo>> GetCustomerDetails(int OrgId)
        {
            List<CustomerInfo> customerInfo = new();
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_GetCustomerDetailsByOrgId";

                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                command.Parameters.Add(new SqlParameter("@OrgId", OrgId));

                await this._MasterContext.Database.OpenConnectionAsync();
                using var result = await command.ExecuteReaderAsync();
                if (result != null)
                {
                    while (await result.ReadAsync())
                    {
                        CustomerInfo info = new();
                        info.userId = Convert.ToInt32(result["UserId"]);
                        info.orgId = Convert.ToInt32(OrgId);
                        info.Username = result["Username"].ToString();
                        info.Email = result["Email"].ToString();
                        info.Status = result["Status"].ToString();
                        if (info.Status == "Active")
                        {
                            info.Active = true;
                        }
                        customerInfo.Add(info);
                    }
                }
            }
            return customerInfo;
        }
    }
}
 