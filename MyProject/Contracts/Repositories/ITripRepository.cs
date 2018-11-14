using Entities.Models;
using Entities.ExtendedModels;
using System.Collections.Generic;

namespace Contracts.Repositories
{
    public interface ITripRepository : IRepository<Trip>
    {
        IEnumerable<Trip> GetAllTrips();
        Trip GetTripById(int tripId);
        TripExtended GetTripWithDetails(int tripId);
    }
}
