using EmsiStudySpace.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmsiStudySpace.Controllers
{
    [Authorize(Roles = "Professeur")]

    public class TypeDocumentsController : Controller
    {
        private readonly AppDbContext _context;
        public TypeDocumentsController( AppDbContext context) 
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeDocuments.ToListAsync());
        }
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom")] TypeDocument typeDocument)
        {
            
                _context.Add(typeDocument);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeDocument = await _context.TypeDocuments.FindAsync(id);
            if (typeDocument == null)
            {
                return NotFound();
            }
            return View(typeDocument);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom")] TypeDocument typeDocument)
        {
            if (id != typeDocument.Id)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(typeDocument);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.TypeDocuments.Any(e => e.Id == typeDocument.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
        }




        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeDocument = await _context.TypeDocuments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeDocument == null)
            {
                return NotFound();
            }

            return View(typeDocument);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeDocument = await _context.TypeDocuments
                .Include(td => td.Documents) 
                .FirstOrDefaultAsync(td => td.Id == id);

            if (typeDocument == null)
            {
                return NotFound();
            }

            // Remove related Documents
            _context.Documents.RemoveRange(typeDocument.Documents);
            _context.TypeDocuments.Remove(typeDocument);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool TypeDocumentExists(int id)
        {
            return _context.TypeDocuments.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Details(int id)
        {
            var typeDocument = await _context.TypeDocuments
                .Include(td => td.Documents) 
                .FirstOrDefaultAsync(td => td.Id == id);

            if (typeDocument == null)
            {
                return NotFound();
            }

            return View(typeDocument);
        }

    }
}
