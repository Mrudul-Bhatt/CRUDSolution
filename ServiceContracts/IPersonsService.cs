using System;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace ServiceContracts
{
    public interface IPersonsService
    {
        PersonResponse AddPerson(PersonAddRequest? personAddRequest);
        List<PersonResponse> GetAllPersons();
        PersonResponse? GetPersonById(Guid? personId);
        List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString);
        List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder);
        PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest);
        bool DeletePerson(Guid? personId);
    }
}

