namespace EmsiStudySpace.Models
{
    public class TypeDocument
    {
        public int Id { get; set; }
        public string Nom { get; set; } 

        public ICollection<Document> Documents { get; set; }
    }
}
