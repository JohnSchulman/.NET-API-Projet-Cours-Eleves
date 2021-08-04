using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfsController : ControllerBase
    {
        private readonly MyContext _context;

        public ProfsController(MyContext context)
        {
            _context = context;
        }

        // GET: api/Profs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prof>>> GetProf()
        {
            return await _context.Prof.ToListAsync();
        }

        // GET: api/Profs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prof>> GetProf(int id)
        {
            var prof = await _context.Prof.FindAsync(id);

            if (prof == null)
            {
                return NotFound();
            }

            return prof;
        }

        // PUT: api/Profs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProf(int id, Prof prof)
        {
            if (id != prof.Id)
            {
                return BadRequest();
            }

            _context.Entry(prof).State = EntityState.Modified;
          
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfExists(id))
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

        // POST: api/Profs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Prof>> PostProf(Prof prof)
        {
            _context.Prof.Add(prof);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProf", new { id = prof.Id }, prof);
        }

        // DELETE: api/Profs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Prof>> DeleteProf(int id)
        {
            var prof = await _context.Prof.FindAsync(id);
            if (prof == null)
            {
                return NotFound();
            }

            _context.Prof.Remove(prof);
            await _context.SaveChangesAsync();

            return prof;
        }

        private bool ProfExists(int id)
        {
            return _context.Prof.Any(e => e.Id == id);
        }


     

    }
}
