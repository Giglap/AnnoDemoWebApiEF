using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnnoDemoWebApiEF;
using AnnoDemoWebApiEF.Models;

namespace AnnoDemoWebApiEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnotationsController : ControllerBase
    {
        private readonly AnnotationContext _context;

        public AnnotationsController(AnnotationContext context)
        {
            _context = context;
        }

        // GET: api/Annotations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Annotation>>> GetAnnotations()
        {
          if (_context.Annotations == null)
          {
              return NotFound();
          }
            return await _context.Annotations.OrderBy(a => a.BeginTime).ToListAsync();
        }

        // GET: api/Annotations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Annotation>> GetAnnotation(int id)
        {
          if (_context.Annotations == null)
          {
              return NotFound();
          }
            var annotation = await _context.Annotations.FindAsync(id);

            if (annotation == null)
            {
                return NotFound();
            }

            return annotation;
        }

        // PUT: api/Annotations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnnotation(int id, Annotation annotation)
        {
            if (id != annotation.Id)
            {
                return BadRequest();
            }

            _context.Entry(annotation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnotationExists(id))
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

        // POST: api/Annotations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Annotation>> PostAnnotation(Annotation annotation)
        {
          if (_context.Annotations == null)
          {
              return Problem("Entity set 'AnnotationContext.Annotations'  is null.");
          }
            _context.Annotations.Add(annotation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnnotation", new { id = annotation.Id }, annotation);
        }

        // DELETE: api/Annotations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnotation(int id)
        {
            if (_context.Annotations == null)
            {
                return NotFound();
            }
            var annotation = await _context.Annotations.FindAsync(id);
            if (annotation == null)
            {
                return NotFound();
            }

            _context.Annotations.Remove(annotation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnnotationExists(int id)
        {
            return (_context.Annotations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        //*********************************************************************************************************

        // GET: api/Annotations/user/john
        [HttpGet("user/{user}")]
        public async Task<ActionResult<IEnumerable<Annotation>>> GetAnnotationsByUser(string user)
        {
            if (_context.Annotations == null)
            {
                return NotFound();
            }
            var annotations = await _context.Annotations.Where(a => a.User == user).OrderBy(a => a.BeginTime).ToListAsync();

            if (annotations == null || annotations.Count == 0)
            {
                return NotFound();
            }

            return annotations;
        }

        // GET: api/Annotations/time/100/200
        [HttpGet("time/{start}/{end}")]
        public async Task<ActionResult<IEnumerable<Annotation>>> GetAnnotationsByTime(int start, int end)
        {
            if (_context.Annotations == null)
            {
                return NotFound();
            }
            var annotations = await _context.Annotations.Where(a => a.BeginTime >= start && a.BeginTime <= end).OrderBy(a => a.BeginTime).ToListAsync();

            if (annotations == null || annotations.Count == 0)
            {
                return NotFound();
            }

            return annotations;
        }

        // GET: api/Annotations/time/100/200/user/john/imdbid/tt1234567
        [HttpGet("time/{start}/{end}/user/{user}/imdbid/{imdbid}")]
        public async Task<ActionResult<IEnumerable<Annotation>>> GetAnnotationsByTimeUserImdbId(int start, int end, string user, string imdbid)
        {
            if (_context.Annotations == null)
            {
                return NotFound();
            }
            var annotations = await _context.Annotations.Where(a => a.BeginTime >= start && a.BeginTime <= end && a.User == user && a.ImdbId == imdbid).OrderBy(a => a.BeginTime).ToListAsync();

            if (annotations == null || annotations.Count == 0)
            {
                return NotFound();
            }

            return annotations;
        }

        // GET: api/Annotations/user/john/imdbids
        [HttpGet("user/{user}/imdbids")]
        public async Task<ActionResult<IEnumerable<string>>> GetImdbIdsByUser(string user)
        {
            if (_context.Annotations == null)
            {
                return NotFound();
            }
            var imdbids = await _context.Annotations.Where(a => a.User == user).Select(a => a.ImdbId).Distinct().ToListAsync();

            if (imdbids == null || imdbids.Count == 0)
            {
                return NotFound();
            }

            return imdbids;
        }

        // GET: api/Annotations/user/john/imdbid/tt1234567
        [HttpGet("user/{user}/imdbid/{imdbid}")]
        public async Task<ActionResult<IEnumerable<Annotation>>> GetAnnotationsByUserImdbId(string user, string imdbid)
        {
            if (_context.Annotations == null)
            {
                return NotFound();
            }
            var annotations = await _context.Annotations.Where(a => a.User == user && a.ImdbId == imdbid).OrderBy(a => a.BeginTime).ToListAsync();

            if (annotations == null || annotations.Count == 0)
            {
                return NotFound();
            }

            return annotations;
        }

        // GET: api/Annotations/imdbid/tt1234567
        [HttpGet("imdbid/{imdbid}")]
        public async Task<ActionResult<IEnumerable<Annotation>>> GetAnnotationsByImdbId(string imdbid)
        {
            if (_context.Annotations == null)
            {
                return NotFound();
            }
            var annotations = await _context.Annotations.Where(a => a.ImdbId == imdbid).OrderBy(a => a.BeginTime).ToListAsync();

            if (annotations == null || annotations.Count == 0)
            {
                return NotFound();
            }

            return annotations;
        }


    }
}
