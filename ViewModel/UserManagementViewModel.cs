using EmsiStudySpace.Models;

namespace EmsiStudySpace.ViewModel
{
    public class UserManagementViewModel
    {
        public List<ApplicationUser> Students { get; set; }
        public List<ApplicationUser> Professors { get; set; }
    }
}
