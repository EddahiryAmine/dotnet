using EmsiStudySpace.Models;
using Microsoft.AspNetCore.Identity;
public class Niveau
{
    public int Id { get; set; }
    public string Nom { get; set; }

    public ICollection<Filiere> Filieres { get; set; } = new List<Filiere>(); 
}