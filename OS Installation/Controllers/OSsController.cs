using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OS_Installation.Models;

namespace OS_Installation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OSsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public OSsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/OSs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OS>>> GetOperatingSystems()
        {
          if (_context.OperatingSystems == null)
          {
              return NotFound();
          }
            return await _context.OperatingSystems.ToListAsync();
        }

        // GET: api/OSs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OS>> GetOS(long id)
        {
          if (_context.OperatingSystems == null)
          {
              return NotFound();
          }
            var oS = await _context.OperatingSystems.FindAsync(id);

            if (oS == null)
            {
                return NotFound();
            }

            return oS;
        }

        // PUT: api/OSs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOS(long id, OS oS)
        {
            if (id != oS.Id)
            {
                return BadRequest();
            }

            _context.Entry(oS).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OSExists(id))
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

        // POST: api/OSs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OS>> PostOS(OS oS)
        {
          if (_context.OperatingSystems == null)
          {
              return Problem("Entity set 'ApplicationContext.OperatingSystems'  is null.");
          }
            _context.OperatingSystems.Add(oS);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOS", new { id = oS.Id }, oS);
        }

        // DELETE: api/OSs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOS(long id)
        {
            if (_context.OperatingSystems == null)
            {
                return NotFound();
            }
            var oS = await _context.OperatingSystems.FindAsync(id);
            if (oS == null)
            {
                return NotFound();
            }

            _context.OperatingSystems.Remove(oS);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OSExists(long id)
        {
            return (_context.OperatingSystems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
