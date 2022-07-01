using Shoppite.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Repositories
{
    public interface IOrderRepository
    {
        Task<Order_DTO> PostOrder(Order_DTO order);
    }
}
