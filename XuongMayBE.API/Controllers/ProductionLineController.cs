using Microsoft.AspNetCore.Mvc;
using XuongMay.Contract.Services.Interface;
using XuongMay.ModelViews.PaginationModelView;
using XuongMay.ModelViews.ProductionLineModelViews;

namespace XuongMayBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionLineionLineController : ControllerBase
    {
        private readonly IProductionLineService _productLineService;

        public ProductionLineionLineController(IProductionLineService productLineService)
        {
            _productLineService = productLineService;

        }

        [HttpPost("create_ProductionLine")]
        public async Task<IActionResult> CreateProductionLine([FromQuery] ProductionLineModelView request)
        {
            var ProductionLine = await _productLineService.CreateProductionLine(request);
            return Ok(ProductionLine);
        }

        [HttpGet("get_AllProductionLines")]
        public async Task<IActionResult> GetAllProductionLines([FromQuery] PaginationModelView request)
        {
            var pageNumber = request.pageNumber ?? 1;
            var pageSize = request.pageSize ?? 2;
            var ProductionLines = await _productLineService.GetAllProductionLines(pageNumber, pageSize);
            return Ok(ProductionLines);
        }

        [HttpGet("get_ProductionLineById/{id}")]
        public async Task<IActionResult> GetProductionLineById(string id)
        {
            var ProductionLine = await _productLineService.GetProductionLineById(id);
            if (ProductionLine == null)
            {
                return NotFound();
            }
            return Ok(ProductionLine);
        }

        [HttpPut("update_ProductionLine/{id}")]
        public async Task<IActionResult> UpdateProductionLine(string id, [FromQuery] ProductionLineModelView request)
        {
            var ProductionLine = await _productLineService.UpdateProductionLine(id, request);
            if (ProductionLine == null)
            {
                return NotFound();
            }
            return Ok(ProductionLine);
        }

        [HttpDelete("delete_ProductionLine/{id}")]
        public async Task<IActionResult> DeleteProductionLine(string id)
        {
            var result = await _productLineService.DeleteProductionLine(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
