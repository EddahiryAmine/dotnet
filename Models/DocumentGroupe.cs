namespace EmsiStudySpace.Models
{
    public class DocumentGroupe
    {
        public int DocumentId { get; set; }
        public Document Document { get; set; }

        public int GroupeId { get; set; }
        public Groupe Groupe { get; set; }
    }
}
