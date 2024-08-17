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

        [HttpPost("create_productCategory")]
        public async Task<IActionResult> CreateProductCategory([FromBody] CategoryModelView request)
        {
            var productCategory = await _categoryService.CreateProductCategory(request);
            return Ok(productCategory);
        }

        [HttpGet("get_AllProductCategories")]
        public async Task<IActionResult> GetAllProductCategories()
        {
            var productCategories = await _categoryService.GetAllProductCategories();
            return Ok(productCategories);
        }

        [HttpGet("get_ProductCategoryById/{id}")]
        public async Task<IActionResult> GetProductCategoryById(string id)
        {
            var productCategory = await _categoryService.GetProductCategoryById(id);
            if (productCategory == null)
            {
                return NotFound();
            }
            return Ok(productCategory);
        }

        [HttpPut("update_ProductCategory/{id}")]
        public async Task<IActionResult> UpdateProductCategory(string id, [FromBody] CategoryModelView request)
        {
            var productCategory = await _categoryService.UpdateProductCategory(id, request);
            if (productCategory == null)
            {
                return NotFound();
            }
            return Ok(productCategory);
        }

        [HttpDelete("delete_ProductCategory/{id}")]
        public async Task<IActionResult> DeleteProductCategory(string id)
        {
            var result = await _categoryService.DeleteProductCategory(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
