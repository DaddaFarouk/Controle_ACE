using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Controle_ACE_2.Data;
using Controle_ACE_2.Models;

namespace Controle_ACE_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalariesAPI : ControllerBase
    {
        private readonly DbSalarieContext _context;

        public SalariesAPI(DbSalarieContext context)
        {
            _context = context;
        }

        // GET: api/SalariesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Salarie>>> GetSalarie()
        {
            return await _context.Salarie.ToListAsync();
        }

        // GET: api/SalariesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Salarie>> GetSalarie(int id)
        {
            var salarie = await _context.Salarie.FindAsync(id);

            if (salarie == null)
            {
                return NotFound();
            }

            return salarie;
        }

        // PUT: api/SalariesAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalarie(int id, Salarie salarie)
        {
            if (id != salarie.ID)
            {
                return BadRequest();
            }

            _context.Entry(salarie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalarieExists(id))
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

        // POST: api/SalariesAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Salarie>> PostSalarie(Salarie salarie)
        {
            _context.Salarie.Add(salarie);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SalarieExists(salarie.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSalarie", new { id = salarie.ID }, salarie);
        }

        // DELETE: api/SalariesAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalarie(int id)
        {
            var salarie = await _context.Salarie.FindAsync(id);
            if (salarie == null)
            {
                return NotFound();
            }

            _context.Salarie.Remove(salarie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalarieExists(int id)
        {
            return _context.Salarie.Any(e => e.ID == id);
        }

        // GET: api/SalariesAPI/Departement/2
        [HttpGet("Departement/{id}")]
        public async Task<ActionResult<IEnumerable<Salarie>>> GetSalarieByDepartment(Int32 id = 1)
        {
           return await _context.Salarie.Where(s => s.DepartementId == id).ToListAsync();
        }
    }
}
