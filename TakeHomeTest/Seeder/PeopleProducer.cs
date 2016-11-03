using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TakeHomeTest.Models;

namespace TakeHomeTest.Seeder {
	public static class PeopleProducer {
		public static Random RandomNumber = new Random();
		private static string[] _interests = new string[]{
			"Biking", "Hiking", "Rock Climbing", "Camping", "Sky Diving", "Canyoneering", "Spelunking", "Splints",
			"Foosball", "Pingpong", "Soccer", "Hockey", "Football", "Lacrosse", "Basketball", "Concussion Research",
			"Shopping", "Long walks on the beach", "Massage Therapy",
			"Hypnosis", "Neuro Linquistic Programming", "Therapy", "Psychology", "Out of body experience",
			"Gaming", "Board Games", "Role Playing", "Comics", "Fan Theories", "Virtual Reality",
			"Creative Writing", "Reading", "Poetry", "Short Stories", "History",
			"Music", "Chello", "Violin", "Trumpet", "Bassoon", "Flugelhorn", "French Horn", "Penny Flute",
			"Drawing", "Visual Effects", "3D Modeling", "3D Rendering", "Procedural 3D Shaders", "Compositing",
			"Cars", "Engine Rebuilding", "Motorcycles", 
			"Zoology", "Animals", "Apiculture", "Entimology", "Marine Biology", 
			"Ditch Digging", "Gardening", "Grafting", 
			"Mortuary Science", "Anatomy", "Mummification", "Ancient Civilizations", 
			"Conspiracy Theories", "Astronomy", 
			"Rock Collecting", "Coin Collecting", "Spoon Collecting", "Shrunken Head Collecting",
		};

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

		public static void GiveInterests(ref People people) {
			foreach(Person p in people) {
				GiveRandomInterests(p, RandomNumber.Next(2, 10), RandomNumber.Next(1, 10000));
			}
		}

		public static void GiveRandomInterests(Person person, int count, int seed) {
			string[] personInterests = GenerateRandomInterests(count);
			person.Interests = string.Join(",", personInterests);
		}

		public static string[] GenerateRandomInterests(int desiredCount) {
			List<string> personInterests = new List<string>();
			for(int i = 0; i < desiredCount; i++) {
				string interest = _interests[RandomNumber.Next(0, _interests.Length - 1)];
				if(!personInterests.Contains(interest)){
					personInterests.Add(interest);
				}
			}
			return personInterests.ToArray();
		}
	}

}