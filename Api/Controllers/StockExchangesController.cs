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
    public class StockExchangesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StockExchangesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/StockExchanges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockExchange>>> GetStockExchanges()
        {
            return await _context.StockExchanges.ToListAsync();
        }

        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<string>>> GetStockExchangesList()
        {
            return await _context.StockExchanges.Select(se => se.Name).ToListAsync();
        }

        // GET: api/StockExchanges/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockExchange>> GetStockExchange(int id)
        {
            var stockExchange = await _context.StockExchanges.FindAsync(id);

            if (stockExchange == null)
            {
                return NotFound();
            }

            return stockExchange;
        }

        // PUT: api/StockExchanges/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockExchange(int id, StockExchange stockExchange)
        {
            if (id != stockExchange.Id)
            {
                return BadRequest();
            }

            _context.Entry(stockExchange).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockExchangeExists(id))
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

        // POST: api/StockExchanges
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<StockExchange>> PostStockExchange(StockExchange stockExchange)
        {
            _context.StockExchanges.Add(stockExchange);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStockExchange", new { id = stockExchange.Id }, stockExchange);
        }

        // DELETE: api/StockExchanges/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StockExchange>> DeleteStockExchange(int id)
        {
            var stockExchange = await _context.StockExchanges.FindAsync(id);
            if (stockExchange == null)
            {
                return NotFound();
            }

            _context.StockExchanges.Remove(stockExchange);
            await _context.SaveChangesAsync();

            return stockExchange;
        }

        private bool StockExchangeExists(int id)
        {
            return _context.StockExchanges.Any(e => e.Id == id);
        }
    }
}
