using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XuongMay.Contract.Repositories.Entity;
using XuongMay.Contract.Services.Interface;
using XuongMay.Core;
using XuongMay.ModelViews.ProductionLineModelViews;
using XuongMay.Repositories.Context;

namespace XuongMay.Services.Service
{
    public class ProductionLineService : IProductionLineService
    {
        // Các trường riêng tư để lưu trữ các phụ thuộc được tiêm qua constructor
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        // Constructor để khởi tạo các phụ thuộc
        public ProductionLineService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Phương thức để tạo một dòng sản xuất mới
        public async Task<ProductionLine> CreateProductionLine(ProductionLineModelView request)
        {
            // Chuyển đổi ProductionLineModelView thành ProductionLine
            ProductionLine ProductionLine = _mapper.Map<ProductionLine>(request);

            // Thêm dòng sản xuất vào cơ sở dữ liệu
            _context.ProductionLines.Add(ProductionLine);
            await _context.SaveChangesAsync();
            return ProductionLine;
        }

        // Phương thức để lấy tất cả các dòng sản xuất với phân trang
        public async Task<BasePaginatedList<ProductionLine>> GetAllProductionLines(int pageNumber, int pageSize)
        {
            // Lấy tất cả các dòng sản xuất từ cơ sở dữ liệu
            var allCategories = await _context.ProductionLines.ToListAsync();
            
            // Tính tổng số mục và áp dụng phân trang
            var totalItems = allCategories.Count();
            var items = allCategories.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            // Tạo danh sách phân trang và trả về
            var paginatedList = new BasePaginatedList<ProductionLine>(items, totalItems, pageNumber, pageSize);
            return paginatedList;
        }

        // Phương thức để lấy một dòng sản xuất theo ID
        public async Task<ProductionLine> GetProductionLineById(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Invalid production line id.", nameof(id));

            // Tìm dòng sản xuất theo ID
            var productionLine = await _context.ProductionLines.FirstOrDefaultAsync(pl => pl.Id == id);

            // Trả về giá trị mặc định nếu không tìm thấy hoặc ném một ngoại lệ nếu cần
            if (productionLine == null)
            {
                throw new KeyNotFoundException("Production line not found.");
            }

            return productionLine;
        }

        // Phương thức để cập nhật thông tin của một dòng sản xuất theo ID
        public async Task<ProductionLine> UpdateProductionLine(string id, UpdateProductionLineModelView request)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Invalid production line id.", nameof(id));
            if (request == null) throw new ArgumentNullException(nameof(request));

            // Tìm dòng sản xuất theo ID
            var productionLine = await _context.ProductionLines.FirstOrDefaultAsync(pl => pl.Id == id);
            if (productionLine == null)
            {
                throw new KeyNotFoundException("Production line not found."); // Hoặc trả về giá trị mặc định hoặc null nếu cần
            }

            // Cập nhật thông tin dòng sản xuất nếu có thay đổi từ UpdateProductionLineModelView
            if (!string.IsNullOrWhiteSpace(request.LineName))
            {
                productionLine.LineName = request.LineName;
            }

            if (request.WorkerCount.HasValue)
            {
                productionLine.WorkerCount = request.WorkerCount.Value;
            }

            // Cập nhật dòng sản xuất trong cơ sở dữ liệu
            _context.ProductionLines.Update(productionLine);
            await _context.SaveChangesAsync();
            return productionLine;
        }

        // Phương thức để xóa một dòng sản xuất theo ID
        public async Task<bool> DeleteProductionLine(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Invalid production line id.", nameof(id));

            // Tìm dòng sản xuất theo ID
            var productionLine = await _context.ProductionLines.FirstOrDefaultAsync(pl => pl.Id == id);
            if (productionLine == null)
            {
                return false;
            }

            // Xóa dòng sản xuất và lưu thay đổi
            _context.ProductionLines.Remove(productionLine);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
