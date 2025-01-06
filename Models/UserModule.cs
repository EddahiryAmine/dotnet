using System.Reflection;
namespace EmsiStudySpace.Models { 
public class UserModule
{
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int ModuleId { get; set; }  // Clé étrangère vers Module
        public Module Module { get; set; }
    }
}