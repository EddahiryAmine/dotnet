using EmsiStudySpace.Models;

namespace EmsiStudySpace.ViewModel
{
    public class DocumentsByTypeViewModel
    {
        public int Id {  get; set; }
        public string Titre { get; set; }
        public string TypeDocument { get; set; } 
        public string Description { get; set; }
        public DateTime DateCreation { get; set; }
        public string CheminFichier { get; set; }

    }
}
