namespace TakeHomeTest.Migrations {
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.IO;
	using System.Linq;
	using System.Reflection;
	using System.Web;
	using System.Web.Hosting;
	using TakeHomeTest.Models;
	using TakeHomeTest.Seeder;

	internal sealed class Configuration : DbMigrationsConfiguration<TakeHomeTest.Models.PeopleContext> {
		public Configuration() {
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(TakeHomeTest.Models.PeopleContext context) {
			People people = PeopleProducer.ProducePeople(MapPath("~/Seeder/peopleList.xml"));
			if(people != null) {
				PeopleProducer.AppendRandomImagesToPeople(ref people, MapPath("~/Seeder/Pictures"));
				context.People.AddOrUpdate(people.ToArray());
			}
			else {
				throw new Exception(MapPath("~/Seeder/peopleList.xml"));
			}
		}

		private string MapPath(string seedFile) {
			if(HttpContext.Current != null)
				return HttpUtility.UrlDecode(HostingEnvironment.MapPath(seedFile));

			var absolutePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
			var directoryName = Path.GetDirectoryName(absolutePath);
			var path = Path.Combine(directoryName, ".." + seedFile.TrimStart('~').Replace('/', '\\'));

			return HttpUtility.UrlDecode(path);
		}
	}
}
