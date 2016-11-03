using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TakeHomeTest.Models;

namespace TakeHomeTest.Seeder {
	public static class PeopleProducer {
		public static People ProducePeople() {

			People people = PeopleXMLParser.ParseXML(@"peopleList.xml");
			if(people != null) {
				string[] imagePaths = new string[0];
				try {
					imagePaths = Directory.GetFiles(@"Pictures");
				}
				catch(Exception ex) {
					Console.WriteLine(string.Format("Error getting images: {0}", ex.Message));
					return null;
				}
				var rand = new Random();
				if(imagePaths != null && imagePaths.Length > 0) {
					foreach(Person person in people) {
						var image = File.ReadAllBytes(imagePaths[rand.Next(imagePaths.Length)]);
						person.Picture = image;
					}
				}
			}
			return people;
		}
	}

}