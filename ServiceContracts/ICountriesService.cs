using ServiceContracts.DTO;

namespace ServiceContracts
{
    //Adding Task means this method is awaitable and can be used with async/await
    public interface ICountriesService
    {
        Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest);
        Task<List<CountryResponse>> GetAllCountries();
        Task<CountryResponse>? GetCountryById(Guid? Id);
    }
}