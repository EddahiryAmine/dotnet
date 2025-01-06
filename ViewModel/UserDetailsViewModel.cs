namespace EmsiStudySpace.ViewModel
{
    public class UserDetailsViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Filiere { get; set; }
        public List<string> Groupes { get; set; }
        public List<string> ModulesTeaching { get; set; }
        public bool IsAssigned { get; set; }
    }
}
