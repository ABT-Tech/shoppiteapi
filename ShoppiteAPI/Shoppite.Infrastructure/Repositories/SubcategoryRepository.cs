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
    public class SubcategoryRepository : ISubcategoryRepository
    {
        public async Task<Subcatgory_DTO> PostSubcategory(Subcatgory_DTO subcatgory_DTO)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "addsub_categories";
                command.Parameters.AddWithValue("@id", subcatgory_DTO.id);
                command.Parameters.AddWithValue("@org_id", subcatgory_DTO.org_id);
                command.Parameters.AddWithValue("@category_id", subcatgory_DTO.category_id);
                command.Parameters.AddWithValue("@sub_ctg_name", subcatgory_DTO.sub_ctg_name);
                command.Parameters.AddWithValue("@sub_ctg_description", subcatgory_DTO.sub_ctg_description);
                command.Parameters.AddWithValue("@sub_ctg_code", subcatgory_DTO.sub_ctg_code);
                command.Parameters.AddWithValue("@sub_ctg_image", subcatgory_DTO.sub_ctg_image);
                await command.ExecuteNonQueryAsync();
                return subcatgory_DTO;
            }
        }

        public async Task<List<Subcatgory_DTO>> GetAllSubcategory(int org_id)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            List<Subcatgory_DTO> subcatgory_DTO = new List<Subcatgory_DTO>();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "proc_getallsub_categories";
                command.Parameters.AddWithValue("@org_id", org_id);
                var dataReader = await command.ExecuteReaderAsync();
                ExtensionMethods extensionMethods = new ExtensionMethods();
                subcatgory_DTO = extensionMethods.DataReaderMapToList<Subcatgory_DTO>(dataReader);
                connection.Close();
                return subcatgory_DTO;
            }
        }

        public async Task<List<Subcatgory_DTO>> GetAllSubcategoryByCategory(int org_id, int category_id)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            List<Subcatgory_DTO> subcatgory_DTO = new List<Subcatgory_DTO>();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "proc_getallsub_category_by_category";
                command.Parameters.AddWithValue("@org_id", org_id);
                command.Parameters.AddWithValue("@category_id", category_id);
                var dataReader = await command.ExecuteReaderAsync();
                ExtensionMethods extensionMethods = new ExtensionMethods();
                subcatgory_DTO = extensionMethods.DataReaderMapToList<Subcatgory_DTO>(dataReader);
                connection.Close();
                return subcatgory_DTO;
            }
        }

        public async Task<List<Subcatgory_DTO>> DeleteSubcategory(int id, int org_id)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            List<Subcatgory_DTO> subcatgory_DTO = new List<Subcatgory_DTO>();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "deletesub_categories";
                command.Parameters.AddWithValue("@id", id);
                await command.ExecuteNonQueryAsync();
                connection.Close();
                subcatgory_DTO = await GetAllSubcategory(org_id);
                return subcatgory_DTO;
            }
        }
    }
}
