using Shoppite.Core.Repositories;
using Shoppite.Infrastructure.Data;
using Shoppite.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using Shoppite.Core.DTOs;
using Shoppite.Core.Extensions;
using Shoppite.Core.Entities;
using Shoppite.Core.Model;

namespace Shoppite.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        protected readonly Shoppite_masterContext _MasterContext;
        public CartRepository(Shoppite_masterContext dbContext)
        {
            _MasterContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
     
        public async Task<string> AddToCart(CartRequest Cart)
        {
            using (var command = this._MasterContext.Database.GetDbConnection().CreateCommand())
            {
                string strSQL = "SP_AddToCart";

                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                var parameter = command.CreateParameter();
                parameter.ParameterName = "@OrgId";
                parameter.Value = Cart.orgId;
                command.Parameters.Add(parameter);
                parameter = command.CreateParameter();
                parameter.ParameterName = "@UserId";
                parameter.Value = Cart.UserId;
                command.Parameters.Add(parameter);
                parameter = command.CreateParameter();
                parameter.ParameterName = "@ProId";
                parameter.Value = Cart.proId;
                command.Parameters.Add(parameter);
                parameter = command.CreateParameter();
                parameter.ParameterName = "@Qty";
                parameter.Value = Cart.Qty;
                command.Parameters.Add(parameter);
                await this._MasterContext.Database.OpenConnectionAsync();

                await command.ExecuteNonQueryAsync();
                
            }
            return "success";
        }
    }
}
