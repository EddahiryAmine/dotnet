namespace EmsiStudySpace.Models
{
    public enum StatutReservation
    {
        EnCours,  
        Reserve,  
        Refuse    
    }

    public class Reservation
    {
        public int Id { get; set; }
        public DateTime DateReservation { get; set; }
        public TimeSpan? HeureDebut { get; set; } 
        public TimeSpan? HeureFin { get; set; }   

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public StatutReservation Statut { get; set; } = StatutReservation.EnCours;
    }
}
