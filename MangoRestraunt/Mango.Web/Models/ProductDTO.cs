namespace Mango.Web.Models
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
