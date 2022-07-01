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
        public async Task<Order_DTO> PostOrder(Order_DTO order)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "addorder";
                command.Parameters.AddWithValue("@user_id", order.user_id);
                command.Parameters.AddWithValue("@org_id", order.org_id);
                await command.ExecuteNonQueryAsync();
                return order;
            }
        }
    }
}
