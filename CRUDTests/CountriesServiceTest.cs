using System;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace CRUDTests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;

        public CountriesServiceTest()
        {
            _countriesService = new CountriesService(false);
        }

        #region AddCountry
        //CountryAddRequest null, throw ArgumentNullExcp
        [Fact]
        public void AddCountry_NullCountry()
        {
            CountryAddRequest? countryAddRequest = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                _countriesService.AddCountry(countryAddRequest);
            });
        }

        //CountryName null, ArgExp
        [Fact]
        public void AddCountry_CountryNameIsNull()
        {
            CountryAddRequest? countryAddRequest = new CountryAddRequest() { CountryName = null };

            Assert.Throws<ArgumentException>(() =>
            {
                _countriesService.AddCountry(countryAddRequest);
            });
        }

        //CountryName duplicate, ArgExp
        [Fact]
        public void AddCountry_CountryNameIsDuplicate()
        {
            CountryAddRequest? countryAddRequest1 = new CountryAddRequest() { CountryName = "Bahamas" };
            CountryAddRequest? countryAddRequest2 = new CountryAddRequest() { CountryName = "Bahamas" };

            Assert.Throws<ArgumentException>(() =>
            {
                _countriesService.AddCountry(countryAddRequest1);
                _countriesService.AddCountry(countryAddRequest2);
            });
        }

        //CountryName valid, add country to list of countries
        [Fact]
        public void AddCountry_ProperCountryDetails()
        {
            CountryAddRequest? countryAddRequest = new CountryAddRequest() { CountryName = "Japan" };

            CountryResponse countryResponse = _countriesService.AddCountry(countryAddRequest);

            List<CountryResponse> countryResponses = _countriesService.GetAllCountries();

            Assert.True(countryResponse.CountryID != Guid.Empty);
            Assert.Contains(countryResponse, countryResponses);
        }
        #endregion

        #region GetAllCountries

        [Fact]
        public void GetAllCountries_EmptyList()
        {
            List<CountryResponse> countryResponses = _countriesService.GetAllCountries();

            Assert.Empty(countryResponses);
        }

        [Fact]
        public void GetAllCountries_AddFewCountries()
        {
            List<CountryAddRequest> countryAddRequests = new List<CountryAddRequest>()
            {
                new CountryAddRequest() {CountryName = "Canada"},
                new CountryAddRequest() {CountryName = "South Korea"}
            };

            List<CountryResponse> countryResponses = new List<CountryResponse>();

            foreach (CountryAddRequest countryAddRequest in countryAddRequests)
            {
                countryResponses.Add(_countriesService.AddCountry(countryAddRequest));
            }

            List<CountryResponse> actualCountryResponses = _countriesService.GetAllCountries();

            foreach (CountryResponse expectedCountry in countryResponses)
            {
                Assert.Contains(expectedCountry, actualCountryResponses);
            }
        }

        #endregion

        #region GetCountryById

        [Fact]
        public void GetCountryById_NullCountryId()
        {
            Guid? Id = null;

            CountryResponse? countryResponse = _countriesService.GetCountryById(Id);

            Assert.Null(countryResponse);
        }

        [Fact]
        public void GetCountryById_ValidId()
        {
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = "China"
            };

            CountryResponse countryResponse = _countriesService.AddCountry(countryAddRequest);

            CountryResponse? countryResponseById = _countriesService.GetCountryById(countryResponse.CountryID);

            Assert.Equal(countryResponse, countryResponseById);
        }

        #endregion

    }
}

