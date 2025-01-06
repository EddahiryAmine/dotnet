namespace EmsiStudySpace.Models
{
    public class GroupeModule
    {
        public int GroupeId { get; set; }
        public Groupe Groupe { get; set; }

        public int ModuleId { get; set; }
        public Module Module { get; set; }
    }
}
