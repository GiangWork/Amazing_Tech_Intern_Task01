using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XuongMay.Contract.Repositories.Entity;
using XuongMay.Contract.Services.Interface;
using XuongMay.Core;
using XuongMay.ModelViews.CategoryModelView;
using XuongMay.Repositories.Context;

namespace XuongMay.Services
{
    public class CategoryService : ICategoryService
    {
        // Các trường riêng tư để lưu trữ các phụ thuộc được tiêm qua constructor
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        // Constructor để khởi tạo các phụ thuộc
        public CategoryService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Phương thức để tạo một danh mục mới
        public async Task<Category> CreateCategory(CategoryModelView request)
        {
            // Chuyển đổi CategoryModelView thành Category
            Category Category = _mapper.Map<Category>(request);

            // Thêm danh mục vào cơ sở dữ liệu
            _context.Categorys.Add(Category);
            await _context.SaveChangesAsync();
            return Category;
        }

        // Phương thức để lấy tất cả danh mục với phân trang
        public async Task<BasePaginatedList<Category>> GetAllCategories(int pageNumber, int pageSize)
        {
            // Lấy tất cả danh mục từ cơ sở dữ liệu
            var allCategories = await _context.Categorys.ToListAsync();

            // Tính tổng số mục và phân trang
            var totalItems = allCategories.Count();
            var items = allCategories.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            // Tạo danh sách phân trang và trả về
            var paginatedList = new BasePaginatedList<Category>(items, totalItems, pageNumber, pageSize);
            return paginatedList;
        }

        // Phương thức để lấy danh mục theo ID
        public async Task<Category> GetCategoryById(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Invalid category id.", nameof(id));

            // Tìm danh mục theo ID
            var category = await _context.Categorys.FirstOrDefaultAsync(pc => pc.Id == id);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found.");
            }

            return category;
        }

        // Phương thức để cập nhật danh mục theo ID
        public async Task<Category> UpdateCategory(string id, UpdateCategoryModelView request)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Invalid category id.", nameof(id));
            if (request == null) throw new ArgumentNullException(nameof(request));

            // Tìm danh mục theo ID
            var category = await _context.Categorys.FirstOrDefaultAsync(pc => pc.Id == id);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found.");
            }

            // Cập nhật thông tin danh mục nếu có thay đổi
            if (!string.IsNullOrWhiteSpace(request.CategoryName))
            {
                category.CategoryName = request.CategoryName;
            }

            // Cập nhật danh mục trong cơ sở dữ liệu
            _context.Categorys.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }

        // Phương thức để xóa danh mục theo ID
        public async Task<bool> DeleteCategory(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Invalid category id.", nameof(id));

            // Tìm danh mục theo ID
            var category = await _context.Categorys.FirstOrDefaultAsync(pc => pc.Id == id);
            if (category == null)
            {
                return false;
            }

            // Xóa danh mục và lưu thay đổi
            _context.Categorys.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
