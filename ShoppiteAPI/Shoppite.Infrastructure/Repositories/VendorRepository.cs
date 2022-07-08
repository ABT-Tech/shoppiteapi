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
    public class VendorRepository : IVendorRepository
    {
        public async Task<Vendor_DTO> PostVendor(Vendor_DTO vendor)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "addorganization";
                command.Parameters.AddWithValue("@org_name", vendor.org_name);
                command.Parameters.AddWithValue("@org_logo", vendor.org_logo);
                command.Parameters.AddWithValue("@vender_name", vendor.vender_name);
                command.Parameters.AddWithValue("@v_email", vendor.v_email);
                command.Parameters.AddWithValue("@v_phone", vendor.v_phone);
                command.Parameters.AddWithValue("@v_mobile", vendor.v_mobile);
                command.Parameters.AddWithValue("@state", vendor.state);
                command.Parameters.AddWithValue("@city", vendor.city);
                command.Parameters.AddWithValue("@pincode", vendor.pincode);
                command.Parameters.AddWithValue("@org_address", vendor.org_address);
                command.Parameters.AddWithValue("@org_description", vendor.org_description);
                command.Parameters.AddWithValue("@v_bank_name", vendor.v_bank_name);
                command.Parameters.AddWithValue("@v_account_number", vendor.v_account_number);
                command.Parameters.AddWithValue("@v_ifsc_code", vendor.v_ifsc_code);
                command.Parameters.AddWithValue("@v_bank_branch_name", vendor.v_bank_branch_name);
                command.Parameters.AddWithValue("@v_upi_id", vendor.v_upi_id);
                command.Parameters.AddWithValue("@v_id_proof", vendor.v_id_proof);
                await command.ExecuteNonQueryAsync();
                return null;
            }
        }
    }
}
