using EfCoreRelationshipsDemo.Data;
using EfCoreRelationshipsDemo.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EfCoreRelationshipsDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController(SampleDbContext context) : ControllerBase
    {
        // GET: api/Actors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actor>>> GetActors()
        {
            if (context.Actors == null)
            {
                return NotFound();
            }

            return await context.Actors.Include(x => x.Movies).ToListAsync();
        }

        // GET: api/Actors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Actor>> GetActor(Guid id)
        {
            if (context.Actors == null)
            {
                return NotFound();
            }

            var actor = await context.Actors.FindAsync(id);

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

            context.Entry(actor).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
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
            if (context.Actors == null)
            {
                return Problem("Entity set 'SampleDbContext.Actors'  is null.");
            }

            context.Actors.Add(actor);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetActor", new { id = actor.Id }, actor);
        }

        // DELETE: api/Actors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActor(Guid id)
        {
            if (context.Actors == null)
            {
                return NotFound();
            }

            var actor = await context.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }

            context.Actors.Remove(actor);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}/movies/{movieId}")]
        public async Task<IActionResult> AddMovie(Guid id, Guid movieId)
        {
            if (context.Actors == null)
            {
                return NotFound("Actors is null.");
            }

            var actor = await context.Actors.Include(x => x.Movies).SingleOrDefaultAsync(x => x.Id == id);
            if (actor == null)
            {
                return NotFound($"Actor with id {id} not found.");
            }

            var movie = await context.Movies.FindAsync(movieId);
            if (movie == null)
            {
                return NotFound($"Movie with id {movieId} not found.");
            }

            if (actor.Movies.Any(x => x.Id == movie.Id))
            {
                return Problem($"Movie with id {movieId} already exists for Actor {id}.");
            }

            actor.Movies.Add(movie);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetActor", new { id = actor.Id }, actor);
        }

        [HttpGet("{id}/movies")]
        public async Task<IActionResult> GetMovies(Guid id)
        {
            if (context.Actors == null)
            {
                return NotFound("Actors is null.");
            }

            var actor = await context.Actors.Include(x => x.Movies).SingleOrDefaultAsync();
            if (actor == null)
            {
                return NotFound($"Actor with id {id} not found.");
            }

            return Ok(actor.Movies);
        }

        [HttpDelete("{id}/movies/{movieId}")]
        public async Task<IActionResult> DeleteMovie(Guid id, Guid movieId)
        {
            if (context.Actors == null)
            {
                return NotFound("Actors is null.");
            }

            var actor = await context.Actors.Include(x => x.Movies).SingleOrDefaultAsync();
            if (actor == null)
            {
                return NotFound($"Actor with id {id} not found.");
            }

            var movie = await context.Movies.FindAsync(movieId);
            if (movie == null)
            {
                return NotFound($"Movie with id {movieId} not found.");
            }

            actor.Movies.Remove(movie);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActorExists(Guid id)
        {
            return (context.Actors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
