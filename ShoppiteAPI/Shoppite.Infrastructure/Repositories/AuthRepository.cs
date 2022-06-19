using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
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
        
        public async Task<string> Authentication(string username, string password)
        {
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
                var dataReader = await command.ExecuteReaderAsync();
                if(dataReader.Read())
                {
                    result = "success";
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

            // 5. Return Token from method
            return tokenHandler.WriteToken(token);
        }
    }
}
