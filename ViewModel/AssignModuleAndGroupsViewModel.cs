using System.Collections.Generic;
using EmsiStudySpace.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmsiStudySpace.ViewModel
{
    public class AssignModuleAndGroupsViewModel
    {
        public string UserId { get; set; }
        public string? UserName { get; set; }
        public int? SelectedModuleId { get; set; }
        public List<Module> Modules { get; set; } = new List<Module>();
        public List<int> SelectedGroupIds { get; set; } = new List<int>();
        public List<Groupe> Groupes { get; set; } = new List<Groupe>();  
    }
}
