using System;
using ServiceContracts.DTO;

namespace ServiceContracts
{
    public interface IPersonsService
    {
        PersonResponse AddPerson(PersonAddRequest? personAddRequest);
        List<PersonResponse> GetAllPersons();
        PersonResponse? GetPersonById(Guid? personId);
        List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString);
    }
}

