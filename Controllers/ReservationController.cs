using EmsiStudySpace.Models;
using EmsiStudySpace.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmsiStudySpace.Controllers
{

    public class ReservationController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ReservationController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> MyReservations()
        {
            var user = await _userManager.GetUserAsync(User);

            var reservations = await _context.Reservations
                .Include(r => r.Room)
                .Where(r => r.UserId == user.Id)
                .ToListAsync();

            return View(reservations);
        }


        [HttpGet]
        public IActionResult Create()
        {
            // Filtrer uniquement les salles qui ne sont pas réservées avec un statut "Reserve"
            var availableRooms = _context.Rooms
                .Include(r => r.Reservations)
                .Where(r => !r.Reservations.Any(res => res.Statut == StatutReservation.Reserve))
                .ToList();

            var viewModel = new ReservationViewModel
            {
                Rooms = availableRooms
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ReservationViewModel model)
        {
            
                var isRoomReserved = await _context.Reservations
                    .AnyAsync(r => r.RoomId == model.RoomId &&
                                   r.Statut == StatutReservation.Reserve &&
                                   r.DateReservation == model.DateReservation &&
                                   (r.HeureDebut < model.HeureFin && r.HeureFin > model.HeureDebut));

                if (isRoomReserved)
                {
                    ModelState.AddModelError("", "La salle est déjà réservée pour ce créneau horaire.");
                    model.Rooms = _context.Rooms.ToList();
                    return View(model);
                }

                var reservation = new Reservation
                {
                    DateReservation = model.DateReservation,
                    HeureDebut = model.HeureDebut,
                    HeureFin = model.HeureFin,
                    RoomId = model.RoomId,
                    UserId = _userManager.GetUserId(User),
                    Statut = StatutReservation.EnCours
                };

                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();

                return RedirectToAction("MyReservations");
            

            model.Rooms = _context.Rooms.ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Annuler(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            if (reservation.Statut == StatutReservation.Reserve)
            {
                TempData["Error"] = "Vous ne pouvez pas annuler une réservation déjà approuvée.";
                return RedirectToAction("MyReservations");
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Réservation annulée avec succès.";
            return RedirectToAction("MyReservations");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Room)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            // Vérifier si la réservation est expirée ou approuvée
            if (reservation.Statut == StatutReservation.Reserve &&
                (reservation.DateReservation.Date < DateTime.Now.Date ||
                (reservation.DateReservation.Date == DateTime.Now.Date && reservation.HeureFin <= DateTime.Now.TimeOfDay)))
            {
                reservation.Statut = StatutReservation.Refuse; // Marquer comme expirée
                _context.Reservations.Update(reservation);
                await _context.SaveChangesAsync();

                TempData["Error"] = "La réservation a expiré et ne peut plus être modifiée.";
                return RedirectToAction("MyReservations");
            }

            if (reservation.Statut == StatutReservation.Reserve)
            {
                TempData["Error"] = "Vous ne pouvez pas modifier une réservation déjà approuvée.";
                return RedirectToAction("MyReservations");
            }

            var viewModel = new ReservationViewModel
            {
                DateReservation = reservation.DateReservation,
                RoomId = reservation.RoomId,
                Rooms = _context.Rooms
                    .Include(r => r.Reservations)
                    .Where(r => !r.Reservations.Any(res =>
                        (res.Statut == StatutReservation.Reserve && res.Id != id)))
                    .ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ReservationViewModel model)
        {
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            // Vérifier si la réservation est expirée ou approuvée
            if (reservation.Statut == StatutReservation.Reserve &&
                (reservation.DateReservation.Date < DateTime.Now.Date ||
                (reservation.DateReservation.Date == DateTime.Now.Date && reservation.HeureFin <= DateTime.Now.TimeOfDay)))
            {
                reservation.Statut = StatutReservation.Refuse; // Marquer comme expirée
                _context.Reservations.Update(reservation);
                await _context.SaveChangesAsync();

                TempData["Error"] = "La réservation a expiré et ne peut plus être modifiée.";
                return RedirectToAction("MyReservations");
            }

            if (reservation.Statut == StatutReservation.Reserve)
            {
                TempData["Error"] = "Vous ne pouvez pas modifier une réservation déjà approuvée.";
                return RedirectToAction("MyReservations");
            }

            // Mise à jour des données de la réservation
            reservation.DateReservation = model.DateReservation;
            reservation.RoomId = model.RoomId;

            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Réservation modifiée avec succès.";
            return RedirectToAction("MyReservations");
        }
















    }

}
