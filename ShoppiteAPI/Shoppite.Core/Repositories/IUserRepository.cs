using Shoppite.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Repositories
{
    public interface IUserRepository
    {
        Task<List<DTOs.UserDTO>> GetUserById(int org_id, int user_id);
        Task<string> UserRegistration(UserRegistrationDTO registrationDTO);
        Task UpdateUserProfile(UserDTO users);
        Task<string> ForgetPassword(ForgetPassword password);
        Task<List<CustomerInfo>> GetCustomerDetails(int OrgId);
    }
}
