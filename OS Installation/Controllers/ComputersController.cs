using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OS_Installation.Models;

namespace OS_Installation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputersController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ComputersController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Computers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Computer>>> GetComputers()
        {
          if (_context.Computers == null)
          {
              return NotFound();
          }
            return await _context.Computers.ToListAsync();
        }

        // GET: api/Computers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Computer>> GetComputer(long id)
        {
          if (_context.Computers == null)
          {
              return NotFound();
          }
            var computer = await _context.Computers.FindAsync(id);

            if (computer == null)
            {
                return NotFound();
            }

            return computer;
        }

        // PUT: api/Computers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComputer(long id, Computer computer)
        {
            if (id != computer.Id)
            {
                return BadRequest();
            }

            _context.Entry(computer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComputerExists(id))
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

        // POST: api/Computers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Computer>> PostComputer(Computer computer)
        {
          if (_context.Computers == null)
          {
              return Problem("Entity set 'ApplicationContext.Computers'  is null.");
          }
            _context.Computers.Add(computer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComputer", new { id = computer.Id }, computer);
        }

        // DELETE: api/Computers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComputer(long id)
        {
            if (_context.Computers == null)
            {
                return NotFound();
            }
            var computer = await _context.Computers.FindAsync(id);
            if (computer == null)
            {
                return NotFound();
            }

            _context.Computers.Remove(computer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComputerExists(long id)
        {
            return (_context.Computers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
