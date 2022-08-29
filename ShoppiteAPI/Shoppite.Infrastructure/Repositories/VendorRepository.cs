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
        public async Task<Vendor_Users_DTO> PostVendorUsers(Vendor_Users_DTO vendorusers)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "addvendorusers";
                command.Parameters.AddWithValue("@org_id", vendorusers.org_id);
                command.Parameters.AddWithValue("@role_id", vendorusers.role_id);
                command.Parameters.AddWithValue("@username", vendorusers.username);
                command.Parameters.AddWithValue("@password", vendorusers.password);
                command.Parameters.AddWithValue("@first_name", vendorusers.first_name);
                command.Parameters.AddWithValue("@last_name", vendorusers.last_name);
                command.Parameters.AddWithValue("@mobile_number", vendorusers.mobile_number);
                command.Parameters.AddWithValue("@email_id", vendorusers.email_id);
                command.Parameters.AddWithValue("@address_1", vendorusers.address_1);
                command.Parameters.AddWithValue("@address_2", vendorusers.address_2);
                command.Parameters.AddWithValue("@city", vendorusers.city);
                command.Parameters.AddWithValue("@state", vendorusers.state);
                command.Parameters.AddWithValue("@pincode", vendorusers.pincode);
                command.Parameters.AddWithValue("@id_proof", vendorusers.id_proof);
                command.Parameters.AddWithValue("@isactive", vendorusers.isactive);
                command.Parameters.AddWithValue("@created_date", vendorusers.created_date);
                command.Parameters.AddWithValue("@updated_date", vendorusers.updated_date);
                await command.ExecuteNonQueryAsync();
                return null;
            }
        }
        public async Task<List<Vendor_Users_DTO>> GetAllVendorUsers(int org_id)
        {
            GeneralDbContext generalDbContext = new GeneralDbContext();
            List<Vendor_Users_DTO> Vendor_Users_DTO = new List<Vendor_Users_DTO>();
            using (var connection = new SqlConnection(generalDbContext.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "proc_getallvendorusers";
                command.Parameters.AddWithValue("@org_id", org_id);
                var dataReader = await command.ExecuteReaderAsync();
                ExtensionMethods extensionMethods = new ExtensionMethods();
                Vendor_Users_DTO = extensionMethods.DataReaderMapToList<Vendor_Users_DTO>(dataReader);
                connection.Close();
                return Vendor_Users_DTO;
            }
        }
    }
    
}
