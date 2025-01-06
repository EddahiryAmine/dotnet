using EmsiStudySpace.Models;

namespace EmsiStudySpace.ViewModel
{
    public class UserDataViewModel
    {

        public ApplicationUser User { get; set; }
        public List<Module> Modules { get; set; }
        public List<Groupe> Groupes { get; set; }
        public List<Filiere> Filieres { get; set; }
    }
}
