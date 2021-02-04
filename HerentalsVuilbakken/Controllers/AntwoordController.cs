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
    public class AntwoordController : ControllerBase
    {
        private readonly VuilbakContext _context;
        public AntwoordController(VuilbakContext context)
        {
            _context = context;
        }
        // GET: api/Antwoord
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Antwoord>>> GetAntwoorden()
        {
            return await _context.Antwoorden.Include(x => x.Enquete).ToListAsync();
        }
        // GET: api/Antwoord/Active/1
        [Authorize]
        [HttpGet("Active/{id}")]
        public async Task<ActionResult<IEnumerable<Antwoord>>> GetAntwoordenActiveEnquete(int id)
        {

            var enquetes = await _context.Antwoorden.Where(x => x.EnqueteID == id).Include(x => x.Enquete).ToListAsync();

            if (enquetes == null)
            {
                return NotFound();
            }
            return enquetes;
        }
        // GET: api/Antwoord/1
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Antwoord>> GetAntwoord(int id)
        {

            var antwoord = await _context.Antwoorden.Include(x => x.Enquete).SingleOrDefaultAsync(v => v.AntwoordID == id);

            if (antwoord == null)
            {
                return NotFound();
            }
            return antwoord;
        }
        // POST: api/Antwoord
        [HttpPost]
        public async Task<ActionResult<Antwoord>> PostAntwoord(Antwoord antwoord)
        {

            _context.Antwoorden.Add(antwoord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAntwoord", new { id = antwoord.AntwoordID }, antwoord);
        }
    }
}
