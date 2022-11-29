using System;
using System.ComponentModel.DataAnnotations;
using Entities;
using ServiceContracts.Enums;

namespace ServiceContracts.DTO
{
    public class PersonAddRequest
    {
        [Required(ErrorMessage = "Person Name can't be blank")]
        public string? PersonName { get; set; }

        [Required(ErrorMessage = "Email can't be blank")]
        [EmailAddress(ErrorMessage = "Email value should be a valid email")]
        //generates type of email in input in html
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date of birth can't be blank")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender can't be null")]
        public GenderOptions? Gender { get; set; }

        [Required(ErrorMessage = "Country can't be null")]
        public Guid? CountryID { get; set; }

        public string? Address { get; set; }

        public bool ReceivesNewsLetters { get; set; }

        public Person ToPerson()
        {
            return new Person()
            {
                PersonName = PersonName,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Gender = Gender.ToString(),
                Address = Address,
                CountryID = CountryID,
                ReceivesNewsLetters = ReceivesNewsLetters
            };
        }
    }
}

