using Mango.Services.ProductAPI.Models.DTOs;
using Mango.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/products")]
    public class ProductAPIController : ControllerBase
    {
        protected ResponseDTO _response;
        private IProductRepository _productRepository;

        public ProductAPIController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            this._response = new ResponseDTO();
        }

        [HttpGet]
        [Authorize]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<ProductDTO> productDTOs = await _productRepository.GetProducts();
                _response.Result = productDTOs;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };
            }
            return _response;
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<object> Get(int id)
        {
            try
            {
                ProductDTO productDTO = await _productRepository.GetProductById(id);
                _response.Result = productDTO;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [Authorize]
        public async Task<object> Post([FromBody] ProductDTO model)
        {
            try
            {
                ProductDTO productDTO = await _productRepository.CreateUpdateProduct(model);
                _response.Result = productDTO;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };
            }
            return _response;
        }

        [HttpPut]
        [Authorize]
        public async Task<object> Put([FromBody] ProductDTO model)
        {
            try
            {
                ProductDTO productDTO = await _productRepository.CreateUpdateProduct(model);
                _response.Result = productDTO;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<object> Delete(int id)
        {
            try
            {
                bool is_success = await _productRepository.DeleteProduct(id);
                _response.Result = is_success;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };
            }
            return _response;
        }
    }
}
