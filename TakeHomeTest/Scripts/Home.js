var Home = {}
Home.SearchButtonClick = function() {
	var name = $("#searchCriteria").val();
	if (name) {
		alert("name is valid: " + name);

		$.ajax({
			type: "GET",
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data: name,
			url: "/Home/GetData?name="+name,
			success: OnSuccess,
			error: OnError
		});
	}
	function OnSuccess(data) {
		$("#results").html("");
		data = JSON.parse(data);
		data.length;
		for (var i = 0; i < data.length; i++) {
			$("#results").append(data[i].FirstName);
		}
	}
	function OnError(data) {
		alert("ERROR");
		$("#results").html("No Results");
	}
};

Home.TestFunc = function () {
	var name = $("#searchCriteria").val();
	if (name) {
		alert("Doiing the other");

		$.post("/Home/GetData", name, OnSuccess);
	}
	function OnSuccess(data) {
		$("#results").html(data);
	}
	function OnError(data) {
		alert("ERROR");
		$("#results").html("No Results");
	}
};

Home.CreateButtonClick = function () {
	var person = { FirstName: "Me", LastName: "Mine" }
	$.ajax({
		type: "POST",
		contentType: "application/json; charset=utf-8",
		data: "{person:" + JSON.stringify(person) + "}",
		url: "/Home/GetData",
		success: function (data) {
			alert("Person Name: " + data.FirstName + " " + data.LastName);
		},
		error: function (result) {
			alert("No Results");
		}
	})
};

$(document).ready(function () {
	$(".searchButton").click(Home.SearchButtonClick);
	$(".createButton").click(Home.CreateButtonClick);
});
