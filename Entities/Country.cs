using System.ComponentModel.DataAnnotations;

namespace Entities
{
    /// <summary>
    /// This is model and should be in seperate project and it should not directly deal with controllers 
    /// and view. It should only deal with business logic. And it should only be used as DTO
    /// </summary>
    public class Country
    {
        //Primary Key Annotation
        [Key]
        public Guid CountryID { get; set; }
        public string? CountryName { get; set; }

        //Navigation Property to Person Table : These properties are null by default
        //public virtual ICollection<Person>? Persons { get; set; }
    }
}