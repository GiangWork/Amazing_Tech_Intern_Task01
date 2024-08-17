using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XuongMay.Contract.Repositories.Entity;
using XuongMay.Contract.Services.Interface;
using XuongMay.ModelViews.CategoryModelView;
using XuongMay.Repositories.Context;

namespace XuongMay.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public CategoryService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductCategory> CreateProductCategory(CategoryModelView request)
        {
            ProductCategory productCategory = _mapper.Map<ProductCategory>(request);
            _context.Categorys.Add(productCategory);
            await _context.SaveChangesAsync();
            return productCategory;
        }

        public async Task<List<ProductCategory>> GetAllProductCategories()
        {
            return await _context.Categorys.ToListAsync();
        }

        public async Task<ProductCategory> GetProductCategoryById(string id)
        {
            return await _context.Categorys.FirstOrDefaultAsync(pc => pc.Id == id);
        }

        public async Task<ProductCategory> UpdateProductCategory(string id, CategoryModelView request)
        {
            ProductCategory productCategory = await _context.Categorys.FirstOrDefaultAsync(pc => pc.Id == id);
            if (productCategory == null)
            {
                return null;
            }
            productCategory.CategoryName = request.CategoryName;
            await _context.SaveChangesAsync();
            return productCategory;
        }

        public async Task<bool> DeleteProductCategory(string id)
        {
            ProductCategory productCategory = await _context.Categorys.FirstOrDefaultAsync(pc => pc.Id == id);
            if (productCategory == null)
            {
                return false;
            }
            _context.Categorys.Remove(productCategory);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
