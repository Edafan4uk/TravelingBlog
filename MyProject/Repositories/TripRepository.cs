using System;
using System.Collections.Generic;
using System.Linq;
using Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Entities.ExtendedModels;
using Entities;

namespace Repositories
{
    public class TripRepository : Repository<Trip>, ITripRepository
    {
        public TripRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public Trip GetTripById(int tripId)
        {
            return SingleOrDefault(t => t.Id.Equals(tripId));
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            return FindAll()
                .OrderBy(t => t.Name);
        }

        public TripExtended GetTripWithDetails(int id)
        {
            return new TripExtended(GetTripById(id))
            {
                Comments = RepositoryContext.Comments
                .Where(c => c.TripId == id),

                PostBlogs = RepositoryContext.PostBlogs
                .Where(pb => pb.TripId == id),

                TagTrips = RepositoryContext.TagTrips
                .Where(tt => tt.TripId == id),

                CountryTrips = RepositoryContext.CountryTrips
                .Where(ct => ct.TripId == id),

                Ratings = RepositoryContext.Ratings
                .Where(r => r.TripId == id)
            };
        }
    }
}
