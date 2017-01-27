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
		var delay = 500;
		setTimeout(function () {
			DisplayProgressMessage(this, "Loading Results...")
		}, delay);
	}

	function OnSuccess(data) {
		data = JSON.parse(data);
		$("#results").html("");
		$("#returnCountLabel").html("Found " + data.length + " Matches");
		if (data.length > 0) {
			for (var i = 0; i < data.length; i++) {
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

	function CreatePersonCard(jsonDataItem) {
		//convert image to base64
		var base64 = btoa(uint8ToString(jsonDataItem.Picture));
		var imgSrc = 'data:image/jpg;base64,' + base64;
		//Format Address
		var addresses = jsonDataItem.Address.split(":");
		//The Template we use to display each result
		var personCardTemplate = [
		'<div class="grid-33">',
			'<div class="card">',
				'<div class="card-header">',
					'<h3>', jsonDataItem.FirstName + " " + jsonDataItem.LastName, '</h3>',
					'<div class="card-header-controls">',
						'<i class="material-icons">&#xE872;</i>',
					'</div>',
				'</div>',
				'<div class="card-body">',
					'<img src="' + imgSrc + '" alt="" title="" />',
					'<div class="card-content">',
						'<p>', addresses[0], '</p>',
						'<p>', addresses[1], '</p>',
						'<p>', jsonDataItem.UserName, '</p>',
						'<p>', jsonDataItem.PhoneNumber, '</p>',
					'</div>',
				'</div>',
			'</div>',
		'</div>'].join("\n");
		return personCardTemplate;
	}

};

Home.DeleteButtonClick = function (id) {
	$.ajax({
		url: "/Home/Delete?name=" + id,
		type: 'DELETE',
		success: function (result) {
			// Do something with the result
		}
	});
};

function DisplayProgressMessage(ctl, msg) {
	$(ctl).prop("disabled", true).text(msg);
	$(".submit-progress").remove("hidden");
};

function DisplayWaitingMessage(ctl, msg) {
	$(ctl).prop("disabled", true).text(msg);
	$(".submit-progress").remove("hidden");
};

$(document).ready(function () {
	$(".searchButton").click(Home.SearchButtonClick);
	$(".material-icons").add('click', function(){
		Home.DeleteButtonClick(0);
	});
	$(".createButton").click(Home.CreateButtonClick);
});

