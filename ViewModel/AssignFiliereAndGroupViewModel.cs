using System.Collections.Generic;
using EmsiStudySpace.Models;

namespace EmsiStudySpace.ViewModel
{
    public class AssignFiliereAndGroupViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        public int? SelectedFiliereId { get; set; }  // ID de la filière sélectionnée
        public List<Filiere> Filieres { get; set; } = new List<Filiere>(); // Liste des filières disponibles

        public int? SelectedGroupeId { get; set; }  // ID du groupe sélectionné
        public List<Groupe> Groupes { get; set; } = new List<Groupe>(); // Liste des groupes disponibles
    }
}
