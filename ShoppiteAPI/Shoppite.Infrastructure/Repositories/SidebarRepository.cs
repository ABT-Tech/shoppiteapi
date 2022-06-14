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
    public class SidebarRepository: ISidebarRepository
    {
        public async Task<List<Sidebar_DTO>> GetsidebarNavList()
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            List<Sidebar_DTO> sidebar_DTO = new List<Sidebar_DTO>();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "proc_getallsidemenu";
                command.Parameters.AddWithValue("@org_id", 1);
                var dataReader = await command.ExecuteReaderAsync();
                ExtensionMethods extensionMethods = new ExtensionMethods();
                sidebar_DTO = extensionMethods.DataReaderMapToList<Sidebar_DTO>(dataReader);
                connection.Close();
                return sidebar_DTO;
            }
        }
    }
}
