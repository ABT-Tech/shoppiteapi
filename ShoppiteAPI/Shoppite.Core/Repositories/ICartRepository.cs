using Shoppite.Core.DTOs;
using Shoppite.Core.Model;
using Shoppite.Core.Repositories.Base;
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

        //custom operations here
        Task<string> AddToCart(CartRequest Cart);
        Task<List<ChangeAddress>> GetAddressByUserId(int OrgId, int UserId);
        Task RemovefromFavourite(int ProductId,int UserId,int OrgId, int? SpecificationId);
        Task RemoveFromCart(int userid, int proid, int orgid, int? SpecificationId);
        Task<CartDTO> GetNoOfItemsInCart(int UserId, int OrgId);
    }
}
