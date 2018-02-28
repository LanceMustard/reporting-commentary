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
    [Route("api/ReportingItems")]
    public class ReportingItemsController : Controller
    {
        private readonly CommentaryContext _context;

        public ReportingItemsController(CommentaryContext context)
        {
            _context = context;
        }

        // GET: api/ReportingItems
        [HttpGet]
        public IEnumerable<ReportingItem> GetReportingItems()
        {
            return _context.ReportingItems;
        }

        // GET: api/ReportingItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReportingItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reportingItem = await _context.ReportingItems.SingleOrDefaultAsync(m => m.Id == id);

            if (reportingItem == null)
            {
                return NotFound();
            }

            return Ok(reportingItem);
        }

        // PUT: api/ReportingItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReportingItem([FromRoute] int id, [FromBody] ReportingItem reportingItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reportingItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(reportingItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportingItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(reportingItem);
        }

        // POST: api/ReportingItems
        [HttpPost]
        public async Task<IActionResult> PostReportingItem([FromBody] ReportingItem reportingItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ReportingItems.Add(reportingItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReportingItem", new { id = reportingItem.Id }, reportingItem);
        }

        // DELETE: api/ReportingItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportingItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reportingItem = await _context.ReportingItems.SingleOrDefaultAsync(m => m.Id == id);
            if (reportingItem == null)
            {
                return NotFound();
            }

            _context.ReportingItems.Remove(reportingItem);
            await _context.SaveChangesAsync();

            return Ok(reportingItem);
        }

        private bool ReportingItemExists(int id)
        {
            return _context.ReportingItems.Any(e => e.Id == id);
        }
    }

    [Produces("application/json")]
    [Route("api/ContractReportingItem")]
    public class ContractReportingItemController : Controller
    {
        private readonly CommentaryContext _context;

        public ContractReportingItemController(CommentaryContext context)
        {
            _context = context;
        }

        // GET: api/ContractCBS/5
        [HttpGet("{id}")]
        public List<ReportingItem> GetContractReportingItem([FromRoute] int id)
        {
            List<ReportingItem> reportingItems = (from o in _context.ReportingItems where o.ContractId == id select o).ToList();
            return reportingItems;
        }
    }
}