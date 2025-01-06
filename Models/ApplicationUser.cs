using EmsiStudySpace.Models;
using Microsoft.AspNetCore.Identity;
namespace EmsiStudySpace.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? LastLoginTime { get; set; }
        public DateTime? LastLogoutTime { get; set; }
        public int? FiliereId { get; set; }
        public Filiere Filiere { get; set; }

        public int? GroupeId { get; set; }
        public Groupe Groupe { get; set; }

        public ICollection<Groupe> Groupes { get; set; } = new List<Groupe>();
        public ICollection<Module> ModulesTeaching { get; set; } = new List<Module>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public ICollection<UserModule> UserModules { get; set; } = new List<UserModule>();
        public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
        public ICollection<UserConnectionHistory> ConnectionHistories { get; set; }

    }
}