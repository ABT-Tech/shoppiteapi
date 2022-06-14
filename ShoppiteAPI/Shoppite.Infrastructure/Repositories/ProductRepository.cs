using Microsoft.Data.SqlClient;
using Shoppite.Core.DTOs;
using Shoppite.Core.Entities;
using Shoppite.Core.Extensions;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Infrastructure.Repositories
{
    public class ProductRepository: IProductRepository
    {
        public async Task<List<Product_DTO>> GetproductNavList()
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            List<Product_DTO> product_DTO = new List<Product_DTO>();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "proc_getallproducts";
                command.Parameters.AddWithValue("@org_id", 1);
                var dataReader = await command.ExecuteReaderAsync();
                ExtensionMethods extensionMethods = new ExtensionMethods();
                product_DTO = extensionMethods.DataReaderMapToList<Product_DTO>(dataReader);
                connection.Close();
                return product_DTO;
            }
        }
    }
}
