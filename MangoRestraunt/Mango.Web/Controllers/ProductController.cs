using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDTO> products = new();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var res = await _productService.GetAllProductsAsync<ResponseDTO>(accessToken);
            if (res.Result != null) { 
                products = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(res.Result));
            }
            return View(products);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> Create(ProductDTO model)
        {
            if (ModelState.IsValid) {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var res = await _productService.CreateProductAsync<ResponseDTO>(model, accessToken);
                if (res.Result != null)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int productId)
        {
            
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var res = await _productService.GetProductByIdAsync<ResponseDTO>(productId,accessToken);
                if (res.Result != null)
                {
                    var product = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(res.Result));
                    return View(product);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDTO model)
        {
            
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var res = await _productService.UpdateProductAsync<ResponseDTO>(model,accessToken);
                if (res.Result != null)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Delete(int productId)
        {
            
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var res = await _productService.DeleteProductAsync<ResponseDTO>(productId,accessToken);
                if (res.Result != null)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return NotFound();
        }
    }
}
