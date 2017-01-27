using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
				XDocument xml = XDocument.Load(xmlpath);
				int i = 0;
				List<Person> persons = xml.Descendants("people").
					Descendants("person").
					Select(p => new Person(){ 
						PersonID = i++,
						FirstName = p.Element("FirstName").Value,
						LastName = p.Element("LastName").Value,
						Address = string.Format("{0}:{1}, {2} {3}", p.Element("Address").Value, p.Element("City").Value, p.Element("State").Value, p.Element("Zip").Value),
						PhoneNumber = p.Element("PhoneNumber").Value,
					}).ToList();

				People people = new People();
				people.AddRange(persons);
				return people;
			}
			catch(InvalidOperationException ex) {
				Console.WriteLine(string.Format("InvalidOperationException - InnerException:{0}", ex.InnerException.Message));
				return null;
			}
		}
	}
}