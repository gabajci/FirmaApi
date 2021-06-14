using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirmaApi.Models;

namespace FirmaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkAssignmentsController : ControllerBase
    {
        private readonly FirmaDbContext _context;

        public WorkAssignmentsController(FirmaDbContext context)
        {
            _context = context;
        }

        // GET: api/WorkAssignments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkAssignment>>> GetWorkAssignments()
        {
            return await _context.WorkAssignments.ToListAsync();
        }

        // GET: api/WorkAssignments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkAssignment>> GetWorkAssignment(int id)
        {
            var workAssignment = await _context.WorkAssignments.FindAsync(id);

            if (workAssignment == null)
            {
                return NotFound();
            }

            return workAssignment;
        }

        // PUT: api/WorkAssignments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkAssignment(int id, WorkAssignment workAssignment)
        {
            if (id != workAssignment.Id)
            {
                return BadRequest();
            }

            _context.Entry(workAssignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkAssignmentExists(id))
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

        // POST: api/WorkAssignments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkAssignment>> PostWorkAssignment(WorkAssignment workAssignment)
        {
            _context.WorkAssignments.Add(workAssignment);
            try
            {
            await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WorkAssignmentExists(workAssignment.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetWorkAssignment", new { id = workAssignment.Id }, workAssignment);
        }

        // DELETE: api/WorkAssignments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkAssignment(int id)
        {
            var workAssignment = await _context.WorkAssignments.FindAsync(id);
            if (workAssignment == null)
            {
                return NotFound();
            }

            _context.WorkAssignments.Remove(workAssignment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkAssignmentExists(int id)
        {
            return _context.WorkAssignments.Any(e => e.Id == id);
        }
    }
}
