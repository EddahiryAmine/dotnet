using Microsoft.AspNetCore.Mvc;
using EmsiStudySpace.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using EmsiStudySpace.ViewModel;
using Microsoft.AspNetCore.Hosting;
using System.IO;

[Authorize(Roles = "Etudiant")]
public class StudentController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public StudentController(AppDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _userManager = userManager;
        _webHostEnvironment = webHostEnvironment;
    }

    // Action pour afficher les types de documents disponibles
    public async Task<IActionResult> DocumentTypes()
    {
        var documentTypes = await _context.TypeDocuments.ToListAsync();
        return View(documentTypes);
    }

    // Action pour afficher les documents filtrés par le type sélectionné
    [HttpGet("documents/{typeDocumentId?}")]
    public async Task<IActionResult> DocumentsByType(int? typeDocumentId)
    {
        var userId = User.Identity.Name;
        var user = await _userManager.FindByNameAsync(userId);
        var groupId = user.GroupeId;

        // Si aucun type de document n'est sélectionné, afficher tous les types
        if (!typeDocumentId.HasValue)
        {
            return View("DocumentTypes"); // Vue pour sélectionner un type de document
        }

        // Récupérer les documents filtrés par le type sélectionné
        var documents = await _context.DocumentGroupes
            .Where(dg => dg.GroupeId == groupId && dg.Document.TypeDocumentId == typeDocumentId)
            .Include(dg => dg.Document)
            .ThenInclude(d => d.TypeDocument)  // Inclure les informations sur le type de document
            .Select(dg => dg.Document)
            .ToListAsync();

        var viewModel = documents.Select(d => new DocumentsByTypeViewModel
        {
            Titre = d.Titre,
            TypeDocument = d.TypeDocument.Nom, // Inclure le nom du type de document
            Description = d.Description,
            DateCreation = d.DateCreation,
            CheminFichier = d.CheminFichier
        }).ToList();

        return View(viewModel);
    }

    // Action pour télécharger un fichier
    public IActionResult Download(string cheminFichier)
    {
        if (string.IsNullOrEmpty(cheminFichier))
        {
            return NotFound("Le chemin du fichier est introuvable.");
        }

        // Construire le chemin complet
        var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", cheminFichier);

        // Vérification si le fichier existe
        if (!System.IO.File.Exists(fullPath))
        {
            return NotFound($"Fichier introuvable. Chemin : {fullPath}");
        }

        // Déterminer le type MIME
        var contentType = "application/octet-stream"; // Vous pouvez adapter cela en fonction des extensions
        return PhysicalFile(fullPath, contentType, Path.GetFileName(fullPath));
    }

}
