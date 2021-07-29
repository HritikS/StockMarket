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
    public class CompaniesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CompaniesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            return await _context.Companies.ToListAsync();
        }

        [HttpGet("full")]
        public async Task<ActionResult<IEnumerable<Object>>> GetCompaniesFull()
        {
            var companies = await (from c in _context.Companies 
                                   join s in _context.Sectors on c.SectorId equals s.Id
                                   join cse in _context.CompanyStockExchanges on c.Id equals cse.CompanyId
                                   join se in _context.StockExchanges on cse.StockExchangeId equals se.Id
                                   select new 
                                   { c,
                                     sector = s.Name,
                                     stockExchanges = se.Name 
                                   }).ToListAsync();
            if (companies == null)
                return NotFound();
            return companies;
        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<Company>> GetCompanyByName(string name)
        {
            var company = await (from c in _context.Companies where c.Name == name select c).SingleOrDefaultAsync();
            if (company == null)
                return NotFound();
            return company;
        }

        [HttpGet("pattern/{name}")]
        public async Task<ActionResult<IEnumerable<Object>>> GetCompaniesByPattern(string name)
        {
            var companies = await (from c in _context.Companies
                                   join s in _context.Sectors on c.SectorId equals s.Id
                                   join cse in _context.CompanyStockExchanges on c.Id equals cse.CompanyId
                                   join se in _context.StockExchanges on cse.StockExchangeId equals se.Id
                                   where c.Name.StartsWith(name) select new 
                                   { c,
                                     sector = s.Name,
                                     stockExchanges = se.Name
                                   }).ToListAsync();
            if (companies == null)
                return NotFound();
            return companies;
        }

        [HttpGet("sector/{name}")]
        public async Task<ActionResult<IEnumerable<string>>> GetCompaniesBySector(string name)
        {
            var companies = await (from c in _context.Companies
                                   join s in _context.Sectors on c.SectorId equals s.Id
                                   where s.Name == name
                                   select c.Name).ToListAsync();
            if (companies == null)
                return NotFound();
            return companies;
        }

        [HttpGet("StockExchange/{name}")]
        public async Task<ActionResult<IEnumerable<string>>> GetCompaniesByStockExchange(string name)
        {
            var companies = await (from c in _context.Companies
                                   join cse in _context.CompanyStockExchanges on c.Id equals cse.CompanyId
                                   join se in _context.StockExchanges on cse.StockExchangeId equals se.Id
                                   where se.Name == name
                                   select c.Name).ToListAsync();
            if (companies == null)
                return NotFound();
            return companies;
        }

        // PUT: api/Companies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, Company company)
        {
            if (id != company.Id)
            {
                return BadRequest();
            }

            _context.Entry(company).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
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

        // POST: api/Companies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompany", new { id = company.Id }, company);
        }

        // DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Company>> DeleteCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return company;
        }

        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }
    }
}
