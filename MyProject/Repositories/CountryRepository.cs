using System;
using System.Linq;
using Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Entities.ExtendedModels;
using Entities;
using System.Collections.Generic;

namespace Repositories
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public Country GetCountryById(int countryId)
        {
            return SingleOrDefault(c => c.Id.Equals(countryId));
        }

        public IEnumerable<Country> GetAllCountries()
        {
            return FindAll()
                .OrderBy(c => c.Name);
        }

        public CountryExtended GetCountryWithDetails(int id)
        {
            return new CountryExtended(GetCountryById(id))
            {
                Users = RepositoryContext.Users
                .Where(u => u.CountryId == id),

                CountryTrips = RepositoryContext.CountryTrips
                .Where(ct => ct.CountryId == id),

                 CountryPostBlogs = RepositoryContext.CountryPostBlogs
                .Where(cpb => cpb.CountryId == id)
            };
        }
    }
}
