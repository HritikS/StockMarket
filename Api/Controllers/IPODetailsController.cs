using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Models;
using System;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IPODetailsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IPODetailsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/IPODetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IPODetail>>> GetIPODetails()
        {
            return await _context.IPODetails.ToListAsync();
        }

        [HttpGet("sorted")]
        public async Task<ActionResult<IEnumerable<Object>>> GetIPODetailsSorted()
        {
            var ipos = await (from ipo in _context.IPODetails
                              join c in _context.Companies on ipo.CompanyId equals c.Id
                              join se in _context.StockExchanges on ipo.StockExchangeId equals se.Id
                              orderby ipo.OpenDateTime select new 
                              { 
                                ipo,
                                company = c.Name,
                                stockExchange = se.Name
                              }).ToListAsync();
            if (ipos == null)
                return NotFound();
            return ipos;
        }

        // GET: api/IPODetails/5/5
        [HttpGet("{CompanyId}/{StockExchangeId}")]
        public async Task<ActionResult<IPODetail>> GetIPODetail(int CompanyId, int StockExchangeId)
        {
            var iPODetail = await _context.IPODetails.FindAsync(CompanyId, StockExchangeId);
            if (iPODetail == null)
                return NotFound();
            return iPODetail;
        }

        [HttpGet("company/{name}")]
        public async Task<ActionResult<IEnumerable<IPODetail>>> GetCompanyIPODetails(string name)
        {
            var ipos = await (from ipo in _context.IPODetails
                              join c in _context.Companies on ipo.CompanyId equals c.Id
                              where c.Name == name
                              select ipo).ToListAsync();
            if (ipos == null)
                return NotFound();
            return ipos;
        }

        // PUT: api/IPODetails/5/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{CompanyId}/{StockExchangeId}")]
        public async Task<IActionResult> PutIPODetail(int CompanyId, int StockExchangeId, IPODetail iPODetail)
        {
            if (CompanyId != iPODetail.CompanyId || StockExchangeId != iPODetail.StockExchangeId)
            {
                return BadRequest();
            }

            _context.Entry(iPODetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IPODetailExists(CompanyId, StockExchangeId))
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

        // POST: api/IPODetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<IPODetail>> PostIPODetail(IPODetail iPODetail)
        {
            _context.IPODetails.Add(iPODetail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (IPODetailExists(iPODetail.CompanyId, iPODetail.StockExchangeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetIPODetail", iPODetail);
        }

        // DELETE: api/IPODetails/5/5
        [HttpDelete("{CompanyId}/{StockExchangeId}")]
        public async Task<ActionResult<IPODetail>> DeleteIPODetail(int CompanyId, int StockExchangeId)
        {
            var iPODetail = await _context.IPODetails.FindAsync(CompanyId, StockExchangeId);
            if (iPODetail == null)
            {
                return NotFound();
            }

            _context.IPODetails.Remove(iPODetail);
            await _context.SaveChangesAsync();

            return iPODetail;
        }

        private bool CompanyStockExchangeExists(int CompanyId, int StockExchangeId) => _context.CompanyStockExchanges.Any(e => e.CompanyId == CompanyId && e.StockExchangeId == StockExchangeId);

        private bool IPODetailExists(int CompanyId, int StockExchangeId)
        {
            return _context.IPODetails.Any(e => e.CompanyId == CompanyId && e.StockExchangeId == StockExchangeId) && CompanyStockExchangeExists(CompanyId, StockExchangeId);
        }
    }
}
