var Home = {}

var blockMessage = '<div class="submit-progress hidden"><label>Please wait while loading results.</label>;</div>';
var blockstyle = '{position:fixed;height:6em;padding-top:2.5em;z-index:1;width:20em;margin:auto;padding-left:2.1em;background-color:black;color:white;}';
var searchReturned = false;
Home.CreatePersonCard = function (jsonDataItem) {
	function uint8ToString(buf) {
		var i, length, out = '';
		if (buf) {
			for (i = 0, length = buf.length; i < length; i += 1) {
				out += String.fromCharCode(buf[i]);
			}
		}
		return out;
	}
	//convert image to base64
	var base64 = btoa(uint8ToString(jsonDataItem.Picture));
	var imgSrc = 'data:image/jpg;base64,' + base64;
	//Format Address
	var addresses = [];
	if (jsonDataItem.Address) {
		addresses = jsonDataItem.Address.split(":");
	}
	if (addresses.length < 2) {
		if (addresses.length < 1) {
			addresses = ["none", "nothing"];
		}
		else {
			addresses = [addresses[0], "nothing"];
		}
	}
	//The Template we use to display each result
	var personCardTemplate = [
	'<div class="grid-33">',
		'<div class="card">',
			'<div class="card-header">',
				'<h3>', jsonDataItem.FirstName + " " + jsonDataItem.LastName, '</h3>',
				'<div class="card-header-controls">',
					'<i class="material-icons" onclick="Home.DeleteButtonClick(' + jsonDataItem.PersonID + ')">&#xE872;</i>',
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
};


Home.OnSearchSuccess = function (data) {
	searchReturned = true;

	data = JSON.parse(data);
	$("#results").html("");
	$("#returnCountLabel").html("Found " + data.length + " Matches");
	if (data.length > 0) {
		for (var i = 0; i < data.length; i++) {
			//The Template we use to display each result
			var personCardTemplate = Home.CreatePersonCard(data[i]);
			$("#results").append(personCardTemplate);
		}
	}
	else {
		$("#returnCountLabel").html("No Results");
	}
	$("#searchButton").prop("disabled", false).text("Search");
	return true;
};

Home.RunAjaxSearch = function (name) {
	function OnError(data) {
		searchReturned = true;
		alert("ERROR");
		$("#results").html("No Results");
	}

	$("#results").html("");
	$.ajax({
		type: "GET",
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		data: name,
		url: "/Home/SearchNames?name=" + name,
		success: Home.OnSearchSuccess,
		error: OnError
	});
	$("#returnCountLabel").html("");

};

Home.SlowSearchButtonClick = function () {
	Home.SearchButtonClick(true);
};

Home.NormalSearchButtonClick = function () {
	Home.SearchButtonClick(false);
};
Home.SearchButtonClick = function (delayed) {
	searchReturned = false;
	$("#returnCountLabel").html("");
	var name = $("#searchCriteria").val();
	if (name) {
		if (delayed) {
			setTimeout(function () {
				Home.RunAjaxSearch(name);
			}, 2000);
		}
		else {
			Home.RunAjaxSearch(name);
		}
	}
	else {
		$("#results").html('Input cannot be blank, type [All] to get all entries.');
		searchReturned = true;
	}
	var delay = 200;
	setTimeout(function () {
		if (!searchReturned) {
			DisplayProgressMessage(this, "Loading Results...")
		}
	}, delay);

};

Home.DeleteButtonClick = function (id) {
	alert("Delete Implementation Incomplete");
	return;
	if (window.confirm("Are you sure you want to delete this entry")) {
		$.ajax({
			url: "/Home/Delete?id=" + id,
			data: id,
			type: 'DELETE',
			success: function (result) {
				alert("Delete Successful");
			},
			error: function (result) {
				alert("Delete Unsuccessful");
			}
		});
	}
};

function DisplayProgressMessage(ctl, msg) {
	$("#results").html('<img src="http://www.bba-reman.com/images/fbloader.gif"/>');
};
