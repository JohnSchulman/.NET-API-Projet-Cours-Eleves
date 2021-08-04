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
    public class ElevesController : ControllerBase
    {
        private readonly MyContext _context;

        public ElevesController(MyContext context)
        {
            _context = context;
        }

        // GET: api/Eleves
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Eleve>>> GetEleve()
        {
            return await _context.Eleve.ToListAsync();
        }

        // GET: api/Eleves/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Eleve>> GetEleve(int id)
        {
            var eleve = await _context.Eleve.FindAsync(id);

            if (eleve == null)
            {
                return NotFound();
            }

            return eleve;
        }

        // PUT: api/Eleves/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEleve(int id, Eleve eleve)
        {
            if (id != eleve.Id)
            {
                return BadRequest();
            }

            _context.Entry(eleve).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EleveExists(id))
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

        // POST: api/Eleves
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Eleve>> PostEleve(Eleve eleve)
        {
            _context.Eleve.Add(eleve);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEleve", new { id = eleve.Id }, eleve);
        }

        // DELETE: api/Eleves/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Eleve>> DeleteEleve(int id)
        {
            var eleve = await _context.Eleve.FindAsync(id);
            if (eleve == null)
            {
                return NotFound();
            }

            _context.Eleve.Remove(eleve);
            await _context.SaveChangesAsync();

            return eleve;
        }

        private bool EleveExists(int id)
        {
            return _context.Eleve.Any(e => e.Id == id);
        }
    }

}


