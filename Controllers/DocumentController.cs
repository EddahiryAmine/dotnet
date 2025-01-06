using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmsiStudySpace.Models;
using EmsiStudySpace.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace EmsiStudySpace.Controllers
{
    [Authorize(Roles = "Professeur")]
    public class DocumentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DocumentController(AppDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

       

            [HttpGet]
                public async Task<IActionResult> Index()
                {
                    var prof = await _userManager.GetUserAsync(User);

                    var documents = await _context.Documents
                        .Where(d => d.ProfId == prof.Id)
                        .Include(d => d.TypeDocument)
                        .Include(d => d.Module)
                        .Include(d => d.DocumentGroupes)
                            .ThenInclude(dg => dg.Groupe)
                        .ToListAsync();

                    return View(documents);
                }







        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var prof = await _userManager.GetUserAsync(User);

            var moduleId = await _context.UserModules
                .Where(um => um.UserId == prof.Id)
                .Select(um => um.ModuleId)
                .FirstOrDefaultAsync();

            var viewModel = new DocumentUploadViewModel
            {
                TypeDocuments = await _context.TypeDocuments.ToListAsync(),
                AvailableGroups = await _context.Groupes
                    .Where(g => g.Professeurs.Any(p => p.Id == prof.Id))
                    .ToListAsync(),
                ModuleId = moduleId, 
                ProfId = prof.Id
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(DocumentUploadViewModel model)
        {
            string filePath = null;

            if (model.FileUpload != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileUpload.FileName;
                filePath = Path.Combine("uploads", uniqueFileName);

                using (var fileStream = new FileStream(Path.Combine(uploadsFolder, uniqueFileName), FileMode.Create))
                {
                    await model.FileUpload.CopyToAsync(fileStream);
                }
            }

            var document = new Document
            {
                Titre = model.Titre,
                TypeDocumentId = model.TypeDocumentId,
                Format = model.Format,
                CheminFichier = filePath,
                Description = model.Description,
                DateCreation = DateTime.Now,
                ProfId = model.ProfId,
                ModuleId = model.ModuleId
            };

            _context.Documents.Add(document);
            await _context.SaveChangesAsync(); 

            if (model.SelectedGroupIds != null && model.SelectedGroupIds.Any())
            {
                foreach (var groupId in model.SelectedGroupIds)
                {
                    var documentGroupe = new DocumentGroupe
                    {
                        DocumentId = document.Id, 
                        GroupeId = groupId
                    };

                    _context.DocumentGroupes.Add(documentGroupe);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var prof = await _userManager.GetUserAsync(User);

            var document = await _context.Documents
                .Include(d => d.DocumentGroupes)
                .FirstOrDefaultAsync(d => d.Id == id && d.ProfId == prof.Id);

            if (document == null)
            {
                return NotFound();
            }

            var viewModel = new DocumentUploadViewModel
            {
                Id = document.Id,
                Titre = document.Titre,
                TypeDocumentId = document.TypeDocumentId,
                Format = document.Format,
                Description = document.Description,
                ModuleId = document.ModuleId,
                ProfId = document.ProfId,
                SelectedGroupIds = document.DocumentGroupes.Select(dg => dg.GroupeId).ToList(),
                TypeDocuments = await _context.TypeDocuments.ToListAsync(),
                AvailableGroups = await _context.Groupes
                    .Where(g => g.Professeurs.Any(p => p.Id == prof.Id))
                    .ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, DocumentUploadViewModel model)
        {
            var prof = await _userManager.GetUserAsync(User);

            var document = await _context.Documents
                .Include(d => d.DocumentGroupes)
                .FirstOrDefaultAsync(d => d.Id == id && d.ProfId == prof.Id);

            if (document == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                model.TypeDocuments = await _context.TypeDocuments.ToListAsync();
                model.AvailableGroups = await _context.Groupes
                    .Where(g => g.Professeurs.Any(p => p.Id == prof.Id))
                    .ToListAsync();
                return View(model);
            }

            string filePath = document.CheminFichier;

            // Gestion de l'upload du fichier
            if (model.FileUpload != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileUpload.FileName;
                filePath = Path.Combine("uploads", uniqueFileName);

                using (var fileStream = new FileStream(Path.Combine(uploadsFolder, uniqueFileName), FileMode.Create))
                {
                    await model.FileUpload.CopyToAsync(fileStream);
                }

                // Suppression de l'ancien fichier
                if (!string.IsNullOrEmpty(document.CheminFichier))
                {
                    var oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, document.CheminFichier);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
            }

            // Mise à jour des données du document
            document.Titre = model.Titre;
            document.TypeDocumentId = model.TypeDocumentId;
            document.Format = model.Format;
            document.CheminFichier = filePath;
            document.Description = model.Description;
            document.ModuleId = model.ModuleId;

            // Mise à jour des groupes associés
            var existingGroups = document.DocumentGroupes.ToList();
            _context.DocumentGroupes.RemoveRange(existingGroups);

            if (model.SelectedGroupIds != null && model.SelectedGroupIds.Any())
            {
                foreach (var groupId in model.SelectedGroupIds)
                {
                    var documentGroupe = new DocumentGroupe
                    {
                        DocumentId = document.Id,
                        GroupeId = groupId
                    };

                    _context.DocumentGroupes.Add(documentGroupe);
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var prof = await _userManager.GetUserAsync(User);

            // Récupérer le document
            var document = await _context.Documents
                .Include(d => d.TypeDocument)
                .Include(d => d.Module)
                .FirstOrDefaultAsync(d => d.Id == id && d.ProfId == prof.Id);

            if (document == null)
            {
                return NotFound();
            }

            // Passer le document à la vue
            return View(document);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prof = await _userManager.GetUserAsync(User);

            // Chercher le document à supprimer
            var document = await _context.Documents
                .Include(d => d.DocumentGroupes)
                .FirstOrDefaultAsync(d => d.Id == id && d.ProfId == prof.Id);

            if (document == null)
            {
                return NotFound();
            }

            // Suppression du fichier physique si le fichier existe
            if (!string.IsNullOrEmpty(document.CheminFichier))
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, document.CheminFichier);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath); // Suppression du fichier
                }
            }

            // Supprimer les entrées liées à ce document dans la table DocumentGroupes
            var documentGroups = _context.DocumentGroupes.Where(dg => dg.DocumentId == document.Id);
            _context.DocumentGroupes.RemoveRange(documentGroups);

            // Supprimer le document de la base de données
            _context.Documents.Remove(document);

            // Sauvegarder les changements
            await _context.SaveChangesAsync();

            // Message de confirmation (optionnel)
            TempData["SuccessMessage"] = "Document deleted successfully.";

            // Redirection vers l'index des documents
            return RedirectToAction(nameof(Index));
        }









    }
}
