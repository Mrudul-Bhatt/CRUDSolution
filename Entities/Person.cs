using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    //Adding constraints like [Required] here is of no use as the model being used is a DTO and not a person model
    //This is the Database Model any change here will require a migration and database update, so just adding constraints here and starting the application will not work
    //Add constraints in DTO as all the validation will be done there. DB Model should not be used directly in the application to interact
    //Use Fluent API for adding constraints to DB Model after creating the migration
    public class Person
    {
        [Key]
        public Guid PersonID { get; set; }

        [StringLength(40)]
        [Required(ErrorMessage = "Person name can't be blank")]
        public string? PersonName { get; set; }

        [StringLength(40)]
        [Required(ErrorMessage = "Email can't be blank")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Date of birth can't be blank")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Gender can't be blank")]
        public string? Gender { get; set; }

        public Guid? CountryID { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        public bool ReceivesNewsLetters { get; set; }

        public string? TIN { get; set; }

        //Navigation Property to Country Table : These properties are null by default
        public Country? Country { get; set; }
    }
}

