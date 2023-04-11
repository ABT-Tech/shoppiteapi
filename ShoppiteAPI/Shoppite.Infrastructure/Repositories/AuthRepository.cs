using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Shoppite.Core.DTOs;
using Shoppite.Core.Entities;
using Shoppite.Core.Repositories;
using Shoppite.Infrastructure.Helpers;
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
        private EncryptionHelper EncryptPass = new EncryptionHelper();
        public async Task<Users_DTO> Authentication(string username, string password, string type, int OrgId)
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
                command.Parameters.AddWithValue("@password", this.EncryptPass.Encrypt(password));
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@org_id", OrgId);
                var dataReader = await command.ExecuteReaderAsync();
                if(dataReader.Read())
                {
                    result = "success";
                    users_DTO.UserId = dataReader["UserId"] != System.DBNull.Value? Convert.ToInt32(dataReader["UserId"]):0;
                    users_DTO.UserName = dataReader["Username"] != System.DBNull.Value ? dataReader["Username"].ToString() : "";
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
