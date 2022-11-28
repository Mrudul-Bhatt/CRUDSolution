using ServiceContracts.DTO;

namespace ServiceContracts
{
    public interface ICountriesService
    {
        CountryResponse AddCountry(CountryAddRequest? countryAddRequest);
        List<CountryResponse> GetAllCountries();
        CountryResponse? GetCountryById(Guid? Id);
    }
}