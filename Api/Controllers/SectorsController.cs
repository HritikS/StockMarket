﻿using System.Collections.Generic;
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
    public class SectorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SectorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Sectors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sector>>> GetSectors()
        {
            return await _context.Sectors.ToListAsync();
        }

        // GET: api/Sectors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sector>> GetSector(int id)
        {
            var sector = await _context.Sectors.FindAsync(id);

            if (sector == null)
            {
                return NotFound();
            }

            return sector;
        }

        // PUT: api/Sectors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSector(int id, Sector sector)
        {
            if (id != sector.Id)
            {
                return BadRequest();
            }

            _context.Entry(sector).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SectorExists(id))
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

        // POST: api/Sectors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Sector>> PostSector(Sector sector)
        {
            _context.Sectors.Add(sector);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSector", new { id = sector.Id }, sector);
        }

        // DELETE: api/Sectors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Sector>> DeleteSector(int id)
        {
            var sector = await _context.Sectors.FindAsync(id);
            if (sector == null)
            {
                return NotFound();
            }

            _context.Sectors.Remove(sector);
            await _context.SaveChangesAsync();

            return sector;
        }

        private bool SectorExists(int id)
        {
            return _context.Sectors.Any(e => e.Id == id);
        }
    }
}
