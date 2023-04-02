using EfCoreRelationshipsDemo.Data;
using EfCoreRelationshipsDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EfCoreRelationshipsDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly SampleDbContext _context;

        public ActorsController(SampleDbContext context)
        {
            _context = context;
        }

        // GET: api/Actors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actor>>> GetActors()
        {
            if (_context.Actors == null)
            {
                return NotFound();
            }

            return await _context.Actors.Include(x => x.Movies).ToListAsync();
        }

        // GET: api/Actors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Actor>> GetActor(Guid id)
        {
            if (_context.Actors == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors.FindAsync(id);

            if (actor == null)
            {
                return NotFound();
            }

            return actor;
        }

        // PUT: api/Actors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActor(Guid id, Actor actor)
        {
            if (id != actor.Id)
            {
                return BadRequest();
            }

            _context.Entry(actor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(id))
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

        // POST: api/Actors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Actor>> PostActor(Actor actor)
        {
            if (_context.Actors == null)
            {
                return Problem("Entity set 'SampleDbContext.Actors'  is null.");
            }

            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActor", new { id = actor.Id }, actor);
        }

        // DELETE: api/Actors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActor(Guid id)
        {
            if (_context.Actors == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }

            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}/movies/")]
        public async Task<IActionResult> AddMovie(Guid id, Guid moveId)
        {
            if (_context.Actors == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors.Include(x => x.Movies).SingleOrDefaultAsync();
            if (actor == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(moveId);
            if (movie == null)
            {
                return NotFound();
            }

            if (actor.Movies.Any(x => x.Id == movie.Id))
            {
                return Problem("Movie already exists.");
            }

            actor.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActor", new { id = actor.Id }, actor);
        }

        [HttpGet("{id}/movies")]
        public async Task<IActionResult> GetMovies(Guid id)
        {
            if (_context.Actors == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors.Include(x => x.Movies).SingleOrDefaultAsync();
            if (actor == null)
            {
                return NotFound();
            }

            return Ok(actor.Movies);
        }

        [HttpDelete("{id}/movies/{movieId}")]
        public async Task<IActionResult> DeleteMovie(Guid id, Guid movieId)
        {
            if (_context.Actors == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors.Include(x => x.Movies).SingleOrDefaultAsync();
            if (actor == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(movieId);
            if (movie == null)
            {
                return NotFound();
            }

            actor.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActorExists(Guid id)
        {
            return (_context.Actors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
