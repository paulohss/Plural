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


        public IEnumerable<Trip> GetAllTrips()
        {
            return _context.Trips.ToList();
        }
    }
}
