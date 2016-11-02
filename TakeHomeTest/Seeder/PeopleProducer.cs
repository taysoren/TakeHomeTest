using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TakeHomeTest.Models;

namespace TakeHomeTest.Seeder {
	class PeopleProducer {
		public void ProducePeople() {
			People people = PeopleXMLParser.ParseXML(@"people.xml");
			if(people != null) {
				string[] imagePaths = new string[0];
				try {
					imagePaths = Directory.GetFiles(@"Pictures");
				}
				catch(Exception ex) {
					Console.WriteLine(string.Format("Error getting images: {0}", ex.Message));
					return;
				}
				var rand = new Random();
				if(imagePaths != null && imagePaths.Length > 0) {
					foreach(Person person in people) {
						var image = File.ReadAllBytes(imagePaths[rand.Next(imagePaths.Length)]);
						person.Picture = image;
					}
				}
			}
		}
	}
	class People : List<Person> {
	}
}