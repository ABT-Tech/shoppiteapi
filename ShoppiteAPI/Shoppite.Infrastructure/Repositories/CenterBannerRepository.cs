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
    public class CenterBannerRepository : ICenterBannerRepository
    {
        public async Task<List<Center_Banner_DTO>> GetcenterBannerNavList()
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            List<Center_Banner_DTO> center_Banner_DTO = new List<Center_Banner_DTO>();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "proc_getallbannnerlist_for_slider";
                command.Parameters.AddWithValue("@org_id", 1);
                command.Parameters.AddWithValue("@banner_type", "middle_banner");
                var dataReader = await command.ExecuteReaderAsync();
                ExtensionMethods extensionMethods = new ExtensionMethods();
                center_Banner_DTO = extensionMethods.DataReaderMapToList<Center_Banner_DTO>(dataReader);
                connection.Close();
                return center_Banner_DTO;
            }
        }
    }
}
