using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportingCommentary.Models;

namespace ReportingCommentary.Controllers
{
    [Produces("application/json")]
    [Route("api/Contracts")]
    public class ContractsController : Controller
    {
        private readonly CommentaryContext _context;

        public ContractsController(CommentaryContext context)
        {
            _context = context;
        }

        // GET: api/Contracts
        [HttpGet]
        public IEnumerable<Contract> GetContracts()
        {
            //return _context.Contracts;
            return _context.Contracts
                .Include(cm => cm.ContractManager)
                .Include(c => c.Customer);
        }

        // GET: api/Contracts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContract([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Contract = await _context.Contracts.SingleOrDefaultAsync(m => m.Id == id);

            if (Contract == null)
            {
                return NotFound();
            }

            return Ok(Contract);
        }

        // PUT: api/Contracts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContract([FromRoute] int id, [FromBody] Contract Contract)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Contract.Id)
            {
                return BadRequest();
            }

            _context.Entry(Contract).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(Contract);
        }

        // POST: api/Contracts
        [HttpPost]
        public async Task<IActionResult> PostContract([FromBody] Contract Contract)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Contracts.Add(Contract);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContract", new { id = Contract.Id }, Contract);
        }

        // DELETE: api/Contracts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContract([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Contract = await _context.Contracts.SingleOrDefaultAsync(m => m.Id == id);
            if (Contract == null)
            {
                return NotFound();
            }

            _context.Contracts.Remove(Contract);
            await _context.SaveChangesAsync();

            return Ok(Contract);
        }

        private bool ContractExists(int id)
        {
            return _context.Contracts.Any(e => e.Id == id);
        }
    }
}