using Microsoft.Data.SqlClient;
using Shoppite.Core.DTOs;
using Shoppite.Core.Entities;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public async Task<Order_DTO> PostOrder(List<CartProduct> orders)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            foreach (var order in orders)
            {
                using (var connection = new SqlConnection(generalDbContext.ConnectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "addorder";
                    command.Parameters.AddWithValue("@user_id", order.user_id);
                    command.Parameters.AddWithValue("@org_id", order.org_id);
                    command.Parameters.AddWithValue("@prod_quantity", order.prod_quantity);
                    command.Parameters.AddWithValue("@category_id", order.category_id);
                    command.Parameters.AddWithValue("@sub_ctg_id", order.sub_ctg_id);
                    command.Parameters.AddWithValue("@product_id", order.product_id);
                    command.Parameters.AddWithValue("@cproduct_name", order.cproduct_name);
                    command.Parameters.AddWithValue("@cproduct_price", order.cproduct_price);
                    command.Parameters.AddWithValue("@cproduct_image", order.cproduct_image);
                    await command.ExecuteNonQueryAsync();
                    connection.Close();
                }
            }
            return null;
        }
    }
}
