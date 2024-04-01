function setStorageData() {
    if (localStorage.getItem("login")) {
        console.log((localStorage.getItem("login")));

        var data = JSON.parse(localStorage.getItem("login"));

        $("#dashboard_username").text(data.firstName);
    }
}

//setStorageData();