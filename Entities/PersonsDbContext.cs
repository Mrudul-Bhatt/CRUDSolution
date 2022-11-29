using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    //DbContext is present in NuGet Package Microsoft.EntityFrameworkCore.SqlServer, so in order to use it, we need to install it
    public class PersonsDbContext : DbContext
    {
        //This is the constructor of the class to provide the options we used in Program.cs (such as use SqlServer as DB) to Parent DbContext class
        public PersonsDbContext(DbContextOptions<PersonsDbContext> options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Map DbSets to corresponding tables
            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<Person>().ToTable("Persons");

            //Seed data means creating initial data for Database when tables are created initially
            //Seed data manually
            //modelBuilder.Entity<Country>().HasData(
            //    new Country() { CountryID = Guid.NewGuid(), CountryName = "India" },
            //    new Country() { CountryID = Guid.NewGuid(), CountryName = "USA" },
            //    new Country() { CountryID = Guid.NewGuid(), CountryName = "UK" }
            //);

            //modelBuilder.Entity<Person>().HasData(
            //    new Person()
            //    {
            //        PersonID = Guid.Parse("28d11936-9466-4a4b-b9c5-2f0a8e0cbde9"),
            //        PersonName = "Minta",
            //        Email = "mconachya@va.gov",
            //        DateOfBirth = DateTime.Parse("1990-05-24"),
            //        Gender = "Female",
            //        CountryID = Guid.Parse("501c6d33-1bbe-45f1-8fbd-2275913c6218"),
            //        Address = "2 Warrior Avenue",
            //        ReceivesNewsLetters = true
            //    }
            //);

            //Seed data via JSON file
            //Seed to Countries
            string countriesJson = System.IO.File.ReadAllText("JSONData/countries.json");
            List<Country> countries = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(countriesJson);

            foreach (Country country in countries)
                modelBuilder.Entity<Country>().HasData(country);


            //Seed to Persons
            string personsJson = System.IO.File.ReadAllText("JSONData/persons.json");
            List<Person> persons = System.Text.Json.JsonSerializer.Deserialize<List<Person>>(personsJson);

            foreach (Person person in persons)
                modelBuilder.Entity<Person>().HasData(person);

        }
    }
}
