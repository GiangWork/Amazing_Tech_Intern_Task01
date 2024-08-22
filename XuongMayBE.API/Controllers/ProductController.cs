using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XuongMay.Contract.Services.Interface;
using XuongMay.ModelViews.PaginationModelView;
using XuongMay.ModelViews.ProductModelView;

namespace XuongMayBE.API.Controllers
{
    [Authorize(Roles = "Admin")] // Chỉ cho phép người dùng có vai trò "Admin" truy cập
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) => _productService = productService;

        [HttpPost("create_Product")]
        public async Task<IActionResult> CreateProduct([FromQuery] ProductModelView request)
        {
            // Tạo mới sản phẩm và trả về kết quả
            var Product = await _productService.CreateProduct(request);
            return Ok(new { Message = "Create Success", Product });
        }

        [HttpGet("get_AllProducts")]
        public async Task<IActionResult> GetAllProducts([FromQuery] PaginationModelView request)
        {
            var pageNumber = request.pageNumber ?? 1; // Số trang, mặc định là 1 nếu không có
            var pageSize = request.pageSize ?? 5; // Số mục trên mỗi trang, mặc định là 5 nếu không có

            // Lấy tất cả sản phẩm với phân trang
            var Products = await _productService.GetAllProducts(pageNumber, pageSize);

            if (Products == null)
                return NotFound(new { Message = "No Result" }); // Trả về lỗi nếu không có kết quả

            return Ok(Products); // Trả về danh sách sản phẩm
        }

        [HttpGet("get_ProductById/{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            // Lấy sản phẩm theo ID
            var Product = await _productService.GetProductById(id);
            if (Product == null)
            {
                return NotFound(new { Message = "No Result" }); // Trả về lỗi nếu không tìm thấy sản phẩm
            }
            return Ok(Product); // Trả về sản phẩm tìm thấy
        }

        [HttpPut("update_Product/{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromQuery] UpdateProductModelView request)
        {
            // Cập nhật sản phẩm theo ID và trả về kết quả
            var Product = await _productService.UpdateProduct(id, request);
            if (Product == null)
            {
                return BadRequest(new { Message = "Update Fail" }); // Trả về lỗi nếu cập nhật không thành công
            }
            return Ok(new { Message = "Update Success", Product });
        }

        [HttpDelete("delete_Product/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            // Xóa sản phẩm theo ID và trả về kết quả
            var result = await _productService.DeleteProduct(id);
            if (!result)
            {
                return BadRequest(new { Message = "Delete Fail" }); // Trả về lỗi nếu xóa không thành công
            }
            return Ok(new { Message = "Delete Success" }); // Trả về thành công nếu xóa thành công
        }
    }
}
