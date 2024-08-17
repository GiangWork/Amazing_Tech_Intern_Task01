using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XuongMay.Contract.Repositories.Entity;
using XuongMay.ModelViews.ProductionLineModelViews;
using XuongMay.Repositories.Context;

namespace XuongMayBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionLineController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public ProductionLineController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        [HttpPost("create_productionLine")]
        public async Task<IActionResult> CreateProductionLine([FromBody] CreateProductionLineModelView request)
        {
            // Create new production line
            CreateProductionLineModelView createProductionLineModelView = new()
            {
                LineName = request.LineName,
                WorkerCount = request.WorkerCount
            };

            // Mapping createProductionLineModelView to ProductionLine
            ProductionLine productionLine = _mapper.Map<ProductionLine>(createProductionLineModelView);

            // Add new product line to database
            _context.ProductionLines.Add(productionLine);

            // Save change
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("get_AllProductionLine")]
        public async Task<IActionResult> GetAllProductionLine()
        {
            List<ProductionLine> productionLines = await _context.ProductionLines.ToListAsync();

            return Ok(productionLines);
        }

        [HttpGet("get_ProductionLineById/{id}")]
        public async Task<IActionResult> GetProductionLineById(string id)
        {
            ProductionLine productionLine = await _context.ProductionLines.FirstOrDefaultAsync(pl => pl.Id == id);

            // Return when not found production line
            if (productionLine == null)
            {
                return NotFound();
            }

            return Ok(productionLine);
        }

        [HttpPut("update_ProductionLine/{id}")]
        public async Task<IActionResult> UpdateProductionLine(string id, [FromBody] CreateProductionLineModelView request)
        {
            ProductionLine productionLine = await _context.ProductionLines.FirstOrDefaultAsync(pl => pl.Id == id);

            // Return when not found production line
            if (productionLine == null)
            {
                return NotFound();
            }

            // Update data
            productionLine.LineName = request.LineName;
            productionLine.WorkerCount = request.WorkerCount;

            return Ok(productionLine);
        }

        [HttpDelete("delete_ProductionLine/{id}")]
        public async Task<IActionResult> DeleteProductionLine(string id)
        {
            ProductionLine productionLine = await _context.ProductionLines.FirstOrDefaultAsync(pl => pl.Id == id);

            // Return when not found production line
            if (productionLine == null)
            {
                return NotFound();
            }

            // Delete data
            _context.ProductionLines.Remove(productionLine);

            // Save change
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
