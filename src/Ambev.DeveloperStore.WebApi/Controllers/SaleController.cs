using Ambev.DeveloperStore.Domain.Entities;
using Ambev.DeveloperStore.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperStore.WebApi.Controllers
{
    namespace DeveloperStore.API.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class SaleController : BaseController
        {
            private readonly SaleService _saleService;

            public SaleController(SaleService saleService)
            {
                _saleService = saleService;
            }

            [HttpPost]
            public IActionResult CreateSale([FromBody] Sale sale)
            {
                _saleService.CreateSale(sale);
                return CreatedAtAction(nameof(GetSale), new { id = sale.SaleId }, sale);
            }

            [HttpPut("{id}")]
            public IActionResult ModifySale(int id, [FromBody] Sale sale)
            {
                _saleService.ModifySale(sale);
                return NoContent();
            }

            [HttpDelete("{id}")]
            public IActionResult CancelSale(int id)
            {
                _saleService.CancelSale(id);
                return NoContent();
            }

            [HttpGet("{id}")]
            public IActionResult GetSale(int id)
            {
                var sale = _saleService.GetSaleById(id);
                if (sale == null)
                    return NotFound();

                return Ok(sale);
            }
        }
    }

}
