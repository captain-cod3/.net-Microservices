namespace Mango.Services.ProductAPI.Models.DTOs
{
    public class ProductDTO
    {

        public int ProductId { get; set; }
      
        public string Name { get; set; } = string.Empty;
       
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
    }
}
