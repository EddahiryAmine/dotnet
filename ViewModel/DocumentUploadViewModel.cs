
using EmsiStudySpace.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EmsiStudySpace.ViewModels
{
    public class DocumentUploadViewModel
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "Le titre est requis.")]
        [StringLength(100, ErrorMessage = "Le titre ne peut pas dépasser 100 caractères.")]
        public string Titre { get; set; }

        [Required(ErrorMessage = "Veuillez sélectionner un type de document.")]
        [Display(Name = "Type de Document")]
        public int TypeDocumentId { get; set; }

        [Required(ErrorMessage = "Veuillez spécifier un format.")]
        [StringLength(10, ErrorMessage = "Le format ne peut pas dépasser 10 caractères.")]
        public string Format { get; set; }

        [Display(Name = "Fichier")]
        public IFormFile? FileUpload { get; set; }

        [Display(Name = "Date de Création")]
        public DateTime DateCreation { get; set; } = DateTime.Now;

        [StringLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères.")]
        public string Description { get; set; }

        [Display(Name = "Groupes Sélectionnés")]
        public List<int> SelectedGroupIds { get; set; } = new List<int>();

        public int? ModuleId { get; set; }

        public string ProfId { get; set; }

        [Display(Name = "Types de Documents Disponibles")]
        public IEnumerable<TypeDocument> TypeDocuments { get; set; } = new List<TypeDocument>();

        [Display(Name = "Groupes Disponibles")]
        public IEnumerable<Groupe> AvailableGroups { get; set; } = new List<Groupe>();
    }
}

