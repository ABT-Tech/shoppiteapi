using Shoppite.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Repositories
{
   public interface ICartRepository
   {
        Task<List<CartDTO>> GetCartDetails(int OrgId,int UserId);
        Task AddtoFavourite(Favourite favourite);
        Task<List<ChangeAddress>> GetAddressByUserId(int OrgId, int UserId);
        Task RemovefromFavourite(int ProductId,int UserId,int OrgId);
    }
}
