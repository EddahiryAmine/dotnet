using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EmsiStudySpace.Models;
using EmsiStudySpace.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace EmsiStudySpace.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public HomeController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("Utilisateur non trouvé.");
            }

            
            var roles = await _userManager.GetRolesAsync(user);

            var modules = new List<Module>();
            var groupes = new List<Groupe>();
            var filieres = new List<Filiere>(); 

            if (roles.Contains("Professeur"))
            {
                modules = _context.UserModules
                                   .Where(um => um.UserId == user.Id)
                                   .Select(um => um.Module)
                                   .ToList();

                groupes = _context.UserGroups
                                   .Where(ug => ug.UserId == user.Id)
                                   .Select(ug => ug.Groupe)
                                   .ToList();
            }

            if (roles.Contains("Etudiant"))
            {

                var filiere = await _context.Filieres
         .Where(f => f.Id == user.FiliereId)
         .Select(f => new { f.Nom })
         .FirstOrDefaultAsync();

                var groupe = await _context.Groupes
                    .Where(g => g.Id == user.GroupeId)
                    .Select(g => new { g.Nom })
                    .FirstOrDefaultAsync();

                // Assurez-vous que vous avez des valeurs pour la filière et le groupe
                ViewBag.FiliereNom = filiere?.Nom ?? "Non attribuée";
                ViewBag.GroupeNom = groupe?.Nom ?? "Non attribué";
            }




            var viewModel = new UserDataViewModel
            {
                User = user,
                Modules = modules,
                Groupes = groupes,
                Filieres = filieres 
            };

            return View(viewModel);
        }


        private List<Module> GetModulesByUser(string userId)
        {
            return new List<Module>
            {
            };
        }

        private List<Groupe> GetGroupesByUser(string userId)
        {
            return new List<Groupe>
            {
            };
        }

        private List<Filiere> GetFilieresByUser(string userId)
        {
            return new List<Filiere>
            {
            };
        }
      
        public async Task<IActionResult> Statistics()
        {
            var statistics = new StatisticsViewModel
            {
                TotalProfessors = await _context.Users.CountAsync(u => u.ModulesTeaching.Any()),
                TotalStudents = await _context.Users.CountAsync(u => u.FiliereId != null),
                TotalModules = await _context.Modules.CountAsync(),
                TotalGroups = await _context.Groupes.CountAsync(),
                TotalFiliere = await _context.Filieres.CountAsync(),
                TotalRooms = await _context.Rooms.CountAsync(),
                GroupsPerFiliere = await _context.Filieres
                    .Select(f => new GroupsPerFiliere
                    {
                        FiliereName = f.Nom,
                        GroupCount = f.Groupes.Count
                    })
                    .ToListAsync(),
                StudentsPerFiliere = await _context.Filieres
                    .Select(f => new StudentsPerFiliere
                    {
                        FiliereName = f.Nom,
                        StudentCount = f.Etudiants.Count
                    })
                    .ToListAsync()
            };

            return View(statistics);
        }

    }

    
}
