namespace EmsiStudySpace.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
