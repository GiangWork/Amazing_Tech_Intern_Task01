using Microsoft.AspNetCore.Mvc;
using XuongMay.Contract.Services.Interface;
using XuongMay.ModelViews.CategoryModelView;

namespace XuongMayBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("create_Category")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryModelView request)
        {
            var Category = await _categoryService.CreateCategory(request);
            return Ok(Category);
        }

        [HttpGet("get_AllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var productCategories = await _categoryService.GetAllCategories();
            return Ok(productCategories);
        }

        [HttpGet("get_CategoryById/{id}")]
        public async Task<IActionResult> GetCategoryById(string id)
        {
            var Category = await _categoryService.GetCategoryById(id);
            if (Category == null)
            {
                return NotFound();
            }
            return Ok(Category);
        }

        [HttpPut("update_Category/{id}")]
        public async Task<IActionResult> UpdateCategory(string id, [FromBody] CategoryModelView request)
        {
            var Category = await _categoryService.UpdateCategory(id, request);
            if (Category == null)
            {
                return NotFound();
            }
            return Ok(Category);
        }

        [HttpDelete("delete_Category/{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var result = await _categoryService.DeleteCategory(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
