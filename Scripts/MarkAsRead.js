let readRequest;

let markAsRead = (id, url) => {
    readRequest = new XMLHttpRequest;
    if (!readRequest) {
        alert("No instance found");
    }
    readRequest.onreadystatechange = readResponse;
    readRequest.open("POST", url + "/read/" + id);
    readRequest.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    readRequest.send();
    let readButton = document.getElementById(`read-${id}`);
    if (readButton.classList.contains("Disabled")) {
        readButton.classList.remove("Disabled");
    } else {
        readButton.classList.add("Disabled");
    }
}

let readResponse = () => {
    if (readRequest.readyState === XMLHttpRequest.DONE) {
        if (readRequest.status === 200) {
            //response = JSON.parse(readRequest.response.Text)
        }
    }

}