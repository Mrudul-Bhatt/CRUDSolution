using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        //In-Memory Collection
        //private readonly List<Country> _countries;
        private readonly ICountriesRepository _countriesRepository;

        public CountriesService(ICountriesRepository countriesRepository)
        {
            _countriesRepository = countriesRepository;
        }

        public async Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest)
        {
            if (countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }

            if (countryAddRequest.CountryName == null)
            {
                throw new ArgumentException(nameof(countryAddRequest.CountryName));
            }

            //use Any() instead of Count() here for performance 
            //using wait means same server thread is also serving other requests without being blocked and also waiting for db response
            if (await _countriesRepository.GetCountryByCountryName(countryAddRequest.CountryName) != null)
            {
                throw new ArgumentException("Country name already exists");
            }

            //if (_db.Countries.Count(country => country.CountryName == countryAddRequest.CountryName) > 0)
            //{
            //    throw new ArgumentException("Country name already exists");
            //}

            Country country = countryAddRequest.ToCountry();
            country.CountryID = Guid.NewGuid();
            await _countriesRepository.AddCountry(country);

            return country.ToCountryResponse();
        }

        public async Task<List<CountryResponse>> GetAllCountries()
        {
            List<Country> countries = await _countriesRepository.GetAllCountries();

            return countries.Select(country => country.ToCountryResponse()).ToList();
        }

        public async Task<CountryResponse?> GetCountryById(Guid? Id)
        {
            if (Id == null)
            {
                return null;
            }

            Country? country = await _countriesRepository.GetCountryByCountryID(Id.Value);

            if (country == null)
            {
                return null;
            }

            return country.ToCountryResponse();
        }
    }
}

