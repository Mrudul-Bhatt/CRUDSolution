using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
    //In repostories we should do all DB operations, other tasks like validations and so on should be done in Services (Business Layer)
    //Pure Database interaction should be done in repository
    public class CountriesRepository : ICountriesRepository
    {
        private readonly ApplicationDbContext _db;

        public CountriesRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        //Before adding a country to DB, we have to check whether it is a duplicate country being added. This is a business logic which should already be done in Services. Repository should get only validated object and perform only db related tasks
        public async Task<Country> AddCountry(Country country)
        {
            _db.Countries!.Add(country);
            await _db.SaveChangesAsync();
            return country;
        }

        public async Task<List<Country>> GetAllCountries()
        {
            return await _db.Countries!.ToListAsync();
        }

        public async Task<Country?> GetCountryByCountryID(Guid countryID)
        {
            return await _db.Countries!.FirstOrDefaultAsync(country => country.CountryID == countryID);
        }

        public async Task<Country?> GetCountryByCountryName(string countryName)
        {
            return await _db.Countries!.FirstOrDefaultAsync(country => country.CountryName == countryName);
        }
    }
}