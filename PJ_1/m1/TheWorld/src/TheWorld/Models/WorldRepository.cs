using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WordContext _context;
        private ILogger<WordContext> _logger;

        public WorldRepository(WordContext context, ILogger<WordContext> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddStop(string tripName, Stop stop, string userName)
        {
            var trip = GetUserTripByName(tripName, userName);

            if(trip != null)
            {
                trip.Stops.Add(stop);
                _context.Stops.Add(stop);
            }
        }

        public void AddTrip(Trip trip)
        {
            _context.Add(trip);
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            return _context.Trips.ToList();
        }

        public Trip GetTripByName(string tripName)
        {
            return _context.Trips
                .Include(t => t.Stops)
                .Where(t => t.Name.ToLower() == tripName.ToLower())
                .FirstOrDefault();
        }

        public IEnumerable<Trip> GetTripsByUserName(string userName)
        {
            return _context.Trips
                .Include(t=>t.Stops)
                .Where(x => x.UserName == userName)
                .ToList();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public Trip GetUserTripByName(string tripName, string userName)
        {
            return _context.Trips
                .Include(t => t.Stops)
                .Where(x => x.UserName == userName && x.Name == tripName)
                .FirstOrDefault();
        }
    }
}
