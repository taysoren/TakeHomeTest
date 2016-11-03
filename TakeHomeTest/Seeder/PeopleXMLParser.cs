using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml.Serialization;
using TakeHomeTest.Models;

namespace TakeHomeTest.Seeder {
	public static class PeopleXMLParser {
		public static People ParseXML(string xmlpath) {
			//var files = Directory.GetFiles("TestData");
			if(!File.Exists(xmlpath)) {
				Console.WriteLine("File does not exist");
				return null;
			}
			try {
				XDocument xml = new XDocument(xmlpath);
				var persons = xml.Descendants("People").ToList();

				XmlSerializer serializer = new XmlSerializer(typeof(People));
				FileStream stream = new FileStream(xmlpath, FileMode.Open, FileAccess.Read);
				People people = (People)serializer.Deserialize(stream);
				stream.Close();
				return people;
			}
			catch(InvalidOperationException ex) {
				Console.WriteLine(string.Format("InvalidOperationException - InnerException:{0}", ex.InnerException.Message));
				return null;
			}
		}
	}
}