using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HerentalsVuilbakken.Data;
using HerentalsVuilbakken.Models;
using Microsoft.AspNetCore.Authorization;

namespace HerentalsVuilbakken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VuilbakLoggingController : ControllerBase
    {
        private readonly VuilbakContext _context;
        public VuilbakLoggingController(VuilbakContext context)
        {
            _context = context;
        }
        // GET: api/VuilbakLogging
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VuilbakLogging>>> GetVuilbakLoggings()
        {
            return await _context.VuilbakLoggings.ToListAsync();
        }
        // GET: api/VuilbakLogging/1
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<VuilbakLogging>> GetVuilbakLogging(int id)
        {

            var vuilbaklogging = await _context.VuilbakLoggings.OrderByDescending(v => v.Datum).SingleOrDefaultAsync(v => v.VuilbakLoggingID == id);

            if (vuilbaklogging == null)
            {
                return NotFound();
            }
            return vuilbaklogging;
        }
        // GET: api/VuilbakLogging/Vuilbak/1
        [Authorize]
        [HttpGet("Vuilbak/{id}")]
        public async Task<ActionResult<IEnumerable<VuilbakLogging>>> GetVuilbakLoggingByVuilbak(int id)
        {

            return await _context.VuilbakLoggings.Where(v => v.VuilbakID == id).OrderBy(v => v.Datum).ToListAsync();
        }
        // PUT: api/VuilbakLogging/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVuilbakLogging(int id, VuilbakLogging vuilbakLogging)
        {

            if (id != vuilbakLogging.VuilbakID)
            {
                return BadRequest();
            }

            _context.Entry(vuilbakLogging).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VuilbakLoggingExists(id))
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
        // POST: api/VuilbakLogging
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<VuilbakLogging>> PostVuilbakLogging(VuilbakLogging vuilbakLogging)
        {

            _context.VuilbakLoggings.Add(vuilbakLogging);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVuilbakLogging", new { id = vuilbakLogging.VuilbakID }, vuilbakLogging);
        }
        // DELETE: api/Vuilbak/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<VuilbakLogging>> DeleteVuilbakLogging(int id)
        {

            var vuilbakLogging = await _context.VuilbakLoggings.FindAsync(id);
            if (vuilbakLogging == null)
            {
                return NotFound();
            }

            _context.VuilbakLoggings.Remove(vuilbakLogging);
            await _context.SaveChangesAsync();

            return vuilbakLogging;
        }
        private bool VuilbakLoggingExists(int id)
        {
            return _context.VuilbakLoggings.Any(e => e.VuilbakLoggingID == id);
        }
    }
}
