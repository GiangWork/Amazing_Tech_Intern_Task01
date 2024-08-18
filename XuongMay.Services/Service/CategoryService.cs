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

        public async Task<Category> CreateCategory(CategoryModelView request)
        {
            Category Category = _mapper.Map<Category>(request);
            _context.Categorys.Add(Category);
            await _context.SaveChangesAsync();
            return Category;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categorys.ToListAsync();
        }

        public async Task<Category> GetCategoryById(string id)
        {
            return await _context.Categorys.FirstOrDefaultAsync(pc => pc.Id == id);
        }

        public async Task<Category> UpdateCategory(string id, CategoryModelView request)
        {
            Category Category = await _context.Categorys.FirstOrDefaultAsync(pc => pc.Id == id);
            if (Category == null)
            {
                return null;
            }
            Category.CategoryName = request.CategoryName;
            await _context.SaveChangesAsync();
            return Category;
        }

        public async Task<bool> DeleteCategory(string id)
        {
            Category Category = await _context.Categorys.FirstOrDefaultAsync(pc => pc.Id == id);
            if (Category == null)
            {
                return false;
            }
            _context.Categorys.Remove(Category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
