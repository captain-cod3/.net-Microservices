using Mango.Services.ProductAPI.Models.DTOs;

namespace Mango.Services.ProductAPI.Repository
{
    public interface IProductRepository 
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO> GetProductById(int id);
        Task<ProductDTO> CreateUpdateProduct(ProductDTO productDTO);
        Task<bool> DeleteProduct(int id);
    }
}
