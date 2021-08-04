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
    public class CourElevesController : ControllerBase
    {
        private readonly MyContext _context;

        public CourElevesController(MyContext context)
        {
            _context = context;
        }

        // GET: api/CourEleves
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourEleve>>> GetCourEleve()
        {
            return await _context.CourEleve.ToListAsync();
        }

        // GET: api/CourEleves/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourEleve>> GetCourEleve(int id)
        {
            var courEleve = await _context.CourEleve.FindAsync(id);

            if (courEleve == null)
            {
                return NotFound();
            }

            return courEleve;
        }

        // PUT: api/CourEleves/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourEleve(int id, CourEleve courEleve)
        {
            if (id != courEleve.CourId)
            {
                return BadRequest();
            }

            _context.Entry(courEleve).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourEleveExists(id))
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
        

      


        // POST: api/CourEleves
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CourEleve>> PostCourEleve(CourEleve courEleve)
        {
            _context.CourEleve.Add(courEleve);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CourEleveExists(courEleve.CourId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCourEleve", new { id = courEleve.CourId }, courEleve);
        }

        // DELETE: api/CourEleves/5
        [HttpDelete]
        public async Task<ActionResult<CourEleve>> DeleteCourEleve(int CoursId, int EleveId)
        {
            var courEleve = await _context.CourEleve.FindAsync(CoursId, EleveId);
            if (courEleve == null)
            {
                return NotFound();
            }

            _context.CourEleve.Remove(courEleve);
            await _context.SaveChangesAsync();

            return courEleve;
        }

        private bool CourEleveExists(int id)
        {
            return _context.CourEleve.Any(e => e.CourId == id);
        }
    }
}
