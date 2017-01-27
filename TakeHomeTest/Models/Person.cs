using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace TakeHomeTest.Models {
	public class Person {
		#region ctor
		public Person() { }

		public Person(int id, JToken person) {
			PersonID = id;
			//person = person[0];
			FirstName = (string)person["name"]["first"];
			LastName = (string)person["name"]["last"];
			UserName = (string)person["login"]["username"];
			Address = string.Format("{0}:{1}, {2} {3}",(string)person["location"]["street"],(string)person["location"]["city"],(string)person["location"]["state"],(string)person["location"]["postcode"]);
			PhoneNumber = (string)person["phone"];
			using(WebClient client = new WebClient()) {
				Picture = client.DownloadData((string)person["picture"]["medium"]);
			}
		}
		#endregion

		#region Fields/Properties
		public int PersonID { get; set; }

		private string _firstName;

		public string FirstName {
			get { return _firstName; }
			set { _firstName = value; }
		}
		private string _LastName;

		public string LastName {
			get { return _LastName; }
			set { _LastName = value; }
		}

		private string _address;

		public string Address {
			get { return _address; }
			set { _address = value; }
		}
		private string _userName;

		public string UserName {
			get { return _userName; }
			set { _userName = value; }
		}
		private string _phoneNumber;

		public string PhoneNumber {
			get { return _phoneNumber; }
			set { _phoneNumber = value; }
		}
		
		private byte[] _picture;

		public byte[] Picture {
			get { return _picture; }
			set { _picture = value; }
		}
		#endregion//Fields/Properties
	}
}