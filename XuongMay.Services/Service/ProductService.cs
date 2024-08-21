using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XuongMay.Contract.Repositories.Entity;
using XuongMay.Contract.Services.Interface;
using XuongMay.Core;
using XuongMay.ModelViews.ProductModelView;
using XuongMay.Repositories.Context;

namespace XuongMay.Services.Service
{
    public class ProductService : IProductService
    {
        private readonly DatabaseContext _context; // Dùng để truy cập cơ sở dữ liệu
        private readonly IMapper _mapper; // Dùng để ánh xạ giữa các mô hình

        public ProductService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Tạo một sản phẩm mới
        public async Task<Product> CreateProduct(ProductModelView request)
        {
            Product Product = _mapper.Map<Product>(request); // Ánh xạ dữ liệu từ ProductModelView
            _context.Products.Add(Product); // Thêm sản phẩm vào cơ sở dữ liệu
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
            return Product; // Trả về sản phẩm đã tạo
        }

        // Lấy danh sách sản phẩm với phân trang
        public async Task<BasePaginatedList<Product>> GetAllProducts(int pageNumber, int pageSize)
        {
            var allProducts = await _context.Products.ToListAsync(); // Lấy tất cả sản phẩm
            var totalItems = allProducts.Count(); // Tổng số sản phẩm
            var items = allProducts.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(); // Lấy sản phẩm theo phân trang

            var paginatedList = new BasePaginatedList<Product>(items, totalItems, pageNumber, pageSize); // Tạo danh sách phân trang

            return paginatedList; // Trả về danh sách phân trang
        }

        // Lấy thông tin sản phẩm theo ID
        public async Task<Product> GetProductById(string id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(pc => pc.Id == id); // Tìm sản phẩm theo ID
            if (product == null)
            {
                throw new ArgumentException($"No product found with ID {id}", nameof(id)); // Ném lỗi nếu không tìm thấy sản phẩm
            }
            return product; // Trả về sản phẩm tìm thấy
        }

        // Cập nhật thông tin sản phẩm theo ID
        public async Task<Product> UpdateProduct(string id, UpdateProductModelView request)
        {
            Product? Product = await _context.Products.FirstOrDefaultAsync(pc => pc.Id == id); // Tìm sản phẩm theo ID
            if (Product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} was not found."); // Ném lỗi nếu không tìm thấy sản phẩm
            }

            if (!string.IsNullOrWhiteSpace(request.ProductName))
            {
                Product.ProductName = request.ProductName; // Cập nhật tên sản phẩm
            }

            if (!string.IsNullOrWhiteSpace(request.CategoryID))
            {
                Product.CategoryID = request.CategoryID; // Cập nhật ID danh mục sản phẩm
            }

            _context.Products.Update(Product); // Cập nhật sản phẩm
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
            return Product; // Trả về sản phẩm đã cập nhật
        }

        // Xóa sản phẩm theo ID
        public async Task<bool> DeleteProduct(string id)
        {
            Product? Product = await _context.Products.FirstOrDefaultAsync(pc => pc.Id == id); // Tìm sản phẩm theo ID
            if (Product == null)
            {
                return false; // Trả về false nếu không tìm thấy sản phẩm
            }
            _context.Products.Remove(Product); // Xóa sản phẩm
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
            return true; // Trả về true nếu xóa thành công
        }
    }
}
