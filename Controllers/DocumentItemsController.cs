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
    public class DocumentItemsController : ControllerBase
    {
        private readonly DocumentContext _context;

        public DocumentItemsController(DocumentContext context)
        {
            _context = context;
        }

        // GET: api/DocumentItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentItem>>> GetDocumentItems()
        {
            return await _context.DocumentItems.ToListAsync();
        }

        // GET: api/DocumentItems/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentItem>> GetDocumentItem(Guid id)
        {
            var documentItem = await _context.DocumentItems.FindAsync(id);

            if (documentItem == null)
            {
                return NotFound();
            }

            return documentItem;
        }

        // PUT: api/DocumentItems/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumentItem(Guid id, DocumentItem documentItem)
        {
            if (id != documentItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(documentItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentItemExists(id))
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

        // POST: api/DocumentItems
        [HttpPost]
        public async Task<ActionResult<DocumentItem>> PostDocumentItem(DocumentItem documentItem)
        {
            _context.DocumentItems.Add(documentItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDocumentItem), new { id = documentItem.Id }, documentItem);
        }

        // DELETE: api/DocumentItems/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<DocumentItem>> DeleteDocumentItem(Guid id)
        {
            var documentItem = await _context.DocumentItems.FindAsync(id);
            if (documentItem == null)
            {
                return NotFound();
            }

            _context.DocumentItems.Remove(documentItem);
            await _context.SaveChangesAsync();

            return documentItem;
        }

        private bool DocumentItemExists(Guid id)
        {
            return _context.DocumentItems.Any(e => e.Id == id);
        }
    }
}
