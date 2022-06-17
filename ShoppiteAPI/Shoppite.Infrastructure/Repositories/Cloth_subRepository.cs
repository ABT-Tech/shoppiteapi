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
    public class Cloth_subRepository : ICloth_subRepository
    {
        public async Task<List<Cloth_SubCategory_DTO>> GetclothSubNavList()
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            List<Cloth_SubCategory_DTO> cloth_SubCategory_DTO = new List<Cloth_SubCategory_DTO>();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "proc_getallsub_categories_by_cloth";
                command.Parameters.AddWithValue("@org_id", 1);
                command.Parameters.AddWithValue("@category_id", 6);
                var dataReader = await command.ExecuteReaderAsync();
                ExtensionMethods extensionMethods = new ExtensionMethods();
                cloth_SubCategory_DTO = extensionMethods.DataReaderMapToList<Cloth_SubCategory_DTO>(dataReader);
                connection.Close();
                return cloth_SubCategory_DTO;
            }
        }
    }
}
