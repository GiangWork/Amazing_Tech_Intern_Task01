using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XuongMay.Contract.Services.Interface;
using XuongMay.ModelViews.PaginationModelView;
using XuongMay.ModelViews.ProductModelView;

namespace XuongMayBE.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("create_Product")]
        public async Task<IActionResult> CreateProduct([FromQuery] ProductModelView request)
        {
            var Product = await _productService.CreateProduct(request);
            return Ok(new { Message = "Create Success", Product });
        }

        [HttpGet("get_AllProducts")]
        public async Task<IActionResult> GetAllProducts([FromQuery] PaginationModelView request)
        {
            var pageNumber = request.pageNumber ?? 1;
            var pageSize = request.pageSize ?? 5;
            var Products = await _productService.GetAllProducts(pageNumber, pageSize);
            if (Products == null)
                return NotFound(new { Message = "No Result" });
            return Ok(Products);
        }

        [HttpGet("get_ProductById/{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var Product = await _productService.GetProductById(id);
            if (Product == null)
            {
                return NotFound(new { Message = "No Result" });
            }
            return Ok(Product);
        }

        [HttpPut("update_Product/{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromQuery] UpdateProductModelView request)
        {
            var Product = await _productService.UpdateProduct(id, request);
            if (Product == null)
            {
                return BadRequest(new { Message = "Update Fail" });
            }
            return Ok(new { Message = "Update Success", Product });
        }

        [HttpDelete("delete_Product/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var result = await _productService.DeleteProduct(id);
            if (!result)
            {
                return BadRequest(new { Message = "Delete Fail" });
            }
            return Ok(new { Message = "Delete Success" });
        }
    }
}
