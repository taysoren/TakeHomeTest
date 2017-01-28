$(document).ready(function () {
	$("#searchButton").click(Home.NormalSearchButtonClick);
	$("#slowSearchButton").click(Home.SlowSearchButtonClick);
	$(".material-icons").click(Home.DeleteButtonClick);
	$(".createButton").click(Home.CreateButtonClick);
});
