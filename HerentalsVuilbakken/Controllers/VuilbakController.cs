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
    public class VuilbakController : ControllerBase
    {
        private readonly VuilbakContext _context;
        public VuilbakController(VuilbakContext context)
        {
            _context = context;
        }
        // GET: api/Vuilbak
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vuilbak>>> GetVuilbakken()
        {
            return await _context.Vuilbakken.OrderByDescending(v => v.Volheid/v.WanneerVol).Include(z => z.Zone).ToListAsync();
        }
        // GET: api/Vuilbak/1
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Vuilbak>> GetVuilbak(int id)
        {

            var vuilbak = await _context.Vuilbakken.Include(z => z.Zone).SingleOrDefaultAsync(v => v.VuilbakID == id);

            if (vuilbak == null)
            {
                return NotFound();
            }
            return vuilbak;
        }
        // PUT: api/Vuilbak/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVuilbak(int id, Vuilbak vuilbak)
        {

            if (id != vuilbak.VuilbakID)
            {
                return BadRequest();
            }

            _context.Entry(vuilbak).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VuilbakExists(id))
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
        // PUT: api/Vuilbak/Gewicht/1/10
        [HttpPut("Gewicht/{id}")]
        public async Task<IActionResult> PutVuilbakGewicht(int id, string gewicht)
        {

            var vuilbak = await _context.Vuilbakken.SingleOrDefaultAsync(v => v.VuilbakID == id);

            if (vuilbak == null)
            {
                return BadRequest();
            }
            vuilbak.Gewicht = int.Parse(gewicht);

            _context.Entry(vuilbak).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VuilbakExists(id))
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
        // PUT: api/Vuilbak/Volheid/10/5
        [HttpPut("Volheid/{volheid}/{id}")]
        public async Task<IActionResult> PutVuilbakVolheid(int volheid, int id)
        {

            var vuilbak = await _context.Vuilbakken.SingleOrDefaultAsync(v => v.VuilbakID == id);

            if (vuilbak == null)
            {
                return BadRequest();
            }
            vuilbak.Volheid = volheid;

            _context.Entry(vuilbak).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VuilbakExists(id))
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
        // POST: api/Vuilbak
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Vuilbak>> PostVuilbak(Vuilbak vuilbak)
        {

            _context.Vuilbakken.Add(vuilbak);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVuilbak", new { id = vuilbak.VuilbakID }, vuilbak);
        }
        // DELETE: api/Vuilbak/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vuilbak>> DeleteVuilbak(int id)
        {

            var vuilbak = await _context.Vuilbakken.FindAsync(id);
            if (vuilbak == null)
            {
                return NotFound();
            }

            _context.Vuilbakken.Remove(vuilbak);
            await _context.SaveChangesAsync();

            return vuilbak;
        }
        private bool VuilbakExists(int id)
        {
            return _context.Vuilbakken.Any(e => e.VuilbakID == id);
        }
    }
}
