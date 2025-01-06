using EmsiStudySpace.Models;
using Microsoft.AspNetCore.Identity;
namespace EmsiStudySpace.Models { 
public class Module
{
    public int Id { get; set; }
    public string Nom { get; set; }

    public int FiliereId { get; set; } 
    public Filiere Filiere { get; set; }

        public ICollection<GroupeModule> GroupeModules { get; set; }
        public ICollection<ApplicationUser> Professeurs { get; set; } = new List<ApplicationUser>();
    public ICollection<UserModule> UserModules { get; set; } = new List<UserModule>();
}
}
