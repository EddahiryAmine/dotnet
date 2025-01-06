using Microsoft.AspNetCore.Identity;

namespace EmsiStudySpace.ViewModel
{
    public class AssignRoleViewModel
    {
        public string UserId { get; set; }
       

        public string UserName { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public string SelectedRole { get; set; }
    }
}
