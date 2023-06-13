namespace Shoppite.Core.DTOs
{
    public class UpdateProductDetail
    {
        public int? Id { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public int OrgId { get; set; }
    }
}
