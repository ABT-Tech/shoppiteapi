using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Repositories
{
    public interface IJwtAuth
    {
        Task<string> Authentication(string username, string password);

    }
}
