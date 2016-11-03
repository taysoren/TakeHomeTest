using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TakeHomeTest.Seeder;
using TakeHomeTest.Models;
using System.Linq;

namespace TakeHomeTest.Tests.SeederTests {
	[TestClass]
	public class SeederTests {
		[TestMethod]
		public void TestPeopleXMLParser() {
			People people = PeopleXMLParser.ParseXML(@"TestData/TestPersons.xml");
			Assert.IsNotNull(people);
		}

		[TestMethod]
		public void TestPictureAppender() {
			People people = new People() { new Person() { PersonID = 1 }, new Person() { PersonID = 1 }, new Person() { PersonID = 1 }, new Person() { PersonID = 1 } };
			PeopleProducer.AppendRandomImagesToPeople(ref people, @"TestData/Pictures");
			var personsWhoHavePictures = people.Where(p => p.Picture != null).ToArray();
			Assert.IsTrue(personsWhoHavePictures.Length == people.Count);
		}

		[TestMethod]
		public void TestRandomInterestsGenerator() {
			var res1 = PeopleProducer.GenerateRandomInterests(10);
			var res2 = PeopleProducer.GenerateRandomInterests(10);
			var res3 = PeopleProducer.GenerateRandomInterests(10);
			Assert.IsTrue(res1 != res2 && res2 != res3);
		}
	}
}
