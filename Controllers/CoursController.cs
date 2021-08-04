using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DTO;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursController : ControllerBase
    {
        private readonly MyContext _context;

        public CoursController(MyContext context)
        {
            _context = context;
        }

        /*
         * Solution Avec le JsonIgnore
        // GET: api/Cours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cour>>> GetCours()
        {
            return await _context.Cours.Include(x => x.Prof).ToListAsync();
        }
        */


         // Avec DTO

        // GET: api/Cours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoursDTO>>> GetCours()
        {
            var cours = _context.Cours.Include(b => b.Prof).Select(b =>
             new CoursDTO()
             {
                 Id = b.Id,
                 Name = b.Name,
                 NomProf = b.Prof.Name,
                 ProfId = b.ProfId,
                 CoursEleves = b.CoursEleves
             });
            return await cours.ToListAsync();
        }

        /*
         * Solution avec JsonIgnore
        // GET: api/Cours/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cour>> GetCour(int id)
        {
            var cour = await _context.Cours.FindAsync(id);

            if (cour == null)
            {
                return NotFound();
            }

            var prof = await _context.Prof.FindAsync(cour.ProfId);

          //  cour.Prof = prof;

            return cour;
        }
        */

        // Avec DTO

        [HttpGet("{id}")]
        public async Task<ActionResult<CoursDTO>> GetCours(int id)
        {
            var cours = _context.Cours.Include(b => b.Prof).Where(i => i.Id == id).Select(b =>
             new CoursDTO()
             {
                 Id = b.Id,
                 Name = b.Name,
                 NomProf = b.Prof.LastName,
                 SurnomProf = b.Prof.Name,
                 ProfId = b.ProfId



             });
            return await cours.FirstOrDefaultAsync();
        }


   
  


        // PUT: api/Cours/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCour(int id, Cour cour)
        {
            if (id != cour.Id)
            {
                return BadRequest();
            }

            _context.Entry(cour).State = EntityState.Modified;
           // var CourseDB = await _context.Cour.FindAsync(id);
            //CourseDB.Apply(Cour);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourExists(id))
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

        
        // POST: api/Cours
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CourDTOPost>> PostCour(CourDTOPost cour)
        {
            Cour lesson = new Cour();

            lesson.Name = cour.Name;
            lesson.ProfId = cour.ProfId;


            _context.Cours.Add(lesson);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCour", new { id = cour.Id }, lesson);
        }
        /*

        // POST: api/Eleves
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Cour>> PostCours(Cour cour)
        {
            _context.Cours.Add(cour);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCour", new { id = cour.Id }, cour);
        }
        */


        // DELETE: api/Cours/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cour>> DeleteCour(int id)
        {
            var cour = await _context.Cours.FindAsync(id);
            if (cour == null)
            {
                return NotFound();
            }

            _context.Cours.Remove(cour);
            await _context.SaveChangesAsync();

            return cour;
        }

        private bool CourExists(int id)
        {
            return _context.Cours.Any(e => e.Id == id);
        }
    }
}
