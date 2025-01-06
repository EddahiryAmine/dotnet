using EmsiStudySpace.Models;
using EmsiStudySpace.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmsiStudySpace.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users;
            var userRolesViewModel = new List<UserRoleViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRolesViewModel.Add(new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Nom=user.Nom,
                    Prenom = user.Prenom,

                    Roles = roles.ToList()
                });
            }

            return View(userRolesViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AssignRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var roles = _roleManager.Roles.ToList();
            var model = new AssignRoleViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = roles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(AssignRoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            var existingRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, existingRoles);

            // Assigne le nouveau rôle
            if (!string.IsNullOrEmpty(model.SelectedRole))
            {
                await _userManager.AddToRoleAsync(user, model.SelectedRole);
            }

            TempData["successMessage"] = $"Le rôle a été assigné avec succès à {user.UserName}.";
            return RedirectToAction("Index");
        }

        //---------------------------------------------------------------------------



        public async Task<IActionResult> IndexAssign()
        {
            var users = await _userManager.Users.ToListAsync();
            var students = new List<ApplicationUser>();
            var professors = new List<ApplicationUser>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Etudiant"))
                {
                    students.Add(user);
                }
                else if (roles.Contains("Professeur"))
                {
                    professors.Add(user);
                }
            }

            var viewModel = new UserManagementViewModel
            {
                Students = students,
                Professors = professors
            };

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> AssignFiliereAndGroup(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var viewModel = new AssignFiliereAndGroupViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Filieres = await _context.Filieres.ToListAsync(),
                Groupes = user.FiliereId != null ? await _context.Groupes
                                    .Where(g => g.FiliereId == user.FiliereId)
                                    .ToListAsync() : new List<Groupe>(),
                SelectedFiliereId = user.FiliereId,
                SelectedGroupeId = user.GroupeId
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AssignFiliereAndGroup(AssignFiliereAndGroupViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();

            user.FiliereId = model.SelectedFiliereId;
            user.GroupeId = model.SelectedGroupeId;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            return RedirectToAction(nameof(IndexAssign));
        }



        [HttpGet]
        public async Task<IActionResult> AssignModuleAndGroups(string userId)
        {
            var professor = await _userManager.FindByIdAsync(userId);
            if (professor == null)
            {
                return NotFound(); 
            }

            var viewModel = new AssignModuleAndGroupsViewModel
            {
                UserId = professor.Id,
                UserName = $"{professor.Nom} {professor.Prenom}",
                Modules = await _context.Modules.ToListAsync(), 
                Groupes = await _context.Groupes.ToListAsync(), 
                SelectedModuleId = professor.ModulesTeaching.FirstOrDefault()?.Id, 
                SelectedGroupIds = professor.Groupes.Select(g => g.Id).ToList() 
            };

            return View(viewModel); 
        }


        [HttpPost]
        public async Task<IActionResult> AssignModuleAndGroups(AssignModuleAndGroupsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var professor = await _userManager.FindByIdAsync(model.UserId);
                if (professor != null)
                {
                    professor.ModulesTeaching.Clear(); 
                    if (model.SelectedModuleId.HasValue)
                    {
                        var module = await _context.Modules.FindAsync(model.SelectedModuleId.Value);
                        if (module != null)
                        {
                            professor.ModulesTeaching.Add(module); 
                        }
                    }

                    professor.Groupes.Clear(); 
                    if (model.SelectedGroupIds != null)
                    {
                        foreach (var groupId in model.SelectedGroupIds)
                        {
                            var group = await _context.Groupes.FindAsync(groupId);
                            if (group != null)
                            {
                                professor.Groupes.Add(group); 
                            }
                        }
                    }

                    var result = await _userManager.UpdateAsync(professor);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("IndexAssign"); 
                    }
                }
            }

            model.Modules = await _context.Modules.ToListAsync();
            model.Groupes = await _context.Groupes.ToListAsync();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> GetGroupesByFiliere(int filiereId)
        {
            var groupes = await _context.Groupes
                .Where(g => g.FiliereId == filiereId)
                .ToListAsync();

            var groupeList = groupes.Select(g => new { id = g.Id, nom = g.Nom }).ToList();

            return Json(groupeList); 
        }



        //-----------------------------------------------------------
        //Partie pour les reservation
        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return NotFound();

            reservation.Statut = StatutReservation.Reserve;
            await _context.SaveChangesAsync();

            return RedirectToAction("PendingReservations");
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return NotFound();

            reservation.Statut = StatutReservation.Refuse;
            await _context.SaveChangesAsync();

            return RedirectToAction("PendingReservations");
        }
        public async Task<IActionResult> PendingReservations()
        {
            var pendingReservations = await _context.Reservations
                .Include(r => r.Room)
                .Include(r => r.User)
                .Where(r => r.Statut == StatutReservation.EnCours)
                .ToListAsync();

            return View(pendingReservations);
        }
        [HttpGet]
        public async Task<IActionResult> AllReservations()
        {
            var reservations = await _context.Reservations
                .Include(r => r.Room)
                .Include(r => r.User)
                .ToListAsync();

            return View(reservations);
        }


        public void UpdateReservationStatus(int reservationId, StatutReservation statut)
        {
            var reservation = _context.Reservations.Find(reservationId);
            if (reservation != null)
            {
                reservation.Statut = statut;
                _context.SaveChanges();
            }
        }









        //Historique :
        public IActionResult UserHistory()
        {
            var users = _userManager.Users
                .Select(u => new UserHistoryViewModel
                {
                    Nom = u.Nom,
                    Prenom = u.Prenom,
                    Email = u.Email,
                    LastLoginTime = u.LastLoginTime,
                    LastLogoutTime = u.LastLogoutTime,
                    IsActive = u.IsActive
                })
                .ToList();

            return View(users);
        }

        public IActionResult AllUserHistory()
        {
            var histories = _context.UserConnectionHistories
                .Include(h => h.User)
                .Select(h => new
                {
                    h.User.UserName,
                    h.LoginTime,
                    h.LogoutTime
                })
                .ToList();

            return View(histories);
        }
        //Activer et desactiver les comptes : 

        public async Task<IActionResult> UserList()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }
        public async Task<IActionResult> ActiverDasactiver(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.IsActive = !user.IsActive; // Changer l'état du compte (activer/désactiver)
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("UserList"); // Rediriger vers la page avec la liste des utilisateurs
        }


    }
}

