using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TakeHomeTest.Models;

namespace TakeHomeTest.Seeder {
	public static class PeopleProducer {
		public static People ProducePeople(string xmlPath) {
			return PeopleXMLParser.ParseXML(xmlPath);
		}

		public static void AppendRandomImagesToPeople(ref People people, string folderPath) {
			if(people != null) {
				string[] imagePaths = new string[0];
				try {
					imagePaths = Directory.GetFiles(folderPath);
				}
				catch(Exception ex) {
					Console.WriteLine(string.Format("Error getting images: {0}", ex.Message));
					throw new Exception(ex.Message);
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

}