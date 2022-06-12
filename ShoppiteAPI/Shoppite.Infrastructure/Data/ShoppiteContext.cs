using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Infrastructure.Data
{
    public class ShoppiteContext : DbContext
    {
        public ShoppiteContext(DbContextOptions<ShoppiteContext> options) : base (options)
        {

        }

        public DbSet<Shoppite.Core.Entities.Category> Categories { get; set; }
    }
}
