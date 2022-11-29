using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        //In-Memory Collection
        //private readonly List<Country> _countries;
        private readonly PersonsDbContext _db;


        public CountriesService(PersonsDbContext personsDbContext)
        {
            //_countries = new List<Country>();
            _db = personsDbContext;

            //Replaced by Seed Data
            //if (initialize)
            //{
            //    _db.AddRange(new List<Country>() {
            //        new Country() {  CountryID = Guid.Parse("000C76EB-62E9-4465-96D1-2C41FDB64C3B"), CountryName = "USA" },

            //        new Country() { CountryID = Guid.Parse("32DA506B-3EBA-48A4-BD86-5F93A2E19E3F"), CountryName = "Canada" },

            //        new Country() { CountryID = Guid.Parse("DF7C89CE-3341-4246-84AE-E01AB7BA476E"), CountryName = "UK" },

            //        new Country() { CountryID = Guid.Parse("15889048-AF93-412C-B8F3-22103E943A6D"), CountryName = "India" },

            //        new Country() { CountryID = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB"), CountryName = "Australia" }
            //    });
            //}
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
            if (_db.Countries.Where(country => country.CountryName == countryAddRequest.CountryName).Any())
            {
                throw new ArgumentException("Country name already exists");
            }

            //if (_db.Countries.Count(country => country.CountryName == countryAddRequest.CountryName) > 0)
            //{
            //    throw new ArgumentException("Country name already exists");
            //}

            Country country = countryAddRequest.ToCountry();
            country.CountryID = Guid.NewGuid();
            _db.Countries.Add(country);

            //Important to save our changes to db
            _db.SaveChanges();

            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {
            return _db.Countries.Select(country => country.ToCountryResponse()).ToList();
        }

        public CountryResponse? GetCountryById(Guid? Id)
        {
            if (Id == null)
            {
                return null;
            }

            Country? country = _db.Countries.FirstOrDefault(country => country.CountryID == Id);

            if (country == null)
            {
                return null;
            }

            return country.ToCountryResponse();
        }
    }
}

