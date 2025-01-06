using System.ComponentModel.DataAnnotations.Schema;

namespace EmsiStudySpace.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public int TypeDocumentId { get; set; } 
        public TypeDocument TypeDocument { get; set; }
        public string Format { get; set; } 
        public string CheminFichier { get; set; } 
        public string Description { get; set; }

        public int? GroupeId { get; set; }
        public Groupe Groupe { get; set; }

        public int? ModuleId { get; set; }
        public Module Module { get; set; }

        public string ProfId { get; set; }
        public ApplicationUser Prof { get; set; }
        public ICollection<DocumentGroupe> DocumentGroupes { get; set; }

        public DateTime DateCreation { get; set; }

    }
}
