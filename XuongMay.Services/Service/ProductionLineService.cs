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
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public ProductionLineService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductionLine> CreateProductionLine(ProductionLineModelView request)
        {
            ProductionLine ProductionLine = _mapper.Map<ProductionLine>(request);
            _context.ProductionLines.Add(ProductionLine);
            await _context.SaveChangesAsync();
            return ProductionLine;
        }

        public async Task<BasePaginatedList<ProductionLine>> GetAllProductionLines(int pageNumber, int pageSize)
        {
            var allCategories = await _context.ProductionLines.ToListAsync();
            var totalItems = allCategories.Count();
            var items = allCategories.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var paginatedList = new BasePaginatedList<ProductionLine>(items, totalItems, pageNumber, pageSize);
            return paginatedList;
        }

        public async Task<ProductionLine> GetProductionLineById(string id)
        {
            return await _context.ProductionLines.FirstOrDefaultAsync(pc => pc.Id == id);
        }

        public async Task<ProductionLine> UpdateProductionLine(string id, ProductionLineModelView request)
        {
            ProductionLine ProductionLine = await _context.ProductionLines.FirstOrDefaultAsync(pc => pc.Id == id);
            if (ProductionLine == null)
            {
                return null;
            }
            ProductionLine.LineName = request.LineName;
            ProductionLine.WorkerCount = request.WorkerCount;
            _context.ProductionLines.Update(ProductionLine);
            await _context.SaveChangesAsync();
            return ProductionLine;
        }

        public async Task<bool> DeleteProductionLine(string id)
        {
            ProductionLine ProductionLine = await _context.ProductionLines.FirstOrDefaultAsync(pc => pc.Id == id);
            if (ProductionLine == null)
            {
                return false;
            }
            _context.ProductionLines.Remove(ProductionLine);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
