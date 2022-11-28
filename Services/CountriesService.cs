using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        private readonly List<Country> _countries;

        public CountriesService()
        {
            _countries = new List<Country>();
        }

        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
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
            if (_countries.Where(country => country.CountryName == countryAddRequest.CountryName).Any())
            {
                throw new ArgumentException("Country name already exists");
            }

            Country country = countryAddRequest.ToCountry();
            country.CountryID = Guid.NewGuid();
            _countries.Add(country);
            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {
            return _countries.Select(country => country.ToCountryResponse()).ToList();
        }

        public CountryResponse? GetCountryById(Guid? Id)
        {
            if (Id == null)
            {
                return null;
            }

            Country? country = _countries.FirstOrDefault(country => country.CountryID == Id);

            if (country == null)
            {
                return null;
            }

            return country.ToCountryResponse();
        }
    }
}

