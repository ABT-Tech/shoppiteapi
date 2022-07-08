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
    public class CategoriesRepository :ICategoriesRepository
    {
        public async Task<Category_DTO> PostCategory(Category_DTO category_DTO)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "addcategory";
                command.Parameters.AddWithValue("@id", category_DTO.id);
                command.Parameters.AddWithValue("@org_id", category_DTO.org_id);
                command.Parameters.AddWithValue("@category_code", category_DTO.category_code);
                command.Parameters.AddWithValue("@category_name", category_DTO.category_name);
                command.Parameters.AddWithValue("@category_description", category_DTO.category_description);
                command.Parameters.AddWithValue("@category_image", category_DTO.category_image);
                await command.ExecuteNonQueryAsync();
                return category_DTO;
            }
        }

        public async Task<List<Category_DTO>> GetAllCategory(int org_id)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            List<Category_DTO> category_DTO = new List<Category_DTO>();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "proc_getallcategory";
                command.Parameters.AddWithValue("@org_id", org_id);
                var dataReader = await command.ExecuteReaderAsync();
                ExtensionMethods extensionMethods = new ExtensionMethods();
                category_DTO = extensionMethods.DataReaderMapToList<Category_DTO>(dataReader);
                connection.Close();
                return category_DTO;
            }
        }

        public async Task<List<Category_DTO>> DeleteCategory(int id, int org_id)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            List<Category_DTO> category_DTO = new List<Category_DTO>();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "deletecategory";
                command.Parameters.AddWithValue("@id", id);
                await command.ExecuteNonQueryAsync();
                connection.Close();
                category_DTO = await GetAllCategory(org_id);
                return category_DTO;
            }
        }

        public async Task<List<Category_DTO>> UpdateCategory(int id, int org_id, string category_code, string category_name, string category_description, string category_image)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            List<Category_DTO> category_DTO = new List<Category_DTO>();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "updatecategory";
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@org_id", org_id);
                command.Parameters.AddWithValue("@category_code", category_code);
                command.Parameters.AddWithValue("@category_name", category_name);
                command.Parameters.AddWithValue("@category_description", category_description);
                command.Parameters.AddWithValue("@category_image", category_image);
                await command.ExecuteNonQueryAsync();
                connection.Close();
                category_DTO = await GetAllCategory(org_id);
                return category_DTO;
            }
        }
        
        public async Task<List<Category_DTO>> GetCategorybyid(int id)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            List<Category_DTO> category_DTO = new List<Category_DTO>();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "proc_getcategory_by_id";
                command.Parameters.AddWithValue("@id", id);
                var dataReader = await command.ExecuteReaderAsync();
                ExtensionMethods extensionMethods = new ExtensionMethods();
                category_DTO = extensionMethods.DataReaderMapToList<Category_DTO>(dataReader);
                connection.Close();
                return category_DTO;
            }
        }

    }
}
