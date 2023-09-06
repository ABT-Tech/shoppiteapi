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
            var findemail = _MasterContext.UsersProfiles.FirstOrDefault(x => x.UserName == userRegistration.Email && x.OrgId == userRegistration.OrgId&&x.Type== "Client");
            if (findemail != null)
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
            var users = _MasterContext.Users.FirstOrDefault(u => u.UserId == userDTO.userId & u.OrgId == userDTO.OrgId);
            var username = _MasterContext.UsersProfiles.FirstOrDefault(a => a.UserName == users.Email & a.OrgId == users.OrgId);
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
            var userDetails = _MasterContext.Users.Where(u => u.Email == password.Email && u.OrgId == password.OrgId).FirstOrDefault();
            if (userDetails != null)
            {
                if (password.Password == password.ConfirmPassword)
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
        public async Task<string> UpdateUserStatus(CustomerInfo cinfo)
        {
            var CustomerInfo = await _MasterContext.Users.Where(x => x.UserId == cinfo.userId && x.OrgId == cinfo.orgId && x.Email == cinfo.Email).FirstOrDefaultAsync();
            var statusDetail = await _MasterContext.UsersProfiles.Where(c => c.OrgId == cinfo.orgId && c.UserName == CustomerInfo.Email).FirstOrDefaultAsync();
            if (statusDetail != null)
            {
                if (statusDetail.Status == "Active")
                {
                    statusDetail.Status = "InActive";
                    _MasterContext.Entry(statusDetail).State = EntityState.Detached;
                    _MasterContext.Entry(statusDetail).State = EntityState.Modified;
                }
                else
                {
                    statusDetail.Status = "Active";
                    _MasterContext.Entry(statusDetail).State = EntityState.Detached;
                    _MasterContext.Entry(statusDetail).State = EntityState.Modified;
                }
                await _MasterContext.SaveChangesAsync();
                return "Succses";
            }
            else
            {
                return "Something Went wrong..";
            }
        }
        public async Task<string> AddCoupan(CoupanDTO coupanDTO)
        {
            Coupan coupan = new();
            {
                coupan.CoupanCode = coupanDTO.CoupanCode;
                coupan.CreatedAt = DateTime.Now;
            }
            _MasterContext.Coupans.Add(coupan);
            await _MasterContext.SaveChangesAsync();
            return "Coupan added";
        }

        public async Task<UserCoupanResponse> ApplyCoupan(CoupanDTO coupans)
        {
            UserCoupanResponse response = new();
            var getCoupanId = await _MasterContext.Coupans.FirstOrDefaultAsync(x => x.CoupanCode == coupans.CoupanCode);

            if (getCoupanId == null)
            {
                response.StatusCode = 1;
                response.Message = "Sorry, Coupon Not Found!!";
                return response;
            }
            else
            {
                var getUserDetail = await _MasterContext.Users.FirstOrDefaultAsync(x => x.UserId == coupans.UserId && x.OrgId == coupans.OrgId);
                if (getUserDetail == null)
                {
                    response.StatusCode = 1;
                    response.Message = "Details not Found please Update Details!!";
                    return response;
                }
                var getUserMobileNum = await _MasterContext.UsersProfiles.FirstOrDefaultAsync(x => x.UserName == getUserDetail.Email && x.OrgId == coupans.OrgId);
                var getVendorDetails = await _MasterContext.UsersProfiles.FirstOrDefaultAsync(x => x.UserName == getUserDetail.Email && x.OrgId == coupans.OrgId&&x.Type=="vendor");
                if (getUserMobileNum.Type=="vendor")
                {
                    response.StatusCode = 1;
                    response.Message = "You are not applicable for this coupon!!";
                    return response;

                }
                if (getUserMobileNum == null)
                {
                    response.StatusCode = 1;
                    response.Message = "Details not Found please Update Details!!";
                    return response;
                }
                var getUserCouponDeail = await _MasterContext.User_Coupans.FirstOrDefaultAsync(x =>x.CoupanId == getCoupanId.CoupanId && x.ContactNumber == getUserMobileNum.ContactNumber);
                if (getUserCouponDeail == null && getCoupanId != null)
                {
                    coupans.CoupanId = getCoupanId.CoupanId;
                    response.CoupanId = coupans.CoupanId;
                    response.Message = coupans.CoupanCode + " Applied!!";
                    response.StatusCode = 0;
                    return response;
                }
                else if (getUserCouponDeail != null)
                {
                    response.StatusCode = 0;
                    if (getUserCouponDeail.ContactNumber == getUserMobileNum.ContactNumber)
                    {
                        response.StatusCode = 1;
                        response.CoupanId = getCoupanId.CoupanId;
                        response.Message = "You Have Reached To Maximum limit for this shop try in another shop!!";
                        return response;
                    }
                    else
                    {
                        response.StatusCode = 1;
                        response.Message = "Applied Coupon Out Of Stock!!";
                        return response;
                    }
                }
                else
                {
                    response.StatusCode = 1;
                    response.Message = "Applied Coupon Out Of Stock!!";
                    return response;
                }
            }
        }
        public async Task<UserRegisteredCheckResponse> UserVerify(UserExistanceDTO users)
        {
            UserRegisteredCheckResponse userResponse = new();
            var findUser = await _MasterContext.UsersProfiles.Where(x => x.UserName == users.Email && x.ContactNumber == users.ContactNumber && x.Type == "Client").ToListAsync();
            var orgNames = string.Empty;
            if (findUser.Count > 0)
            {
                for (int i = 0; i < findUser.Count; i++)
                {
                    var findOrgs = await _MasterContext.Organizations.FirstOrDefaultAsync(x=>x.Id==findUser[i].OrgId);                
                    orgNames+= findOrgs.OrgName + ",";

                    //orgNames = orgNames.Substring(0, orgNames.Length - 1);
                } 
                userResponse.StatusCode = 0;
                orgNames = orgNames.TrimEnd(',');
                userResponse.Message = "you are Registered with "+ orgNames;               
                return userResponse;
            }
            else
            {
                userResponse.StatusCode = 1;
                userResponse.Message = "you are Not Registered with Any Shop";
                return userResponse;
            }
        }
        public async Task<string> RegisterToanotherOrg(UserExistanceDTO users)
        {
            var findUser = await _MasterContext.UsersProfiles.Where(x => x.UserName == users.Email && x.ContactNumber == users.ContactNumber && x.Type == "Client").ToListAsync();

            for (int i = 0; i <= findUser.Count; i++)
            {
                var regsiterdUserDetail = await _MasterContext.Users.FirstOrDefaultAsync(x => x.OrgId == findUser[0].OrgId && x.Email == findUser[0].UserName);
                if (i == 0)
                {
                    User us = new();
                    {
                        us.Username = regsiterdUserDetail.Username;
                        us.Password = regsiterdUserDetail.Password;
                        us.CreatedDate = DateTime.Now;
                        us.Email = regsiterdUserDetail.Email;
                        us.Guid = Guid.NewGuid();
                        us.OrgId = users.OrgId;
                    }
                    _MasterContext.Users.Add(us);
                    await _MasterContext.SaveChangesAsync();
                    UsersProfile profile = new();
                    {
                        var userGuid = _MasterContext.Users.FirstOrDefault(x => x.Guid == us.Guid);
                        profile.Type = "Client";
                        profile.InsertDate = DateTime.Now;
                        profile.ProfileGuid = userGuid.Guid;
                        profile.OrgId = users.OrgId;
                        profile.UserName = regsiterdUserDetail.Email;
                        profile.ContactNumber = findUser[0].ContactNumber;
                        profile.Address = findUser[0].Address;
                        profile.City = findUser[0].City;
                        profile.State = findUser[0].State;
                        profile.Zip = findUser[0].Zip;
                        profile.Status = "Active";
                    }
                    _MasterContext.UsersProfiles.Add(profile);
                    await _MasterContext.SaveChangesAsync();
                    return "You are Registered!!";
                }
            }
            return "";
        }
    }
}
