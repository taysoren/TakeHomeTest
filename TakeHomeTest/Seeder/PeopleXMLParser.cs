using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace TakeHomeTest.Seeder {
	public static class PeopleXMLParser {
		public static People ParseXML(string xmlpath) {
			if(!File.Exists(xmlpath)) {
				Console.WriteLine("File does not exist");
				return null;
			}
			try {
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