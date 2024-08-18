using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XuongMay.Contract.Repositories.Entity;
using XuongMay.Contract.Services.Interface;
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

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            return await _context.Products.FirstOrDefaultAsync(pc => pc.Id == id);
        }

        public async Task<Product> UpdateProduct(string id, ProductModelView request)
        {
            Product Product = await _context.Products.FirstOrDefaultAsync(pc => pc.Id == id);
            if (Product == null)
            {
                return null;
            }
            Product.ProductName = request.ProductName;
            Product.CategoryID = request.CategoryID;
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
