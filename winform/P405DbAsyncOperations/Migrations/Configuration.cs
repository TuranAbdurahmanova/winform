namespace P405DbAsyncOperations.Migrations
{
    using P405DbAsyncOperations.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<P405DbAsyncOperations.Models.PersonDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(P405DbAsyncOperations.Models.PersonDbContext context)
        {
            Person person1 = new Person
            {
                Email = "turan@gmail.com",
                Name = "Turan",
                Password = "1234",
                Surname = "Abdurahmanova",
            };
            Person person2 = new Person
            {
                Email = "gunelum@code.edu.az",
                Name = "Gunel",
                Password = "1234",
                Surname = "Memmedova",
            };
            Person person3 = new Person
            {
                Email = "matinnn@code.edu.az",
                Name = "Metin",
                Password = "1234",
                Surname = "Nuri",
            };

            context.People.AddOrUpdate(person1, person2, person3);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
