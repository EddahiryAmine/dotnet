using EmsiStudySpace.Models;
using System.Collections.Generic;

public class Filiere
{
    public int Id { get; set; }
    public string Nom { get; set; }

    public ICollection<Module> Modules { get; set; } = new List<Module>();
    public ICollection<Groupe> Groupes { get; set; } = new List<Groupe>(); 
    public ICollection<ApplicationUser> Etudiants { get; set; } = new List<ApplicationUser>();
}


