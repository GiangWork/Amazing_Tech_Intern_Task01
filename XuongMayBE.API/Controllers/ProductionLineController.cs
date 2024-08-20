using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XuongMay.Contract.Services.Interface;
using XuongMay.ModelViews.PaginationModelView;
using XuongMay.ModelViews.ProductionLineModelViews;

namespace XuongMayBE.API.Controllers
{
    [Authorize(Roles = "Admin")]
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
            return Ok(new { Message = "Create Success", ProductionLine });
        }

        [Authorize(Roles = "Admin, Line Manager")]
        [HttpGet("get_AllProductionLines")]
        public async Task<IActionResult> GetAllProductionLines([FromQuery] PaginationModelView request)
        {
            var pageNumber = request.pageNumber ?? 1;
            var pageSize = request.pageSize ?? 2;
            var ProductionLines = await _productLineService.GetAllProductionLines(pageNumber, pageSize);
            if (ProductionLines == null)
                return NotFound(new { Message = "No Result" });
            return Ok(ProductionLines);
        }

        [Authorize(Roles = "Admin, Line Manager")]
        [HttpGet("get_ProductionLineById/{id}")]
        public async Task<IActionResult> GetProductionLineById(string id)
        {
            var ProductionLine = await _productLineService.GetProductionLineById(id);
            if (ProductionLine == null)
            {
                return NotFound(new { Message = "No Result" });
            }
            return Ok(ProductionLine);
        }

        [HttpPut("update_ProductionLine/{id}")]
        public async Task<IActionResult> UpdateProductionLine(string id, [FromQuery] ProductionLineModelView request)
        {
            var ProductionLine = await _productLineService.UpdateProductionLine(id, request);
            if (ProductionLine == null)
            {
                return BadRequest(new { Message = "Update Fail" });
            }
            return Ok(new { Message = "Update Success", ProductionLine });
        }

        [HttpDelete("delete_ProductionLine/{id}")]
        public async Task<IActionResult> DeleteProductionLine(string id)
        {
            var result = await _productLineService.DeleteProductionLine(id);
            if (!result)
            {
                return BadRequest(new { Message = "Delete Fail" });
            }
            return Ok(new { Message = "Delete Success" });
        }
    }
}
