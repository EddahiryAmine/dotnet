using EmsiStudySpace.Models;

namespace EmsiStudySpace.ViewModel
{
    public class ReservationViewModel
    {
     
        public DateTime DateReservation { get; set; }
        public TimeSpan HeureDebut { get; set; } 
        public TimeSpan HeureFin { get; set; }   
        public int RoomId { get; set; }
        public List<Room> Rooms { get; set; }
    }
}
