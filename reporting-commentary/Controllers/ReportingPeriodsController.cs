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
    [Route("api/ReportingPeriods")]
    public class ReportingPeriodsController : Controller
    {
        private readonly CommentaryContext _context;

        public ReportingPeriodsController(CommentaryContext context)
        {
            _context = context;
        }

        // GET: api/ReportingPeriods
        [HttpGet]
        public IEnumerable<ReportingPeriod> GetReportingPeriods()
        {
            return _context.ReportingPeriods;
        }

        // GET: api/ReportingPeriods/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReportingPeriod([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reportingPeriod = await _context.ReportingPeriods.SingleOrDefaultAsync(m => m.Id == id);

            if (reportingPeriod == null)
            {
                return NotFound();
            }

            return Ok(reportingPeriod);
        }

        // PUT: api/ReportingPeriods/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReportingPeriod([FromRoute] int id, [FromBody] ReportingPeriod reportingPeriod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reportingPeriod.Id)
            {
                return BadRequest();
            }

            _context.Entry(reportingPeriod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportingPeriodExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(reportingPeriod);
        }

        // POST: api/ReportingPeriods
        [HttpPost]
        public async Task<IActionResult> PostReportingPeriod([FromBody] ReportingPeriod reportingPeriod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ReportingPeriods.Add(reportingPeriod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReportingPeriod", new { id = reportingPeriod.Id }, reportingPeriod);
        }

        // DELETE: api/ReportingPeriods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportingPeriod([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reportingPeriod = await _context.ReportingPeriods.SingleOrDefaultAsync(m => m.Id == id);
            if (reportingPeriod == null)
            {
                return NotFound();
            }

            _context.ReportingPeriods.Remove(reportingPeriod);
            await _context.SaveChangesAsync();

            return Ok(reportingPeriod);
        }

        private bool ReportingPeriodExists(int id)
        {
            return _context.ReportingPeriods.Any(e => e.Id == id);
        }
    }
}