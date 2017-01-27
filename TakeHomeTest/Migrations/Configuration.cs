namespace TakeHomeTest.Migrations {
	using Newtonsoft.Json.Linq;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.IO;
	using System.Linq;
	using System.Net;
	using System.Reflection;
	using System.Web;
	using System.Web.Hosting;
	using TakeHomeTest.Models;
	using TakeHomeTest.Seeder;

	internal sealed class Configuration : DbMigrationsConfiguration<TakeHomeTest.Models.PeopleContext> {
		public Configuration() {
			AutomaticMigrationsEnabled = false;
			AutomaticMigrationDataLossAllowed = true;
		}

		protected override void Seed(TakeHomeTest.Models.PeopleContext context) {

			if(System.Diagnostics.Debugger.IsAttached == false) {
				//System.Diagnostics.Debugger.Launch();
			}

			People people = new People();
			int seedCount = new Random().Next(50,100);

			using(WebClient wc = new WebClient()) {
				var json = wc.DownloadString("http://api.randomuser.me/?results="+seedCount);
				JToken j = JObject.Parse(json)["results"];
				int idNumber = 0;
				foreach(var person in j) {
					people.Add(new Person(idNumber, person));
					idNumber++;
				}
			}
			if(people != null) {
				context.People.AddOrUpdate(people.ToArray());
			}
			else {
				throw new Exception(MapPath("Seed Failed"));
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
