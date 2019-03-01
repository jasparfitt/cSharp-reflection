let readRequest;

let markAsRead = (id, url, isRead) => {
    readRequest = new XMLHttpRequest;
    if (!readRequest) {
        alert("No instance found");
    }
    readRequest.onreadystatechange = readResponse;
    readRequest.open("POST", url + "/read/" + id);
    readRequest.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    readRequest.send(isRead);
    let readButton = document.getElementById(`read-${id}`);
    let head = document.getElementById(`content-${id}`);
    console.log(head);
    let title = head.children[0];
    let authors = head.children[1];
    console.log(title);
    console.log(authors);
    if (readButton.classList.contains("disabled")) {
        readButton.classList.remove("disabled");
        title.classList.remove("strike-through");
        authors.classList.remove("strike-through");
    } else {
        readButton.classList.add("disabled");
        title.classList.add("strike-through");
        authors.classList.add("strike-through");
    }
}

let readResponse = () => {
    if (readRequest.readyState === XMLHttpRequest.DONE) {
        if (readRequest.status === 200) {
            //response = JSON.parse(readRequest.response.Text)
        }
    }

}