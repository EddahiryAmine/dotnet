namespace EmsiStudySpace.ViewModel
{
    public class UserHistoryViewModel
    {
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string Email { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime? LastLogoutTime { get; set; }
        public bool IsActive { get; set; }
    }
}
