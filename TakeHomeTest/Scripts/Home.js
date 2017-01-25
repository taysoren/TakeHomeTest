var Home = {}

var blockMessage = '<div class="submit-progress hidden"><label>Please wait while loading results.</label>;</div>';
var blockstyle = '{position:fixed;height:6em;padding-top:2.5em;z-index:1;width:20em;margin:auto;padding-left:2.1em;background-color:black;color:white;}';

Home.SearchButtonClick = function () {

	var name = $("#searchCriteria").val();
	if (name) {
		//alert("name is valid: " + name);
		$("#results").html("");
		$.ajax({
			type: "GET",
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data: name,
			url: "/Home/SearchNames?name=" + name,
			success: OnSuccess,
			error: OnError
		});
		$("#returnCountLabel").html("");
		DisplayProgressMessage(this, "Loading Results...")
	}

	function OnSuccess(data) {
		data = JSON.parse(data);
		$("#results").html("");
		$("#returnCountLabel").html("Found " + data.length + " Matches");
		var personCreator = new PersonCardCreator();
		if (data.length > 0) {
			for (var i = 0; i < data.length; i++) {
				//convert image to base64
				var imgSrc = personCreator.ConvertImageData(data[i].Picture);
				//Format Address
				var address = data[i].Address;
				address = address.replace(/:/g, "<br/>");
				//Format interests
				var interests = data[i].Interests;
				interests = interests.replace(/,/g, ", ");
				//The Template we use to display each result
				var personCardTemplate = CreatePersonCard(data[i]);

				$("#results").append(personCardTemplate);
			}
		}
		else {
			$("#returnCountLabel").html("No Results");
		}
		$("#searchButton").prop("disabled", false).text("Search");
	}


	function OnError(data) {
		alert("ERROR");
		$("#results").html("No Results");
	}
};

function PersonCardCreator() {
	var self = this;
	this.CreatePersonCard = function CreatePersonCard(jsonDataItem) {
		//convert image to base64
		var imgSrc = self.ConvertImageData(data[i].Picture);
		//Format Address
		var address = data[i].Address;
		address = address.replace(/:/g, "<br/>");
		//Format interests
		var interests = data[i].Interests;
		interests = interests.replace(/,/g, ", ");
		//The Template we use to display each result
		var personCardTemplate = [
		'<div id="PersonCardDiv" style="border: solid 2px #E0E0E0; margin: 8px; padding:5px;width:auto;height:auto;float:left">',
			'<h2 align="center">',
			data[i].FirstName + " " + data[i].LastName + ", " + data[i].Age,
			'</h2>',
			'<div class="info" id="contactInfo" style="align-content:space-between;margin:5px">',
				'<table cellpadding="10" align="center">',
				'<tr valign ="center">',
				'<td>',
				address,
				'</td>',
				'<td>',
				data[i].PhoneNumber,
				'</td>',
				'<td>',
				data[i].Email,
				'</td>',
				'</tr>',
				'</table>',
			'</div>',
			'<div class ="info" id="otherInfo" style="align-content:space-between;margin:5px;width:inherit">',
				'<table cellpadding="5" align="center" style="width:100%">',
				'<tr>',
				'<td valign="top" style="width:200px" align="center">',
				'<h4>Interests:</h4>',
				interests,
				'</td>',
				'<td align="right">',
				'<img id="personPic" width="196" src="' + imgSrc + '" style="border:solid 2px grey;margin: 2px"/></td>',
				'</td></tr></table>',
			'</div>',
		'</div>',
		].join("\n");

		return personCardTemplate;
	}

	self.ConvertImageData = function (jsonImgData) {
		//Convert to string then base64 
		var base64 = btoa(uint8ToString(jsonImgData));
		return 'data:image/jpg;base64,' + base64;
	}

	self.uint8ToString = function (buf) {
		var i, length, out = '';
		for (i = 0, length = buf.length; i < length; i += 1) {
			out += String.fromCharCode(buf[i]);
		}
		return out;
	}
};




function DisplayProgressMessage(ctl, msg) {
	$(ctl).prop("disabled", true).text(msg);
	$(".submit-progress").remove("hidden");
};

$(document).ready(function () {
	$(".searchButton").click(Home.SearchButtonClick);
	$(".createButton").click(Home.CreateButtonClick);
});

