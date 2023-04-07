using Shoppite.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Repositories
{
    public interface IJwtAuth
    {
        Task<Users_DTO> Authentication(string username, string password,string type,int OrgID);

    }
}
