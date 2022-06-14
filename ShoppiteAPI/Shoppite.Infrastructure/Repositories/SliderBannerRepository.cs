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
    public class SliderBannerRepository: ISliderBannerRepository
    {
        public async Task<List<SliderBanner_DTO>> GetsliderBannerNavList()
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            List<SliderBanner_DTO> sliderBanner_DTO = new List<SliderBanner_DTO>();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "proc_getallbannnerlist_for_slider";
                command.Parameters.AddWithValue("@org_id", 1);
                var dataReader = await command.ExecuteReaderAsync();
                ExtensionMethods extensionMethods = new ExtensionMethods();
                sliderBanner_DTO = extensionMethods.DataReaderMapToList<SliderBanner_DTO>(dataReader);
                connection.Close();
                return sliderBanner_DTO;
            }
        }
    }
}
