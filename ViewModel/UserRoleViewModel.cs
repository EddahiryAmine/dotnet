namespace EmsiStudySpace.ViewModel
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public bool IsActive { get; set; } = true;
        public List<string> Roles { get; set; }
    }
}
