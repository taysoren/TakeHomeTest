namespace TakeHomeTest.Migrations
{
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using TakeHomeTest.Models;
	using TakeHomeTest.Seeder;

    internal sealed class Configuration : DbMigrationsConfiguration<TakeHomeTest.Models.PeopleContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TakeHomeTest.Models.PeopleContext context)
        {
			People people = PeopleProducer.ProducePeople();
			context.People.AddOrUpdate(people.ToArray());
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
