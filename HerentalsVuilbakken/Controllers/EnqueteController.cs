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
    public class EnqueteController : ControllerBase
    {
        private readonly VuilbakContext _context;
        public EnqueteController(VuilbakContext context)
        {
            _context = context;
        }
        // GET: api/Enquete
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enquete>>> GetEnquetes()
        {
            return await _context.Enquetes.ToListAsync();
        }
        // GET: api/Enquete/1
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Enquete>> GetEnquete(int id)
        {

            var enquete = await _context.Enquetes.SingleOrDefaultAsync(v => v.EnqueteID == id);

            if (enquete == null)
            {
                return NotFound();
            }
            return enquete;
        }
        // GET: api/Enquete/Active
        [HttpGet("Active")]
        public async Task<ActionResult<Enquete>> GetActiveEnquete()
        {

            var enquete = await _context.Enquetes.FirstOrDefaultAsync(v => v.Actief == true);

            if (enquete == null)
            {
                return NotFound();
            }
            return enquete;
        }
        // PUT: api/Enquete/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnquete(int id, Enquete enquete)
        {

            if (id != enquete.EnqueteID)
            {
                return BadRequest();
            }

            _context.Entry(enquete).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnqueteExists(id))
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
        // POST: api/Enquete
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Enquete>> PostEnquete(Enquete enquete)
        {

            _context.Enquetes.Add(enquete);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnquete", new { id = enquete.EnqueteID }, enquete);
        }
        // DELETE: api/Enquete/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Enquete>> DeleteEnquete(int id)
        {

            var enquete = await _context.Enquetes.FindAsync(id);
            if (enquete == null)
            {
                return NotFound();
            }

            _context.Enquetes.Remove(enquete);
            await _context.SaveChangesAsync();

            return enquete;
        }
        private bool EnqueteExists(int id)
        {
            return _context.Enquetes.Any(e => e.EnqueteID == id);
        }
    }
}
