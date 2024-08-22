using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XuongMay.Contract.Services.Interface;
using XuongMay.ModelViews.PaginationModelView;
using XuongMay.ModelViews.ProductionLineModelViews;

namespace XuongMayBE.API.Controllers
{
    [Authorize(Roles = "Admin")] // Chỉ cho phép người dùng có vai trò "Admin" truy cập
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionLineionLineController : ControllerBase
    {
        private readonly IProductionLineService _productLineService;

        public ProductionLineController(IProductionLineService productLineService) => _productLineService = productLineService;

        [HttpPost("create_ProductionLine")]
        public async Task<IActionResult> CreateProductionLine([FromQuery] ProductionLineModelView request)
        {
            // Tạo mới dây chuyền sản xuất và trả về kết quả
            var ProductionLine = await _productLineService.CreateProductionLine(request);
            return Ok(new { Message = "Create Success", ProductionLine });
        }

        [Authorize(Roles = "Admin, Line Manager")] // Cho phép người dùng có vai trò "Admin" và "Line Manager" truy cập
        [HttpGet("get_AllProductionLines")]
        public async Task<IActionResult> GetAllProductionLines([FromQuery] PaginationModelView request)
        {
            var pageNumber = request.pageNumber ?? 1; // Số trang, mặc định là 1 nếu không có
            var pageSize = request.pageSize ?? 5; // Số mục trên mỗi trang, mặc định là 5 nếu không có

            // Lấy tất cả dây chuyền sản xuất với phân trang
            var ProductionLines = await _productLineService.GetAllProductionLines(pageNumber, pageSize);

            if (ProductionLines == null)
                return NotFound(new { Message = "No Result" }); // Trả về lỗi nếu không có kết quả

            return Ok(ProductionLines); // Trả về danh sách dây chuyền sản xuất
        }

        [Authorize(Roles = "Admin, Line Manager")] // Cho phép người dùng có vai trò "Admin" và "Line Manager" truy cập
        [HttpGet("get_ProductionLineById/{id}")]
        public async Task<IActionResult> GetProductionLineById(string id)
        {
            // Lấy dây chuyền sản xuất theo ID
            var ProductionLine = await _productLineService.GetProductionLineById(id);
            if (ProductionLine == null)
            {
                return NotFound(new { Message = "No Result" }); // Trả về lỗi nếu không tìm thấy dây chuyền sản xuất
            }
            return Ok(ProductionLine); // Trả về dây chuyền sản xuất tìm thấy
        }

        [HttpPut("update_ProductionLine/{id}")]
        public async Task<IActionResult> UpdateProductionLine(string id, [FromQuery] UpdateProductionLineModelView request)
        {
            // Cập nhật dây chuyền sản xuất theo ID và trả về kết quả
            var ProductionLine = await _productLineService.UpdateProductionLine(id, request);
            if (ProductionLine == null)
            {
                return BadRequest(new { Message = "Update Fail" }); // Trả về lỗi nếu cập nhật không thành công
            }
            return Ok(new { Message = "Update Success", ProductionLine });
        }

        [HttpDelete("delete_ProductionLine/{id}")]
        public async Task<IActionResult> DeleteProductionLine(string id)
        {
            // Xóa dây chuyền sản xuất theo ID và trả về kết quả
            var result = await _productLineService.DeleteProductionLine(id);
            if (!result)
            {
                return BadRequest(new { Message = "Delete Fail" }); // Trả về lỗi nếu xóa không thành công
            }
            return Ok(new { Message = "Delete Success" }); // Trả về thành công nếu xóa thành công
        }
    }
}
