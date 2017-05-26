using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();

        void AddTrip(Trip trip);

        void AddStop(string tripName, Stop stop, string userName);

        Task<bool> SaveChangesAsync();

        IEnumerable<Trip> GetTripsByUserName(string name);
        Trip GetUserTripByName(string tripName, string name);
    }
    
}