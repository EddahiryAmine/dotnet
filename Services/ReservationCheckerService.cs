using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmsiStudySpace.Models;

public class ReservationCheckerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public ReservationCheckerService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var now = DateTime.Now;

                // Trouver les réservations expirées
                var expiredReservations = await context.Reservations
                    .Where(r => r.Statut == StatutReservation.Reserve &&
                                r.DateReservation.Date <= now.Date &&
                                r.HeureFin <= now.TimeOfDay)
                    .ToListAsync();

                foreach (var reservation in expiredReservations)
                {
                    reservation.Statut = StatutReservation.Refuse; 
                }

                if (expiredReservations.Any())
                {
                    await context.SaveChangesAsync(); 
                }
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); 
        }
    }
}
