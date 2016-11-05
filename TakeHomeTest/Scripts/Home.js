var Home = {}

var personCardTemplate = [
	'<div id="PersonCardDiv" style="border: solid 2px #E0E0E0; margin: 8px; padding:5px; width: 420px;height:420px;float:left">',
	'<h2 align="center">{{personHeader}}</h2>',
	'<div class="info" id="contactInfo" style="align-content:space-between;margin:5px"><table cellpadding="5" align="center"><tr valign ="center">',
	'<td>{{Address}}</td>',
	'<td>{{PhoneNumber}}</td>',
	'<td>{{Email}}</td>',
	'</tr></table></div>',
	'<div class ="info" id="otherInfo" style="align-content:space-between;margin:5px"><table cellpadding="5" align="center"><tr>',
	'<h4>Interests:</h4>',
	'<td>{{Interests}}</td>',
	'<td><img id="personPic" width="196" src="{{Picture}}" style="border:solid 2px grey;margin: 2px"/></td>',
	'</td></tr></table></div></div>',
].join("\n");



Home.SearchButtonClick = function() {
	var name = $("#searchCriteria").val();
	if (name) {
		//alert("name is valid: " + name);
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
		if (data.length > 0) {
			for (var i = 0; i < data.length; i++) {
				 
				//var base64 = b64EncodeUnicode(data[i].Picture);
				var base64 = btoa(uint8ToString(data[i].Picture));
				//var base64 = btoa(data[i].Picture);
				//var base64 = data[i].Picture;
				var imgSrc = 'data:image/jpg;base64,' + base64;

				var personCardTemplate = [
				'<div id="PersonCardDiv" style="border: solid 2px #E0E0E0; margin: 8px; padding:5px; width: 420px;height:420px;float:left">',
				'<h2 align="center">',
				data[i].FirstName + " " + data[i].LastName + ", " + data[i].Age,
				'</h2>',
				'<div class="info" id="contactInfo" style="align-content:space-between;margin:5px"><table cellpadding="5" align="center"><tr valign ="center">',
				'<td>',
				data[i].Address,
				'</td>',
				'<td>',
				'<td>',
				data[i].Email,
				'</td>',
				'</tr></table></div>',
				'<div class ="info" id="otherInfo" style="align-content:space-between;margin:5px"><table cellpadding="5" align="center"><tr>',
				'<h4>Interests:</h4>',
				'<td>',
				data[i].Interests,
				'</td>',
				'<td><img id="personPic" width="196" src="' + imgSrc + '" style="border:solid 2px grey;margin: 2px"/></td>',
				'</td></tr></table></div></div>',
				].join("\n");
				$("#results").append(personCardTemplate);
				//$("#results").append(imgSrc);
			}
		}
		else {
			$("#results").html("No Results");
		}
	}

	function uint8ToString(buf) {
		var i, length, out = '';
		for (i = 0, length = buf.length; i < length; i += 1) {
			out += String.fromCharCode(buf[i]);
		}
		return out;
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
