using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TakeHomeTest.Models {
	public class Person {
		#region ctor
		public Person() { }

		public Person(string json) {
			JObject j = JObject.Parse(json);
			//JToken jid = j["PersonID"];
			PersonID = (int)j["PersonID"];
			FirstName = (string)j["FirstName"];
			LastName = (string)j["LastName"];
			Age = (int)j["Age"];
			Address = (string)j["Address"];
			PhoneNumber = (string)j["PhoneNumber"];
			Email = (string)j["Email"];
			Interests = (string)j["Interests"];
			Picture = (byte[])j["Picture"];

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
		private int _age;

		public int Age {
			get { return _age; }
			set { _age = value; }
		}
		private string _address;

		public string Address {
			get { return _address; }
			set { _address = value; }
		}
		private string _phoneNumber;

		public string PhoneNumber {
			get { return _phoneNumber; }
			set { _phoneNumber = value; }
		}
		private string _email;

		public string Email {
			get { return _email; }
			set { _email = value; }
		}
		private string _interests;

		public string Interests {
			get { return _interests; }
			set { _interests = value; }
		}
		private byte[] _picture;

		public byte[] Picture {
			get { return _picture; }
			set { _picture = value; }
		}
		#endregion//Fields/Properties
	}
}