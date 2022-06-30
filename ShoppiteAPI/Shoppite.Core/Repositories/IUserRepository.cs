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
        Task<Users_DTO> PostUserSignup(Users_DTO users_DTO);
        //Task<Users_DTO> GetUserInfo(int org_id, int id);
    }
}
