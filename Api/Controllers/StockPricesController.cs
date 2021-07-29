using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockPricesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StockPricesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/StockPrices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockPrice>>> GetStockPrices()
        {
            return await _context.StockPrices.ToListAsync();
        }

        [HttpGet("{CompanyId}/{StockExchangeId}")]
        public async Task<ActionResult<IEnumerable<StockPrice>>> GetCompanyStockPrice(int CompanyId, int StockExchangeId)
        {
            var stockPrice = await (from sp in _context.StockPrices
                                    where sp.CompanyId == CompanyId && sp.StockExchangeId == StockExchangeId
                                    select sp).ToListAsync();
            if (stockPrice == null)
                return NotFound();
            return stockPrice;
        }

        // PUT: api/StockPrices/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockPrice(int id, StockPrice stockPrice)
        {
            if (id != stockPrice.CompanyId)
            {
                return BadRequest();
            }

            _context.Entry(stockPrice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockPriceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StockPrices
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<StockPrice>> PostStockPrice(StockPrice stockPrice)
        {
            _context.StockPrices.Add(stockPrice);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StockPriceExists(stockPrice.CompanyId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStockPrice", new { id = stockPrice.CompanyId }, stockPrice);
        }

        // DELETE: api/StockPrices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StockPrice>> DeleteStockPrice(int id)
        {
            var stockPrice = await _context.StockPrices.FindAsync(id);
            if (stockPrice == null)
            {
                return NotFound();
            }

            _context.StockPrices.Remove(stockPrice);
            await _context.SaveChangesAsync();

            return stockPrice;
        }

        private bool StockPriceExists(int id)
        {
            return _context.StockPrices.Any(e => e.CompanyId == id);
        }
    }
}
