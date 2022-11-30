using Entities;

namespace RepositoryContracts
{
    /// <summary>
    /// Represents Data Access Layer (DAL) for Persons Entity
    /// Repository doesn't work with DTO objects, it works with Entities directly
    /// </summary>
    public interface ICountriesRepository
    {
        Task<Country> AddCountry(Country country);
        Task<List<Country>> GetAllCountries();
        Task<Country?> GetCountryByCountryID(Guid countryID);
        Task<Country?> GetCountryByCountryName(string countryName);
    }
}