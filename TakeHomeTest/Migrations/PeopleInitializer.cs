using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using TakeHomeTest.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Reflection;
using System.Web.Hosting;


namespace TakeHomeTest.Migrations {
	public class PeopleInitializer : System.Data.Entity.DropCreateDatabaseAlways<PeopleContext> {
		public PeopleInitializer() {
		}

		protected override void Seed(TakeHomeTest.Models.PeopleContext context) {

			if(System.Diagnostics.Debugger.IsAttached == false) {
				System.Diagnostics.Debugger.Launch();
			}
			People people = new People();
			int seedCount = new Random().Next(50, 100);

			using(WebClient wc = new WebClient()) {
				var json = wc.DownloadString("http://api.randomuser.me/?results=" + seedCount);
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
				throw new Exception("Could Not retrieve results from http://api.randomuser.me");
			}
		}

	}
}