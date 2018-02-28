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
    [Route("api/CBS")]
    public class CBSController : Controller
    {
        private readonly CommentaryContext _context;

        public CBSController(CommentaryContext context)
        {
            _context = context;
        }

        // GET: api/CBS
        [HttpGet]
        public IEnumerable<CBS> GetCBSs()
        {
            return _context.CBSs;
        }

        // GET: api/CBS/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCBS([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cBS = await _context.CBSs.SingleOrDefaultAsync(m => m.Id == id);

            if (cBS == null)
            {
                return NotFound();
            }

            return Ok(cBS);
        }

        // PUT: api/CBS/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCBS([FromRoute] int id, [FromBody] CBS cBS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cBS.Id)
            {
                return BadRequest();
            }

            _context.Entry(cBS).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CBSExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(cBS);
        }

        // POST: api/CBS
        [HttpPost]
        public async Task<IActionResult> PostCBS([FromBody] CBS cBS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CBSs.Add(cBS);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCBS", new { id = cBS.Id }, cBS);
        }

        // DELETE: api/CBS/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCBS([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cBS = await _context.CBSs.SingleOrDefaultAsync(m => m.Id == id);
            if (cBS == null)
            {
                return NotFound();
            }

            _context.CBSs.Remove(cBS);
            await _context.SaveChangesAsync();

            return Ok(cBS);
        }

        private bool CBSExists(int id)
        {
            return _context.CBSs.Any(e => e.Id == id);
        }
    }

    [Produces("application/json")]
    [Route("api/ContractCBS")]
    public class ContractCBSController : Controller
    {
        private readonly CommentaryContext _context;

        public ContractCBSController(CommentaryContext context)
        {
            _context = context;
        }

        // GET: api/ContractCBS/5
        [HttpGet("{id}")]
        public List<CBS> GetContractCBS([FromRoute] int id)
        {
            List<CBS> cbs = (from o in _context.CBSs where o.ContractId == id select o).ToList();

            return cbs;
        }
    }
}