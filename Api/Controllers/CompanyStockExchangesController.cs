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
    public class CompanyStockExchangesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CompanyStockExchangesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CompanyStockExchanges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyStockExchange>>> GetCompanyStockExchanges()
        {
            return await _context.CompanyStockExchanges.ToListAsync();
        }


        [HttpGet("{CompanyId}/{StockExchangeId}")]
        public async Task<ActionResult<CompanyStockExchange>> GetCompanyStockExchange(int CompanyId, int StockExchangeId)
        {
            var companyStockExchange = await _context.CompanyStockExchanges.FindAsync(CompanyId, StockExchangeId);
            if (companyStockExchange == null)
                return NotFound();
            return companyStockExchange;
        }

        // PUT: api/CompanyStockExchanges/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{CompanyId}/{StockExchangeId}")]
        public async Task<IActionResult> PutCompanyStockExchange(int CompanyId, int StockExchangeId, CompanyStockExchange companyStockExchange)
        {
            if (CompanyId != companyStockExchange.CompanyId || StockExchangeId != companyStockExchange.StockExchangeId)
            {
                return BadRequest();
            }

            _context.Entry(companyStockExchange).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyStockExchangeExists(CompanyId, StockExchangeId))
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

        // POST: api/CompanyStockExchanges
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CompanyStockExchange>> PostCompanyStockExchange(CompanyStockExchange companyStockExchange)
        {
            _context.CompanyStockExchanges.Add(companyStockExchange);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CompanyStockExchangeExists(companyStockExchange.CompanyId, companyStockExchange.StockExchangeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCompanyStockExchange", companyStockExchange);
        }


        [HttpDelete("{CompanyId}/{StockExchangeId}")]
        public async Task<ActionResult<CompanyStockExchange>> DeleteCompanyStockExchange(int CompanyId, int StockExchangeId)
        {
            var companyStockExchange = await _context.CompanyStockExchanges.FindAsync(CompanyId, StockExchangeId);
            if (companyStockExchange == null)
                return NotFound();
            _context.CompanyStockExchanges.Remove(companyStockExchange);
            await _context.SaveChangesAsync();
            return companyStockExchange;
        }

        private bool CompanyStockExchangeExists(int CompanyId, int StockExchangeId) => _context.CompanyStockExchanges.Any(e => e.CompanyId == CompanyId && e.StockExchangeId == StockExchangeId);
    }
}
