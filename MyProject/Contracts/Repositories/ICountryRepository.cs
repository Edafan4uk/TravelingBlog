using Entities.Models;
using Entities.ExtendedModels;
using System.Collections.Generic;

namespace Contracts.Repositories
{
    public interface ICountryRepository: IRepository<Country>
    {
        IEnumerable<Country> GetAllCountries();
        Country GetCountryById(int countryId);
        CountryExtended GetCountryWithDetails(int countryId);
    }
}
