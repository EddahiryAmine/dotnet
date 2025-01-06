using EmsiStudySpace.Models;
using Microsoft.AspNetCore.Identity;
namespace EmsiStudySpace.Models { 
public class Groupe
{
    public int Id { get; set; }
    public string Nom { get; set; }

    public int FiliereId { get; set; }
    public Filiere Filiere { get; set; }

        public ICollection<DocumentGroupe> DocumentGroupes { get; set; } // Relation many-to-many avec Document

        public ICollection<ApplicationUser> Etudiants { get; set; } = new List<ApplicationUser>();

    public ICollection<ApplicationUser> Professeurs { get; set; } = new List<ApplicationUser>();
        public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();

        public ICollection<GroupeModule> GroupeModules { get; set; }
    }
}