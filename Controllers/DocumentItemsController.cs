using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Plagiarism_C.Models;
using System.Collections;
using System.Text;

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
            try
            {
                var documentItem = await _context.DocumentItems.ToListAsync();
                return Ok(documentItem);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong: {ex}");
                return StatusCode(500, "Internal server error");
            }

        }

        // GET: api/DocumentItems/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentItem>> GetDocumentItem(Guid id)
        {
            try
            {
                var documentItem = await _context.DocumentItems.FindAsync(id);
                return Ok(documentItem);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong: {ex}");
                return NotFound();
            }
        }

        // PUT: api/DocumentItems/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumentItem(Guid id, DocumentItem documentItem)
        {
            
            if (id.CompareTo(documentItem.Id) != 0)
            {
               return BadRequest();
            }

            _context.Entry(documentItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!DocumentItemExists(id))
                {
                    Console.WriteLine($"Something went wrong: {ex}");
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
            try
            {
                _context.DocumentItems.Add(documentItem);
                await _context.SaveChangesAsync();
                return Ok(CreatedAtAction(nameof(GetDocumentItem), new { id = documentItem.Id }, documentItem));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/DocumentItems/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<DocumentItem>> DeleteDocumentItem(Guid id)
        {
            var documentItem = await _context.DocumentItems.FindAsync(id);
            if (documentItem == null)
            {
                Console.WriteLine($"Something went wrong: Obj is null");
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

        public static DocumentItem getScore(DocumentItem source, ArrayList library)
        {
            var textArray = source.Text.Split('.');
            string[] libArray = (string[])library.ToArray(typeof(string));


            // Declarations
            // Setting the scene
            int score = textArray.Count();


            // Getting score
            for (int index = 0; index < textArray.Count(); index++)
            {
                score += rollingHash(textArray[index], libArray);
            }


            source.Score = (score / textArray.Count() * 100) % 100;
            return source;
        }

        //Hashes all the things and returns equality counts
        private static int rollingHash(string txt, string[] lib)
        {
            int score = 0;
            lib = hashArray(lib);
            foreach (string s in lib)
            {
                if (hashCompare(txt, s))
                {
                    score++;
                }
            }

            return score;
        }


        //Compares two hashed strings
        private static bool hashCompare(string txt, string lib)
        {
            var hash1 = hashFunction(txt);
            if (hash1 == lib)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Hashes the string
        private static string hashFunction(string txt)
        {
            string[] array = txt.Split(' ');
            int SizeCounter = array.Count();
            int hashValue = 0;
            foreach (string s in array)
            {
                byte[] uniBytes = Encoding.Unicode.GetBytes(s);
                int valueToHash = 0;

                foreach (byte b in uniBytes)
                {
                    valueToHash += Convert.ToInt32(b.ToString());
                }

                hashValue += valueToHash * SizeCounter;
                SizeCounter--;
            }

            return hashValue.ToString();
        }

        // Hashes whole library
        public static string[] hashArray(string[] lib)
        {
            for (int index = 0; index < lib.Count(); index++)
            {
                lib[index] = hashFunction(lib[index]).ToString();
            }
            return lib;
        }
    }
}
