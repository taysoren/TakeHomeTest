using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TakeHomeTest.Seeder;
using TakeHomeTest.Models;

namespace TakeHomeTest.Tests.SeederTests {
	[TestClass]
	public class SeederTests {
		[TestMethod]
		public void TestPeopleXMLParser() {
			People people = PeopleXMLParser.ParseXML(@"TestData/TestPersons.xml");
			Assert.IsNotNull(people);
		}
	}
}
