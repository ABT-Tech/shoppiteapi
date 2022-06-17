using Shoppite.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.Repositories
{
    public interface ICloth_subRepository
    {
        Task<List<Cloth_SubCategory_DTO>> GetclothSubNavList();
    }
}
