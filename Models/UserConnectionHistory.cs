namespace EmsiStudySpace.Models
{
    public class UserConnectionHistory
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public ApplicationUser User { get; set; }
    }
}
