using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using HerentalsVuilbakken.Data;
using HerentalsVuilbakken.Models;
using HerentalsVuilbakken.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace HerentalsVuilbakken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        private readonly VuilbakContext _context;
        public ZoneController(VuilbakContext context)
        {
            _context = context;
        }
        // GET: api/Zone
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zone>>> GetZones()
        {
            return await _context.Zones.ToListAsync();
        }
        // GET: api/Zone/1
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Zone>> GetZone(int id)
        {

            var zone = await _context.Zones.SingleOrDefaultAsync(v => v.ZoneID == id);

            if (zone == null)
            {
                return NotFound();
            }
            return zone;
        }
        // PUT: api/Zone/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutZone(int id, Zone zone)
        {

            if (id != zone.ZoneID)
            {
                return BadRequest();
            }

            _context.Entry(zone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZoneExists(id))
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
        // POST: api/Zone
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Zone>> PostZone(Zone zone)
        {

            _context.Zones.Add(zone);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetZone", new { id = zone.ZoneID }, zone);
        }
        // DELETE: api/Zone/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Zone>> DeleteZone(int id)
        {

            var zone = await _context.Zones.FindAsync(id);
            if (zone == null)
            {
                return NotFound();
            }

            _context.Zones.Remove(zone);
            await _context.SaveChangesAsync();

            return zone;
        }
        private bool ZoneExists(int id)
        {
            return _context.Zones.Any(e => e.ZoneID == id);
        }
    }
}
