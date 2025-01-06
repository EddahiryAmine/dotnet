
using EmsiStudySpace.Models;

public class UserGroup
{
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }


    public int GroupeId { get; set; }
    public Groupe Groupe { get; set; }


}
