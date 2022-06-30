using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Shoppite.Core.DTOs;
using Shoppite.Core.Entities;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Infrastructure.Repositories
{
    public class AuthRepository : IJwtAuth
    {
        private string key = "abcdefghijklmnopqrstuvwxyz1234567890";
        
        public async Task<Users_DTO> Authentication(string username, string password,int OrgId)
        {
            Users_DTO users_DTO = new Users_DTO();
            GeneralDbContext generalDbContext = new GeneralDbContext();
            var result = string.Empty;
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "proc_CheckUserExist";
                command.Parameters.AddWithValue("@email", username);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@org_id", OrgId);
                var dataReader = await command.ExecuteReaderAsync();
                if(dataReader.Read())
                {
                    result = "success";
                    users_DTO.id = dataReader["id"] != System.DBNull.Value? Convert.ToInt32(dataReader["id"]):0;
                    users_DTO.org_id = dataReader["org_id"] != System.DBNull.Value ? Convert.ToInt32(dataReader["org_id"]):0;
                    users_DTO.user_name = dataReader["user_name"] != System.DBNull.Value ? dataReader["user_name"].ToString() : "";
                    users_DTO.f_name = dataReader["f_name"] != System.DBNull.Value ? dataReader["f_name"].ToString() : "";
                    users_DTO.l_name = dataReader["l_name"] != System.DBNull.Value ? dataReader["l_name"].ToString() : "";
                    users_DTO.email = dataReader["email"] != System.DBNull.Value ? dataReader["email"].ToString() : "";
                    users_DTO.password = dataReader["password"] != System.DBNull.Value ? dataReader["password"].ToString() : "";
                    users_DTO.phone_number = dataReader["phone_number"] != System.DBNull.Value ? dataReader["phone_number"].ToString() : "";
                    users_DTO.address_1 = dataReader["address_1"] != System.DBNull.Value ? dataReader["address_1"].ToString() : "";
                    users_DTO.address_2 = dataReader["address_2"] != System.DBNull.Value ? dataReader["address_2"].ToString() : "";
                    users_DTO.area = dataReader["area"] != System.DBNull.Value ? dataReader["area"].ToString() : "";
                    users_DTO.city = dataReader["city"] != System.DBNull.Value ? Convert.ToInt32(dataReader["city"]): 0;
                    users_DTO.state = dataReader["state"] != System.DBNull.Value ? Convert.ToInt32(dataReader["state"]): 0;
                    users_DTO.country = dataReader["country"] != System.DBNull.Value ? Convert.ToInt32(dataReader["country"]): 0;
                    users_DTO.pincode = dataReader["pincode"] != System.DBNull.Value ? Convert.ToInt32(dataReader["pincode"]): 0;
                    users_DTO.is_active = dataReader["is_active"] != System.DBNull.Value ? Convert.ToBoolean(dataReader["is_active"]): true;

                }
                connection.Close();
            }
            if(result != "success")
            {
                return null;
            }

            // 1. Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create Private Key to Encrypted
            var tokenKey = Encoding.ASCII.GetBytes(key);

            //3. Create JETdescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username)
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            //4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            users_DTO.jwt_token = tokenHandler.WriteToken(token);


            // 5. Return Token from method
            
                return users_DTO;
           
        }
        
    }
}
