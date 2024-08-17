using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XuongMay.Contract.Repositories.Entity;
using XuongMay.ModelViews.CategoryModelView;
using XuongMay.Repositories.Context;

namespace XuongMayBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public ProductCategoryController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("create_productCategory")]
        public async Task<IActionResult> CreateProductCategory([FromBody] CategoryModelView request)
        {
            // Create new product category
            CategoryModelView categoryModelView = new()
            {
                CategoryName = request.CategoryName,
            };

            // Mapping createProductCategoryModelView to ProductCategory
            ProductCategory productCategory = _mapper.Map<ProductCategory>(categoryModelView);

            // Add new product category to database
            _context.Categorys.Add(productCategory);

            // Save changes
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("get_AllProductCategories")]
        public async Task<IActionResult> GetAllProductCategories()
        {
            List<ProductCategory> productCategories = await _context.Categorys.ToListAsync();

            return Ok(productCategories);
        }

        [HttpGet("get_ProductCategoryById/{id}")]
        public async Task<IActionResult> GetProductCategoryById(string id)
        {
            ProductCategory productCategory = await _context.Categorys.FirstOrDefaultAsync(pc => pc.Id == id);

            // Return when not found product category
            if (productCategory == null)
            {
                return NotFound();
            }

            return Ok(productCategory);
        }

        [HttpPut("update_ProductCategory/{id}")]
        public async Task<IActionResult> UpdateProductCategory(string id, [FromBody] CategoryModelView request)
        {
            ProductCategory productCategory = await _context.Categorys.FirstOrDefaultAsync(pc => pc.Id == id);

            // Return when not found product category
            if (productCategory == null)
            {
                return NotFound();
            }

            // Update data
            productCategory.CategoryName = request.CategoryName;

            // Save changes
            await _context.SaveChangesAsync();

            return Ok(productCategory);
        }

        [HttpDelete("delete_ProductCategory/{id}")]
        public async Task<IActionResult> DeleteProductCategory(string id)
        {
            ProductCategory productCategory = await _context.Categorys.FirstOrDefaultAsync(pc => pc.Id == id);

            // Return when not found product category
            if (productCategory == null)
            {
                return NotFound();
            }

            // Delete data
            _context.Categorys.Remove(productCategory);

            // Save changes
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
