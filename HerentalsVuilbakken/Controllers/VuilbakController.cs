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
        // GET: api/Vuilbak/1
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Vuilbak>> GetVuilbak(int id)
        {
            if (AuthorizationCheck()) { return Unauthorized(); }

            var vuilbak = await _context.Vuilbakken.SingleOrDefaultAsync(v => v.VuilbakID == id);

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
            if (AuthorizationCheck()) { return Unauthorized(); }

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
        // POST: api/Vuilbak
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Vuilbak>> PostVuilbak(Vuilbak vuilbak)
        {
            if (AuthorizationCheck()) { return Unauthorized(); }

            _context.Vuilbakken.Add(vuilbak);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVuilbak", new { id = vuilbak.VuilbakID }, vuilbak);
        }
        // DELETE: api/Vuilbak/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vuilbak>> DeleteVuilbak(int id)
        {
            if (AuthorizationCheck()) { return Unauthorized(); }

            var vuilbak = await _context.Vuilbakken.FindAsync(id);
            if (vuilbak == null)
            {
                return NotFound();
            }

            _context.Vuilbakken.Remove(vuilbak);
            await _context.SaveChangesAsync();

            return vuilbak;
        }
        private bool AuthorizationCheck() {
            //TO DO
            return false;
            bool isInwoner = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "Inwoner").Value);
            if (!isInwoner)
            {
                return true;
            }
            return false;
        }
        private bool VuilbakExists(int id)
        {
            return _context.Vuilbakken.Any(e => e.VuilbakID == id);
        }
    }
}
