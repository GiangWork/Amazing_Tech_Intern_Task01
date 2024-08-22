using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XuongMay.Contract.Services.Interface;
using XuongMay.ModelViews.CategoryModelView;
using XuongMay.ModelViews.PaginationModelView;

namespace XuongMayBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(Roles = "Admin")] // Chỉ cho phép người dùng có vai trò "Admin" truy cập
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) => _categoryService = categoryService;

        [HttpPost("create_Category")]
        public async Task<IActionResult> CreateCategory([FromQuery] CategoryModelView request)
        {
            // Tạo mới danh mục và trả về kết quả
            var Category = await _categoryService.CreateCategory(request);
            return Ok(new { Message = "Create Success", Category });
        }

        [HttpGet("get_AllCategories")]
        public async Task<IActionResult> GetAllCategories([FromQuery] PaginationModelView request)
        {
            var pageNumber = request.pageNumber ?? 1; // Số trang, mặc định là 1 nếu không có
            var pageSize = request.pageSize ?? 5; // Số mục trên mỗi trang, mặc định là 5 nếu không có

            // Lấy tất cả danh mục với phân trang
            var productCategories = await _categoryService.GetAllCategories(pageNumber, pageSize);

            if (productCategories == null)
                return NotFound(new { Message = "No Result" }); // Trả về lỗi nếu không có kết quả

            return Ok(productCategories); // Trả về danh sách danh mục
        }

        [HttpGet("get_CategoryById/{id}")]
        public async Task<IActionResult> GetCategoryById(string id)
        {
            // Lấy danh mục theo ID
            var Category = await _categoryService.GetCategoryById(id);
            if (Category == null)
            {
                return NotFound(new { Message = "No Result" }); // Trả về lỗi nếu không tìm thấy danh mục
            }

            return Ok(Category); // Trả về danh mục tìm thấy
        }

        [HttpPut("update_Category/{id}")]
        public async Task<IActionResult> UpdateCategory(string id, [FromQuery] UpdateCategoryModelView request)
        {
            // Cập nhật danh mục theo ID và trả về kết quả
            var Category = await _categoryService.UpdateCategory(id, request);
            if (Category == null)
            {
                return BadRequest(new { Message = "Update Fail" }); // Trả về lỗi nếu cập nhật không thành công
            }
            return Ok(new { Message = "Update Success", Category });
        }

        [HttpDelete("delete_Category/{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            // Xóa danh mục theo ID và trả về kết quả
            var result = await _categoryService.DeleteCategory(id);
            if (!result)
            {
                return BadRequest(new { Message = "Delete Fail" }); // Trả về lỗi nếu xóa không thành công
            }
            return Ok(new { Message = "Delete Success" }); // Trả về thành công nếu xóa thành công
        }
    }
}
