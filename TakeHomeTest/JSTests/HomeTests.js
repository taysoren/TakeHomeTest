﻿/// <reference path ="jasmine.js"/>
/// <reference path="../Scripts/Home.js"/>
var badJSON = "[{\"PersonID\":506,\"FirstName\":\"Josephine\",\"LastName\":\"Darakjy\",\"Age\":34,\"Address\":\"4 B Blue Ridge Blvd:Brighton, MI 48116\",\"PhoneNumber\":\"810-292-9388\",\"Email\":\"josephine_darakjy@darakjy.org\",\"Interests\":\"Compositing,Entimology,Basketball,History,Therapy,Soccer,Spelunking\",";
var creator = new PersonCardCreator();

describe("Home.SuccessJSONParse", function () {
	it("will format json results", function () {
		creator.CreatePersonCard(goodJSON);
	});
});

describe("Home.Fail", function () {
	it("will format json results", function () {
		creator.CreatePersonCard(badJSON);
	});
});