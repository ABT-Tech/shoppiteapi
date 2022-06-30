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
    public class UserRepository : IUserRepository
    {
        public async Task<Users_DTO> PostUserSignup(Users_DTO users_DTO)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "adduserSignup";
                command.Parameters.AddWithValue("@org_id", users_DTO.org_id);
                command.Parameters.AddWithValue("@user_name", users_DTO.user_name);
                command.Parameters.AddWithValue("@email", users_DTO.email);
                command.Parameters.AddWithValue("@password", users_DTO.password);
                await command.ExecuteNonQueryAsync();
                return users_DTO;
            }
        }
    }
}
