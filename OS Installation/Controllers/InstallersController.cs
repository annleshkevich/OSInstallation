using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OS_Installation.Models;

namespace OS_Installation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstallersController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public InstallersController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Installers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Installer>>> GetInstallers()
        {
          if (_context.Installers == null)
          {
              return NotFound();
          }
            return await _context.Installers.ToListAsync();
        }

        // GET: api/Installers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Installer>> GetInstaller(long id)
        {
          if (_context.Installers == null)
          {
              return NotFound();
          }
            var installer = await _context.Installers.FindAsync(id);

            if (installer == null)
            {
                return NotFound();
            }

            return installer;
        }

        // PUT: api/Installers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstaller(long id, Installer installer)
        {
            if (id != installer.Id)
            {
                return BadRequest();
            }

            _context.Entry(installer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstallerExists(id))
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

        // POST: api/Installers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Installer>> PostInstaller(Installer installer)
        {
          if (_context.Installers == null)
          {
              return Problem("Entity set 'ApplicationContext.Installers'  is null.");
          }
            _context.Installers.Add(installer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInstaller", new { id = installer.Id }, installer);
        }

        // DELETE: api/Installers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstaller(long id)
        {
            if (_context.Installers == null)
            {
                return NotFound();
            }
            var installer = await _context.Installers.FindAsync(id);
            if (installer == null)
            {
                return NotFound();
            }

            _context.Installers.Remove(installer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InstallerExists(long id)
        {
            return (_context.Installers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
