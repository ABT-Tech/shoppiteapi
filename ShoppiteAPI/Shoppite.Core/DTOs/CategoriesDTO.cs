using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Core.DTOs
{
    public class CategoriesDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int MainCategoryId { get; set; }
        public string CategoryImage { get; set; }

    }
}
