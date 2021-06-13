using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Plagiarism_C.Models;

namespace Plagiarism_C.Controllers
{
    [Route("api/[controller]")] //can change the the url route
    [ApiController]
    public class PlagiarismItemsController : ControllerBase
    {
        private readonly PlagiarismContext _context;

        public PlagiarismItemsController(PlagiarismContext context)
        {
            _context = context;
        }

        // GET: api/PlagiarismItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlagiarismItemDTO>>> GetPlagiarismItems()
        {
            return await _context.PlagiarismItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/PlagiarismItems/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PlagiarismItemDTO>> GetPlagiarismItem(long id)
        {
            var plagiarismItem = await _context.PlagiarismItems.FindAsync(id);

            if (plagiarismItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(plagiarismItem);
        }

        // PUT: api/PlagiarismItems/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlagiarismItem(long id, PlagiarismItemDTO plagiarismItemDTO)
        {
            if (id != plagiarismItemDTO.Id)
            {
                return BadRequest();
            }

            var plagiarismItem = await _context.PlagiarismItems.FindAsync(id);
            if (plagiarismItem == null)
            {
                return NotFound();
            }

            plagiarismItem.Text = plagiarismItemDTO.Text;
            plagiarismItem.Score = plagiarismItemDTO.Score;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!PlagiarismItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/PlagiarismItems
        [HttpPost]
        public async Task<ActionResult<PlagiarismItemDTO>> PostPlagiarismItem(PlagiarismItemDTO plagiarismItemDTO)
        {
            var plagiarismItem = new PlagiarismItem
            {
                Text = plagiarismItemDTO.Text,
                Score = plagiarismItemDTO.Score
            };

            _context.PlagiarismItems.Add(plagiarismItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPlagiarismItem),
                new { id = plagiarismItem.Id },
                ItemToDTO(plagiarismItem));
        }

        // DELETE: api/PlagiarismItems/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlagiarismItem>> DeletePlagiarismItem(long id)
        {
            var plagiarismItem = await _context.PlagiarismItems.FindAsync(id);
            if (plagiarismItem == null)
            {
                return NotFound();
            }

            _context.PlagiarismItems.Remove(plagiarismItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlagiarismItemExists(long id)
        {
            return _context.PlagiarismItems.Any(e => e.Id == id);
        }

        private static PlagiarismItemDTO ItemToDTO(PlagiarismItem plagiarismItem) =>
        new PlagiarismItemDTO
        {
            Id = plagiarismItem.Id,
            Text = plagiarismItem.Text,
            Score = plagiarismItem.Score
        };
    }
}
