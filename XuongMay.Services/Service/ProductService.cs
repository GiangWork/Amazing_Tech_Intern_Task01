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
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public ProductService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Product> CreateProduct(ProductModelView request)
        {
            Product Product = _mapper.Map<Product>(request);
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();
            return Product;
        }

        public async Task<BasePaginatedList<Product>> GetAllProducts(int pageNumber, int pageSize)
        {
            var allProducts = await _context.Products.ToListAsync();
            var totalItems = allProducts.Count();
            var items = allProducts.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var paginatedList = new BasePaginatedList<Product>(items, totalItems, pageNumber, pageSize);

            return paginatedList;
        }

        public async Task<Product> GetProductById(string id)
        {
            return await _context.Products.FirstOrDefaultAsync(pc => pc.Id == id);
        }

        public async Task<Product> UpdateProduct(string id, UpdateProductModelView request)
        {
            Product Product = await _context.Products.FirstOrDefaultAsync(pc => pc.Id == id);
            if (Product == null)
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(request.ProductName))
            {
                Product.ProductName = request.ProductName;
            }

            if (!string.IsNullOrWhiteSpace(request.CategoryID))
            {
                Product.CategoryID = request.CategoryID;
            }
            
            _context.Products.Update(Product);
            await _context.SaveChangesAsync();
            return Product;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            Product Product = await _context.Products.FirstOrDefaultAsync(pc => pc.Id == id);
            if (Product == null)
            {
                return false;
            }
            _context.Products.Remove(Product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
